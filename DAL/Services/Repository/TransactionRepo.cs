using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DOMAIN.Interface;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services.Repository;
public class TransactionRepo : ITransactionRepo
{
    private readonly AppDbContext _appDbContext;

    public TransactionRepo(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> Deposit(Transaction transaction, int UserId)
    {
        try
        {
            transaction.TransDate = DateTime.Now;
            transaction.Status = "Complete";
            transaction.Type = "Deposit";
            transaction.UserId = UserId;
            await _appDbContext.Transactions.AddAsync(transaction);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<List<Transaction>> GetTransaction(Transaction transaction, int UserId)
    {
        try
        {
            var result = await _appDbContext.Transactions
                .OrderBy(x => x.TransDate)
                .Where(x => x.UserId == UserId).ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    Task<bool> ITransactionRepo.SetReminder(Transaction transaction, int UserId) => throw new NotImplementedException();
    public async Task<bool> WithDraw(Transaction transaction, int UserId)
    {
        try
        {
            transaction.TransDate = DateTime.Now;
            transaction.Status = "Complete";
            transaction.Type = "Withdraw";
            transaction.UserId = UserId;
            await _appDbContext.Transactions.AddAsync(transaction);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
