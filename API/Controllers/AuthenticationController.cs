using BCrypt.Net;
using DOMAIN.Interface;
using DOMAIN.Models;
using DOMAIN.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthentication _Authentication;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly ResponseDto _response;
    private readonly Itoken _token;
    public AuthenticationController(IAuthentication authentication,Itoken token, ILogger<AuthenticationController> logger)
    {
        _logger = logger;
        _Authentication = authentication;
        _response = new ResponseDto();
        _token= token;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(User user)
    {
        try
        {
            
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);



            var checkEmail = await _Authentication.GetUserByEmail(user.Email);

            if(checkEmail == null)
            {
                var result = await _Authentication.RegisterUser(user);
                _response.Result = result;
                _response.Success = true;

                return Ok(_response);
            }
            else
            {
                _response.Error = $"User with {user.Email} exists";
                return BadRequest(_response);
            }


        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserDto loginUser)
    {
        try
        {
            var checkUser = await _Authentication.GetUserByEmail(loginUser.Email);
            if(checkUser == null)
            {
                _response.Error = $"User with email {loginUser.Email} does not exist!!";
                return BadRequest(_response);
            }
            else
            {
                var checkPassword = BCrypt.Net.BCrypt.Verify(loginUser.Password, checkUser.Password);

                if(!checkPassword)
                {
                    _response.Error = "Wrong password";
                    return BadRequest(_response);
                }

                var token = _token.GenerateToken(checkUser);

                var loggedUser = new LoginResponseDto()
                {
                    Token = token,
                    user = checkUser

                };
                _response.Success = true;
                _response.Result = loggedUser;

                return Ok(_response);
            }

        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, "Error registering user");
            throw;
        }

    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var results = await _Authentication.GetUsers();
            return Ok(results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
