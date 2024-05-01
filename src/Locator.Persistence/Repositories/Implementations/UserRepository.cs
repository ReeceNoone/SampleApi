using System.Linq.Expressions;
using Locator.Persistence.Entities;

namespace Locator.Persistence.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly IGenericInMemoryRepository<UserEntity> _genericRepository;

    public UserRepository(IGenericInMemoryRepository<UserEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public Task<UserEntity?> GetByIdAsync(Guid id)
    {
        return _genericRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return _genericRepository.GetAllAsync();
    }

    public Task<UserEntity> AddAsync(UserEntity userEntity)
    {
        return _genericRepository.AddAsync(userEntity);
    }

    public Task<UserEntity> UpdateAsync(UserEntity userEntity)
    {
        return _genericRepository.UpdateAsync(userEntity);
    }

    public Task DeleteAsync(Guid id)
    {
        return _genericRepository.DeleteAsync(id);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _genericRepository.ExistsAsync(id);
    }

    public Task<bool> ExistsAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        return _genericRepository.ExistsAsync(predicate);
    }
}