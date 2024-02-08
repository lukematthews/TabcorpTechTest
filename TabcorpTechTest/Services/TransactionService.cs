using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;

namespace TabcorpTechTest.Services
{
    public class TransactionService(ApiContext context, IConfiguration configuration) : ITransactionService
    {
        private readonly ApiContext _context = context;
        private readonly IConfiguration Configuration = configuration;

        public Transaction ToTransaction(TransactionDto transactionDto)
        {
            // throw exceptions for a 400 if customer or product not found.
            //Customer customer = _context.Customers.Where(x => x.CustomerID == transactionDto.CustomerId).First();

            Customer customer = _context.Customers
                .Where(x => x.CustomerID == transactionDto.CustomerId)
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
                CustomerId = customer,
                ProductCode = product,
                Quantity = transactionDto.Quantity,
                TransactionTime = transactionTime,
            };
        }

        public TransactionDto ToTransactionDto(Transaction transaction) => new()
        {
            CustomerId = transaction.CustomerId.CustomerID,
            ProductCode = transaction.ProductCode.ProductCode,
            Quantity = transaction.Quantity,
            TransactionTime = transaction.TransactionTime.ToString(Configuration["TransactionTimeFormat"]),
        };

        public IEnumerable<TransactionDto> GetAllTransactions()
        {
            Console.WriteLine($"Total transaction count: {context.Transactions.Count()}");
            return _context.Transactions
                .Include(t => t.CustomerId)
                .Include(t => t.ProductCode)
                .Select(t => ToTransactionDto(t)).ToList();
        }

        public void SaveTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public List<ValidationResult> ValidateTransaction(Transaction transaction)
        {
            var validationResults = new List<ValidationResult>() {
                ValidatePrice(transaction),
                ValidateProductStatus(transaction),
                ValidateTransactionDate(transaction) };
            validationResults.RemoveAll(vr => vr == ValidationResult.Success);
            return validationResults;
        }

        private ValidationResult ValidateTransactionDate(Transaction transaction)
        {
            var elapsed = (DateTime.Now - transaction.TransactionTime).TotalMinutes;
            return elapsed > Configuration.GetValue<int>("Validation:PastMinutes")
                ? new ValidationResult("Transaction time is invalid")
                : ValidationResult.Success;
        }

        private ValidationResult ValidateProductStatus(Transaction transaction)
        {
            if (transaction.ProductCode.Status == Constants.ProductStatus.Inactive)
            {
                return new ValidationResult("Product state is invalid");
            }
            return ValidationResult.Success;
        }

        private ValidationResult ValidatePrice(Transaction transaction)
        {
            if (transaction.Quantity > 0 && (transaction.Quantity * transaction.ProductCode.Cost) > Configuration.GetValue<int>("Validation:MaxValue"))
            {
                return new ValidationResult("Transaction total cost invalid");
            }
            return ValidationResult.Success;
        }
    }
}
