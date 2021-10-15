using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas
{
    public interface IHasSagaInfrastructureProvider
    {
        ISagaInfrastructureProvider InfrastructureProvider { get; }
    }
}
