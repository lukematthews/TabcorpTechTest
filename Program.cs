using Microsoft.EntityFrameworkCore;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Services;
using TabcorpTechTest.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("TabcorpTechnicalTestDb"));
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApiContext>();
        db.Database.EnsureCreated();
        db.Customers.AddRange(
        [
            new Customer() { CustomerID = 10001, FirstName = "Tony", LastName = "Stark", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.AUSTRALIA },
            new Customer() { CustomerID = 10002, FirstName = "Bruce", LastName = "Banner", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.US },
            new Customer() { CustomerID = 10003, FirstName = "Steve", LastName = "Rogers", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.AUSTRALIA },
            new Customer() { CustomerID = 10004, FirstName = "Wanda", LastName = "Maximoff", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.US },
            new Customer() { CustomerID = 10005, FirstName = "Natasha", LastName = "Romanoff", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.CANADA },
        ]);

        db.Products.AddRange([
            new Product() { ProductCode = "PRODUCT_001", Cost = 50, Status = TabcorpTechTest.Constants.ProductStatus.ACTIVE },
            new Product() { ProductCode = "PRODUCT_002", Cost = 100, Status = TabcorpTechTest.Constants.ProductStatus.INACTIVE },
            new Product() { ProductCode = "PRODUCT_003", Cost = 200, Status = TabcorpTechTest.Constants.ProductStatus.ACTIVE },
            new Product() { ProductCode = "PRODUCT_004", Cost = 10, Status = TabcorpTechTest.Constants.ProductStatus.INACTIVE },
            new Product() { ProductCode = "PRODUCT_005", Cost = 500, Status = TabcorpTechTest.Constants.ProductStatus.ACTIVE },
        ]);
        db.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapHub<TransactionHub>("/transactionHub");

app.Run();