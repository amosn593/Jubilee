using DOMAIN.Interface;
using DOMAIN.Models;
using DOMAIN.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SmileIdController : ControllerBase
{
    private readonly ISmileIdService _smile;

    public SmileIdController(ISmileIdService smile)
    {
        _smile = smile;
    }


    [HttpPost("SmileIDCallback")]
    [AllowAnonymous]
    public async Task<IActionResult> SmileIDCallback()
    {
        try
        {
            using var reader = new StreamReader(HttpContext.Request.Body);


            var body = await reader.ReadToEndAsync();
            Console.WriteLine(body);

            if (!string.IsNullOrEmpty(body))
            {
                //write contents to file
                var mybody = JsonConvert.DeserializeObject<SmileIDCallBackResponse>(body);
                _user.WriteObjectToFile(mybody);
                return Ok(body);
            }

        }
        catch (Exception ex)
        {

        }
        return BadRequest();
    }

    [HttpPost("SmileIDCallback2")]
    [AllowAnonymous]
    public async Task<IActionResult> SmileIDCallback2()
    {
        try
        {
            using var reader = new StreamReader(HttpContext.Request.Body);


            var body = await reader.ReadToEndAsync();
            Console.WriteLine(body);

            if (!string.IsNullOrEmpty(body))
            {
                //write contents to file
                var mybody = JsonConvert.DeserializeObject(body);
                _user.WriteObjectToFile2(mybody);
                return Ok(body);
            }

        }
        catch (Exception ex)
        {

        }
        return BadRequest();
    }


    [HttpPost("GetSmileIDResults/{timestamp}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSmileIDResults(string timestamp)
    {
        try
        {
            var body = _user.ReadObjectFromFile(timestamp);
            return Ok(body);

        }
        catch (Exception ex)
        {

        }
        return BadRequest();
    }

}
