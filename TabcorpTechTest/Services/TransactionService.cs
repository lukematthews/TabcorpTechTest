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
            Customer customer = _context.Customers
                .Where(x => x.Id == transactionDto.CustomerID)
                .FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException();
            }
            Product product = _context.Products
                .Where(x => x.ProductCode == transactionDto.ProductCode)
                .FirstOrDefault();
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            DateTime transactionTime = DateTime.Parse(transactionDto.TransactionTime);

            return new Transaction
            {
                Customer = customer,
                Product = product,
                Quantity = transactionDto.Quantity,
                TransactionTime = transactionTime,
            };
        }

        public TransactionDto ToTransactionDto(Transaction transaction) => new TransactionDto
        {
            CustomerID = transaction.Customer.Id,
            ProductCode = transaction.Product.ProductCode,
            Quantity = transaction.Quantity,
            TransactionTime = String.Format("{0:yyyy/MM/dd HH:mm:ss}", transaction.TransactionTime),
        };

        public IEnumerable<TransactionDto> GetAllTransactions()
        {
            Console.WriteLine($"Total transaction count: {context.Transactions.Count()}");
            return _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Product)
                .Select(t => ToTransactionDto(t)).ToList();
        }

        public void SaveTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
