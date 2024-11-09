using AppForeach.Framework.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForeach.Framework.Tests.Entity_Specification.Data;

public class InvoiceEntitySpecification : BaseEntitySpecification<InvoiceEntity>
{
    public InvoiceEntitySpecification()
    {
        Field(e => e.CustomerNumber).IsRequired().MaxLength(10);

        //Field(e => e.Number).IsRequired().Is<NumberDataType>();
    }
}
