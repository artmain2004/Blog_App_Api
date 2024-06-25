﻿using Domain.Entity;

namespace Domain.Interface;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(Guid id);
    Task CreateUser(User user);
}