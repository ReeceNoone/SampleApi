using Locator.Persistence.Entities;

namespace Locator.Persistence.Repositories;

public interface IUserRepository
{
    public Task<UserEntity?> GetByIdAsync(Guid id);

    public Task<IEnumerable<UserEntity>> GetAllAsync();

    public Task<UserEntity> AddAsync(UserEntity userEntity);

    public Task<UserEntity> UpdateAsync(UserEntity userEntity);

    public Task DeleteAsync(Guid id);
}