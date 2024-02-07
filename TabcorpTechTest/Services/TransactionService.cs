﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

        public TransactionDto ToTransactionDto(Transaction transaction) => new TransactionDto
        {
            CustomerId = transaction.CustomerId.CustomerID,
            ProductCode = transaction.ProductCode.ProductCode,
            Quantity = transaction.Quantity,
            TransactionTime = String.Format("{0:yyyy/MM/dd HH:mm:ss}", transaction.TransactionTime),
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
            if (elapsed > 60)
            {
                return new ValidationResult("Transaction time is invalid");
            }
            return ValidationResult.Success;
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
            if (transaction.Quantity > 0 && (transaction.Quantity * transaction.ProductCode.Cost) > 5000)
            {
                return new ValidationResult("Transaction total cost invalid");
            }
            return ValidationResult.Success;
        }
    }
}
