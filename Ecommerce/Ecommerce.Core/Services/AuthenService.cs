using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs.Authen;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Core.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IGenericRepository<Position> _positionRepository;
        private readonly IGenericRepository<UserPosition> _upRepository;
        private readonly ICommonService _common;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenService(IGenericRepository<User> repository, IGenericRepository<UserPosition> upRepository,
            IGenericRepository<Position> positionRepository,
            ICommonService common,
            IMapper mapper,
            IOptions<JwtSettings> options)
        {
            _repository = repository;
            _upRepository = upRepository;
            _positionRepository = positionRepository;
            _common = common;
            _mapper = mapper;
            _jwtSettings = options.Value;
        }

        public async Task<Response<string>> AddPosition(PositionRequest request)
        {
            var response = new Response<string>();
            try
            {
                var position = await _positionRepository.GetAsync(x => x.PositionName == request.positionName);
                if (position != null)
                {
                    response.isSuccess = Constants.Status.False;
                    response.message = Constants.StatusMessage.DuplicatePosition;
                }
                else
                {
                    var positionResult = await _positionRepository.InsertAsyncAndSave(_mapper.Map<Position>(request));
                    if (positionResult != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.AddSuccessfully;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = Constants.Status.False;
            }
            return response;
        }
        public async Task<Response<LoginResponse>> GenerateToken(User user)
        {
            var response = new Response<LoginResponse>();
            var loginResponse = new LoginResponse();
            try
            {
                var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _jwtSettings.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString())
                    };
                var roles = await _upRepository
                    .GetListAsync(x => x.UserId == user.UserId && x.Status == Constants.Status.Active);
                var roleClaims = roles
                    .Select(x => new Claim(ClaimTypes.Role, _common.GetPositionName(x.PositionId.ToString())));
                claims.AddRange(roleClaims);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                loginResponse.token = new JwtSecurityTokenHandler().WriteToken(token);
                loginResponse.userName = user.Username;
                loginResponse.userId = user.UserId.ToString();
                loginResponse.fullName = user.FullName;
                loginResponse.position = user.Position.PositionName;
                loginResponse.email = user.Email;

                response.message = Constants.StatusMessage.LoginSuccess;
                response.isSuccess = Constants.Status.True;
                response.value = loginResponse;
                response.returnUrl = "~/Dashboard/Index";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public async Task<Response<LoginResponse>> Login(LoginRequest request)
        {
            var response = new Response<LoginResponse>();
            try
            {
                var user = await _repository.GetAsync(x => x.Username == request.userName);
                if (user != null && user.Status == Constants.Status.Active)
                {
                    if (_common.Decrypt(user.Password) == request.password)
                        response = await GenerateToken(user);
                    else
                        response.message = Constants.StatusMessage.InvaildPassword;
                }
                else if (user != null && user.Status == Constants.Status.Inactive)
                    response.message = Constants.StatusMessage.UserInActive;
                else
                    response.message = Constants.StatusMessage.NotFoundUser;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public async Task<Response<string>> Register(RegisterRequest request)
        {
            var response = new Response<string>();
            try
            {
                var userExists = await _repository.GetAsync(x => x.Username == request.userName);
                if (userExists != null)
                {
                    response.isSuccess = Constants.Status.False;
                    response.message = Constants.StatusMessage.DuplicateUser;
                }
                else
                {
                    request.password = _common.Encrypt(request.password);
                    request.positionId = Constants.PositionId.Customer;
                    var user = await _repository
                        .InsertAsyncAndSave(_mapper.Map<User>(request)); // Insert Table User
                    if (user != null)
                    {
                        var result = await _upRepository
                            .InsertAsyncAndSave(_mapper.Map<UserPosition>(user)); // Insert Table UserPosition
                        if (result != null)
                        {
                            response.isSuccess = Constants.Status.True;
                            response.message = Constants.StatusMessage.RegisterSuccess;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = Constants.Status.False;
            }

            return response;
        }
    }
}
