using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Models;
using DOMAIN.Models.Dtos;

namespace DOMAIN.Interface;
public interface ITransactionRepo
{
    Task<bool> Deposit(TransactionDto transaction, int UserId);
    Task<bool> WithDraw(TransactionDto transaction, int UserId);
    Task<bool> SetReminder(TransactionDto transactionDto, int UserId);
    Task<bool> RunReminder(TransactionDto transactionDto, int UserId);
    Task<List<Transaction>> GetTransaction(int UserId);
    Task<List<Bank?>> GetBanks();
}
