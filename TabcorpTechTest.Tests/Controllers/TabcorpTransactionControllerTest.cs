using Moq;
using TabcorpTechnicalTest.Controllers;
using TabcorpTechTest.Constants;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;
using TabcorpTechTest.Services;

namespace TabcorpTechTest.Tests.Controllers;

public class TabcorpTransactionControllerTest
{
    [Fact]
    public void TestGetShouldCallGetAllTransactions()
    {
        var mock = new Mock<ITransactionService>();
        mock.Setup(m => m.GetAllTransactions()).Verifiable();

        TabcorpTransactionController controller = new TabcorpTransactionController(mock.Object);
        controller.Get();
        mock.Verify(m => m.GetAllTransactions());
    }

    [Fact]
    public void TestGetShouldReturnTransactions()
    {
        var mock = new Mock<ITransactionService>();
        var transactions = new TransactionDto[] {
            new() { CustomerId = 1, ProductCode = "2", Quantity = 1, TransactionTime = DateTime.Now.ToString() },
            new() { CustomerId = 2, ProductCode = "3", Quantity = 2, TransactionTime = DateTime.Now.ToString() },
            new() { CustomerId = 3, ProductCode = "4", Quantity = 3, TransactionTime = DateTime.Now.ToString() }
        };
        mock.Setup(m => m.GetAllTransactions()).Returns(transactions).Verifiable();

        TabcorpTransactionController controller = new TabcorpTransactionController(mock.Object);
        var returnedTransactions = controller.Get();
        Assert.Equal(transactions, returnedTransactions);
        mock.Verify(m => m.GetAllTransactions());
    }

    [Fact]
    public void TestPostShouldConvertToDbModelAndSave()
    {
        var mock = new Mock<ITransactionService>();
        var transactionDto = new TransactionDto() { CustomerId = 1, ProductCode = "1", Quantity = 1, TransactionTime = DateTime.Now.ToString() };
        var transaction = new Transaction()
        {
            CustomerId = new Customer() { FirstName = "Luke", LastName = "Matthews", Age = "43", Location = Location.Australia },
            TransactionTime = DateTime.Now,
            Quantity = 1,
            ProductCode = new Product() { ProductCode = "1", Cost = 1, Status = ProductStatus.Active }
        };
        mock.Setup(m => m.ToTransaction(It.Is<TransactionDto>(t => t == transactionDto))).Returns(transaction).Verifiable();
        mock.Setup(m => m.SaveTransaction(It.IsAny<Transaction>())).Verifiable();
        TabcorpTransactionController controller = new TabcorpTransactionController(mock.Object);

        controller.Post(transactionDto);

        mock.Verify(m => m.ToTransaction(It.Is<TransactionDto>(t => t == transactionDto)), Times.Once());
        mock.Verify(m => m.SaveTransaction(It.Is<Transaction>(t => t == transaction)), Times.Once());
    }

}