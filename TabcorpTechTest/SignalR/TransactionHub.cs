using Microsoft.AspNetCore.SignalR;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;
using TabcorpTechTest.Services;

namespace TabcorpTechTest.SignalR
{
    public class TransactionHub : Hub
    {
        private readonly ITransactionService transactionService;

        public TransactionHub(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }
        public async Task SendMessage(string user, TransactionDto transactionDto)
        {
            Transaction transaction = transactionService.ToTransaction(transactionDto);
            transactionService.SaveTransaction(transaction);
            await Clients.All.SendAsync("ReceivedTransaction", user, "ok");
        }
    }
}
