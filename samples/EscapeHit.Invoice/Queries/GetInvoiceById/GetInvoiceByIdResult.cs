namespace EscapeHit.Invoice.Queries.GetInvoiceById
{
    public class GetInvoiceByIdResult
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string CustomerNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
