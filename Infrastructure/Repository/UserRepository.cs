﻿using Domain.Entity;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;


public class UserRepository(BlogAppDbContext context) : IUserRepository
{
    private readonly BlogAppDbContext _context = context;


    public async Task<User?> GetUserByEmail(string email)
    {
        var userByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        return userByEmail;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        var userById = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        return userById;
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();
    }
}