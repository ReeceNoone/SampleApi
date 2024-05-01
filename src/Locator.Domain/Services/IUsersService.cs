using Locator.Contracts.Requests.Users;
using Locator.Domain.Models;

namespace Locator.Domain.Services;

public interface IUsersService
{
    public Task<User?> GetByIdAsync(Guid id);

    public Task<User> UpdateAsync(CreateUserRequest user);

    public Task<User> CreateAsync(CreateUserRequest request);

    public Task<IEnumerable<User>> GetAllAsync();
}