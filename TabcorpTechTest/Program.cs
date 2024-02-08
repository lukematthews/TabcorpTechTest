using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
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
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
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
            new Customer() { Id = 10001, FirstName = "Tony", LastName = "Stark", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.Australia },
            new Customer() { Id = 10002, FirstName = "Bruce", LastName = "Banner", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.US },
            new Customer() { Id = 10003, FirstName = "Steve", LastName = "Rogers", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.Australia },
            new Customer() { Id = 10004, FirstName = "Wanda", LastName = "Maximoff", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.US },
            new Customer() { Id = 10005, FirstName = "Natasha", LastName = "Romanoff", Age = "tony.stark@gmail.com", Location = TabcorpTechTest.Constants.Location.Canada },
        ]);

        db.Products.AddRange([
            new Product() { ProductCode = "PRODUCT_001", Cost = 50, Status = TabcorpTechTest.Constants.ProductStatus.Active },
            new Product() { ProductCode = "PRODUCT_002", Cost = 100, Status = TabcorpTechTest.Constants.ProductStatus.Inactive },
            new Product() { ProductCode = "PRODUCT_003", Cost = 200, Status = TabcorpTechTest.Constants.ProductStatus.Active },
            new Product() { ProductCode = "PRODUCT_004", Cost = 10, Status = TabcorpTechTest.Constants.ProductStatus.Inactive },
            new Product() { ProductCode = "PRODUCT_005", Cost = 500, Status = TabcorpTechTest.Constants.ProductStatus.Active },
        ]);
        db.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapHub<TransactionHub>("/transactionHub");

app.Run();