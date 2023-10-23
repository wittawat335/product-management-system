using Dapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Models;
using Ecommerce.Domain.RepositoryContracts;
using Ecommerce.Infrastructure.DBContext;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class StoredRespository : IStoredRespository
    {
        private readonly DapperContext _context;

        public StoredRespository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> SP_GET_MENU_BY_POSITION(string positionId)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var procedure = "SP_GET_MENU_BY_POSITION";
                    var parameters = new DynamicParameters(new { PositionId = positionId });
                    var results = await connection.QueryAsync<Menu>(procedure, parameters,
                        commandType: CommandType.StoredProcedure);

                    return results.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SP_GET_PERMISSION_BY_POSITION_RESULT>> SP_GET_PERMISSION_BY_POSITION(string positionId)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var procedure = "SP_GET_PERMISSION_BY_POSITION";
                    var parameters = new DynamicParameters(new { PositionId = positionId });
                    var results = await connection.QueryAsync<SP_GET_PERMISSION_BY_POSITION_RESULT>(procedure, parameters,
                        commandType: CommandType.StoredProcedure);

                    return results.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
