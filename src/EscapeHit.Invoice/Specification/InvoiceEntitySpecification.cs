﻿using AppForeach.Framework.DataType;

namespace EscapeHit.Invoice.Specification
{
    public class InvoiceEntitySpecification : BaseEntitySpecification<InvoiceEntity>
    {
        public InvoiceEntitySpecification()
        {
            Field(e => e.CustomerNumber).IsRequired().HasMaxLength(5);

            Field(e => e.Number).IsRequired();//.Is<NumberDataType>(); Is Is not ImplementedYet
        }
    }
}
