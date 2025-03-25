using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DOMAIN.Interface;
using DOMAIN.Models;
using DOMAIN.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Services.Repository;
public class AuthenticationRepo : IAuthentication
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<AuthenticationRepo> _logger;
    public AuthenticationRepo(AppDbContext appDbContext, ILogger<AuthenticationRepo> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    public async Task<User> GetUserByEmail(string Email)
    {
        try
        {
            return await _appDbContext.Users.Where(user => user.Email == Email).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while fetching user by email");
            throw;
        }
    }
    public Task<User> GetUserById(Guid Id) => throw new NotImplementedException();
    public Task<List<User?>> GetUsers()
    {
        try
        {
            var Users = _appDbContext.Users
                .OrderBy(x => x.Id)
                .Include(x => x.Beneficiaries)
                .ToListAsync();
            return Users;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while fetching users");
            throw;
        }   
    }


    public Task<LoginResponseDto> LoginUser() => throw new NotImplementedException();
    public async Task<User> RegisterUser(User RegisterUser)
    {
        try
        {
            _appDbContext.Users.Add(RegisterUser);
            await _appDbContext.SaveChangesAsync();
            return RegisterUser ;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while registering user");
            throw;
        }
    }
}
