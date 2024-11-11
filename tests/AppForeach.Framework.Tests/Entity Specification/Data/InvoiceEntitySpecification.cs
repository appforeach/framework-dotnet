using AppForeach.Framework.DataType;

namespace AppForeach.Framework.Tests.Entity_Specification.Data;

internal class InvoiceEntitySpecification : BaseEntitySpecification<InvoiceEntity>
{
    public InvoiceEntitySpecification()
    {
        Field(e => e.CustomerNumber).IsRequired().MaxLength(10);
    }
}
