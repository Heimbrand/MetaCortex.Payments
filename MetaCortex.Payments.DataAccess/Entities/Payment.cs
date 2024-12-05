namespace MetaCortex.Payments.DataAccess.Entities;

public class Payment 
{
    public string? OrderId { get; set; } = default!;
    public string? PaymentMethod { get; set; }
    public bool IsPaid { get; set; }
}