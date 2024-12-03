using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;
using MetaCortex.Payments.DataAccess.RabbitMq;
using MetaCortex.Payments.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace MetaCortex.Payments.API.Extensions;

public static class PaymentEndpointExtensions
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/payments");

        group.MapGet("", GetAllPaymentsAsync);
        group.MapGet("{id}", GetPaymentByIdAsync);
        group.MapPost("", AddPaymentAsync);
        group.MapPut("", UpdatePaymentAsync);
        group.MapDelete("{id}", DeletePaymentAsync);

        return app;
    }
    #region Methods
    public static async Task<IResult> GetAllPaymentsAsync([FromServices] IPaymentRepository repo, IMessageProducerService messageProducer)
    {
        try
        {
            var payments = await repo.GetAllAsync();

            foreach (var payment in payments)
            {
                await messageProducer.SendMessageAsync($"Order sent: {payment.OrderId}");
                await messageProducer.SendMessageAsync(payment.PaymentMethod);
                await messageProducer.SendMessageAsync(payment.Status );
            }

            return Results.Ok(payments);

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occured: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }
    public static async Task<IResult> GetPaymentByIdAsync([FromServices] IPaymentRepository repo, string id)
    {
        try
        {
            var payment = await repo.GetByIdAsync(id);
            return Results.Ok(payment);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occured: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }
    public static async Task<IResult> AddPaymentAsync([FromServices] IPaymentRepository repo, [FromBody] Payment payment)
    {
        try
        {
            await repo.AddAsync(payment);
            return Results.Created($"/api/payments/{payment.Id}", payment);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occured: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }
    public static async Task<IResult> UpdatePaymentAsync([FromServices] IPaymentRepository repo, [FromBody] Payment payment)
    {
        try
        {
            await repo.UpdateAsync(payment);
            return Results.Ok($"Entity: {payment} has been updated");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occured: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }
    public static async Task<IResult> DeletePaymentAsync([FromServices] IPaymentRepository repo, string id)
    {
        try
        {
            await repo.DeleteAsync(id);
            return Results.Ok($"Entity with the id: {id} has been deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occured: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }
    #endregion
}