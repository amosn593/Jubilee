using System.Collections.Concurrent;
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
    private readonly IEmail _email;

    private static ConcurrentDictionary<string,string> _otpStore= new ConcurrentDictionary<string,string>();
    public AuthenticationController(IAuthentication authentication,Itoken token,IEmail email,
        ILogger<AuthenticationController> logger)
    {
        _logger = logger;
        _Authentication = authentication;
        _response = new ResponseDto();
        _token= token;
        _email= email;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(User user)
    {
        try
        {
            
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);



            var checkEmail = await _Authentication.GetUserByEmail(user.Email);
            var otp = GenerateOtp();

            if (checkEmail == null)
            {
                var result = await _Authentication.RegisterUser(user);
                _response.Result = result;
                _response.Success = true;
                await _email.SendEmail(user.Email, "Registration OTP", $"Welcome {user.FirstName} {Environment.NewLine} Your otp is: {otp}");

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
                var otp = GenerateOtp();

                //store otp
                _otpStore[checkUser.Email] = otp;

                //var loggedUser = new LoginResponseDto()
                //{
                //    Token = token,
                //    user = checkUser

                //};
                _response.Success = true;
                _response.Message = "OTP sent to your email. Please verify to complete";
                //_response.Result = loggedUser;
                await _email.SendEmail(checkUser.Email, "Login confirmation", $"Welcome {checkUser.FirstName} {Environment.NewLine} Your otp is: {otp}");
                return Ok(_response);

            }

        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, "Error registering user");
            throw;
        }

    }
    [HttpPost("VerifyOTP")]
    public async Task<IActionResult> VerifyOTP([FromBody] OTPVerificationDto oTPVerification)
    {
        try
        {
            if(!_otpStore.TryGetValue(oTPVerification.Email, out string storedOtp))
            {
                _response.Error = "No OTP found. Please request a new OTP";
                return BadRequest(_response);
            }
            if (storedOtp !=oTPVerification.OTP)
            {
                _response.Error = "Invalid OTP";
                return BadRequest(_response);
            }

            //Retrieve user for token generation
            var checkUser = _Authentication.GetUserByEmail(oTPVerification.Email).Result;

            var token= _token.GenerateToken(checkUser);

            _otpStore.TryRemove(oTPVerification.Email, out _);

            var loggedUser = new LoginResponseDto()
            {
                Token = token,
                user = checkUser

            };

            _response.Success = true;
            _response.Message = "OTP verified. Login successful";
            _response.Result= loggedUser;
            return Ok(_response);


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error verifying OTP");
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
    private string GenerateOtp()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}
