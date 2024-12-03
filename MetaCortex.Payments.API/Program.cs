using MetaCortex.Payments.API.Extensions;
using MetaCortex.Payments.DataAccess;
using MetaCortex.Payments.DataAccess.Interfaces;
using MetaCortex.Payments.DataAccess.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>( serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient($"mongodb://{settings.Host}:{settings.Port}");
});
builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();



var app = builder.Build();



app.UseHttpsRedirection();

app.MapPaymentEndpoints();


app.Run();


