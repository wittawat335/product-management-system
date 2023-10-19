using Ecommerce.Domain.Models;
namespace Ecommerce.Domain.RepositoryContracts
{
    public interface IStoredRespository
    {
        Task<List<SP_GET_PERMISSION_BY_POSITION_RESULT>> SP_GET_PERMISSION_BY_POSITION(string positionId);
    }
}
