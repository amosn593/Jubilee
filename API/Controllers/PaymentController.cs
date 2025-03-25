using DOMAIN.Interface;
using DOMAIN.Models;
using DOMAIN.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly ITransactionRepo _transaction;

    public PaymentController(ITransactionRepo transaction)
    {
        _transaction = transaction;
    }

    // GET: api/<PaymentController>
    [HttpPost("Deposit/{UserId}")]
    public async Task<IActionResult> Deposit([FromBody] TransactionDto transaction, int UserId)
    {
        try
        {
            var Results = await _transaction.Deposit(transaction, UserId);
            return Ok(Results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/<PaymentController>/5
    [HttpPost("WithDraw/{UserId}")]
    public async Task<IActionResult> WithDraw([FromBody] WithdrawDto transaction, int UserId)
    {
        try
        {
            var Results = await _transaction.WithDraw(transaction, UserId);
            return Ok(Results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST api/<PaymentController>
    [HttpGet("GetTransactions/{UserId}")]
    public async Task<IActionResult> GetTransactions(int UserId)
    {
        try
        {
            var Results = await _transaction.GetTransaction(UserId);
            return Ok(Results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAllTransactions")]
    public async Task<IActionResult> GetAllTransactions()
    {
        try
        {
            var Results = await _transaction.GetAllTransaction();
            return Ok(Results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetBanks")]
    public async Task<IActionResult> GetBanks()
    {
        try
        {
            var Results = await _transaction.GetBanks();
            return Ok(Results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT api/<PaymentController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    // DELETE api/<PaymentController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
