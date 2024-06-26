﻿namespace Locator.Persistence.Entities;

public class UserEntity : IEntity
{
    public required Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }
}