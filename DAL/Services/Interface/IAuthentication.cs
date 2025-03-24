using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.Dtos;

namespace DAL.Services.Interface;
public interface IAuthentication
{
    Task<string> RegisterUser(User RegisterUser);
    Task<LoginResponseDto> LoginUser();
    Task<User> GetUserById(Guid Id);
    Task<User> GetUserByEmail(string Email);
    Task<List<User>> GetUsers();
}
