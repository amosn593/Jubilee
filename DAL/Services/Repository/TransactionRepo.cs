using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DOMAIN.Interface;
using DOMAIN.Models;

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
    Task<List<Transaction>> ITransactionRepo.GetTransaction(Transaction transaction, int UserId) => throw new NotImplementedException();
    Task<bool> ITransactionRepo.SetReminder(Transaction transaction, int UserId) => throw new NotImplementedException();
    Task<bool> ITransactionRepo.WithDraw(Transaction transaction, int UserId) => throw new NotImplementedException();
}
