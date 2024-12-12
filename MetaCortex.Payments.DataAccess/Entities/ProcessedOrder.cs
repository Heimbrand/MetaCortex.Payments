namespace MetaCortex.Payments.DataAccess.Entities;

public class ProcessedOrder : BaseDocument
{
    public DateTime OrderDate { get; set; }
    public string CustomerId { get; set; }
    public string PaymentMethod { get; set; }
    public bool VIPStatus { get; set; }
    public List<Products> Products { get; set; }
    public Payment? PaymentPlan { get; set; }
}