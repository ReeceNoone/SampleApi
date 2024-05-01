using AutoMapper;
using Locator.Common.Services;
using Locator.Contracts.Requests.Users;
using Locator.Domain.Models;
using Locator.Persistence.Entities;
using Locator.Persistence.Repositories;

namespace Locator.Domain.Services.Implementations;

public class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;
    private readonly IGuidProvider _guidProvider;
    private readonly IMapper _mapper;

    public UsersService(IUserRepository userRepository, IMapper mapper, IGuidProvider guidProvider)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _guidProvider = guidProvider;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return _mapper.Map<User?>(await _userRepository.GetByIdAsync(id));
    }

    public async Task<User> UpdateAsync(CreateUserRequest user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);

        return _mapper.Map<User>(await _userRepository.UpdateAsync(userEntity));
    }

    public async Task<User> CreateAsync(CreateUserRequest request)
    {
        var userEntity = new UserEntity { Id = _guidProvider.NewGuid(), Email = request.Email, FirstName = request.FirstName, LastName = request.LastName };

        return _mapper.Map<User>(await _userRepository.AddAsync(userEntity));
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<User>>(users);
    }
}