using AutoMapper;
using EscapeHit.Invoice.Commands.CreateInvoice;
using EscapeHit.Invoice.Queries.GetInvoiceById;

namespace EscapeHit.Invoice.Tests;

public class AutoaMapperTests
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateInvoiceMappingProfile>();
            cfg.AddProfile<GetInvoiceByIdMappingProfile>();
        });

        config.AssertConfigurationIsValid();
    }
}