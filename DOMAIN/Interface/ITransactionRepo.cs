using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Models;

namespace DOMAIN.Interface;
public interface ITransactionRepo
{
    Task<bool> Deposit(Transaction transaction, int UserId);
    Task<bool> WithDraw(Transaction transaction, int UserId);
    Task<bool> SetReminder(Transaction transaction, int UserId);
    Task<List<Transaction>> GetTransaction(Transaction transaction, int UserId);
}
