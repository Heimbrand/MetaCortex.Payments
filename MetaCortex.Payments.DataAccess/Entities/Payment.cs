namespace MetaCortex.Payments.DataAccess.Entities;

public class Payment : BaseDocument
{
    public string OrderId { get; set; } = default!;
    public string? PaymentMethod { get; set; }
    public string? Status { get; set; }
}