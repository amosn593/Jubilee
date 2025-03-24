using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.Dtos;
using DAL.Services.Interface;

namespace DAL.Services.Repository;
public class AuthenticationRepo : IAuthentication
{
    public AuthenticationRepo()
    {
        
    }
    public Task<User> GetUserByEmail(string Email) => throw new NotImplementedException();
    public Task<User> GetUserById(Guid Id) => throw new NotImplementedException();
    public Task<List<User>> GetUsers() => throw new NotImplementedException();
    public Task<LoginResponseDto> LoginUser() => throw new NotImplementedException();
    public Task<string> RegisterUser(User RegisterUser) => throw new NotImplementedException();
}
