namespace MetaCortex.Payments.DataAccess.Entities;

public class PaymentHistory : BaseDocument
{
    public string OrderId { get; set; } = default!;
    public string? PaymentMethod { get; set; }
    public bool? IsPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
    public List<Products>? Products { get; set; }

}