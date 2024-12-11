﻿using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;
using MetaCortex.Payments.DataAccess.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace MetaCortex.Payments.API.Extensions;

public static class ProcessedOrderEndpointExtensions
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
    public static async Task<IResult> GetAllPaymentsAsync([FromServices] IProcessedPaymentHistoryRepository repo)
    {
        try
        {
            var payments = await repo.GetAllAsync();


            return Results.Ok(payments);

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occured: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }
    public static async Task<IResult> GetPaymentByIdAsync([FromServices] IProcessedPaymentHistoryRepository repo, string id)
    {
        try
        {
            var payment = await repo.GetByOrderIdAsync(id);
            return Results.Ok(payment);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occured: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }
    public static async Task<IResult> AddPaymentAsync([FromServices] IProcessedPaymentHistoryRepository repo, [FromBody] PaymentHistory payment)
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
    public static async Task<IResult> UpdatePaymentAsync([FromServices] IProcessedPaymentHistoryRepository repo, [FromBody] PaymentHistory payment)
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
    public static async Task<IResult> DeletePaymentAsync([FromServices] IProcessedPaymentHistoryRepository repo, string id)
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