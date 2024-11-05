namespace EscapeHit.Invoice.Queries.GetInvoiceById
{

    public interface IGetInvoiceByIdResultMapping
    {
        GetInvoiceByIdResult MapFrom(InvoiceEntity entity);
    }

    public class GetInvoiceByIdResultMapping : IGetInvoiceByIdResultMapping
    {
        public GetInvoiceByIdResult MapFrom(InvoiceEntity entity)
        {
            var output = new GetInvoiceByIdResult
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Number = entity.Number,
                CustomerNumber = entity.CustomerNumber
            };

            return output;
        }
    }
}
