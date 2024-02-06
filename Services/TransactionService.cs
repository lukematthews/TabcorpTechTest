using Microsoft.EntityFrameworkCore;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;

namespace TabcorpTechTest.Services
{
    public class TransactionService(ApiContext context) : ITransactionService
    {
        private readonly ApiContext _context = context;

        public Transaction ToTransaction(TransactionDto transactionDto)
        {
            // throw exceptions for a 400 if customer or product not found.
            Customer customer = _context.Customers.Where(x => x.CustomerID == transactionDto.CustomerId).First();
            Product product = _context.Products.Where(x => x.ProductCode == transactionDto.ProductCode).First();
            DateTime transactionTime = DateTime.Parse(transactionDto.TransactionTime);

            return new Transaction {
                CustomerId = customer,
                ProductCode = product,
                Quantity = transactionDto.Quantity,
                TransactionTime = transactionTime,
            };
        }

        public TransactionDto ToTransactionDto(Transaction transaction) => new TransactionDto
        {
            CustomerId = transaction.CustomerId.CustomerID,
            ProductCode = transaction.ProductCode.ProductCode,
            Quantity = transaction.Quantity,
            TransactionTime = String.Format("{0:yyyy/MM/dd HH:mm:ss}", transaction.TransactionTime),
        };

        public IEnumerable<TransactionDto> GetAllTransactions()
        {
            return _context.Transactions
                .Include(t => t.CustomerId)
                .Include(t => t.ProductCode)
                .Select(t => ToTransactionDto(t)).ToList();
        }

        public async void SaveTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
