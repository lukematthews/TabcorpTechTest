using System.ComponentModel.DataAnnotations;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;

namespace TabcorpTechTest.Services
{
    public interface ITransactionService
    {
        public Transaction ToTransaction(TransactionDto transactionDto);
        public TransactionDto ToTransactionDto(Transaction transaction);

        public IEnumerable<TransactionDto> GetAllTransactions();

        public void SaveTransaction(Transaction transaction);
        List<ValidationResult> ValidateTransaction(Transaction transaction);
    }
}
