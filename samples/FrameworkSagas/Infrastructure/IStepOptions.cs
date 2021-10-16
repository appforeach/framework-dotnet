using System;

namespace FrameworkSagas.Infrastructure
{
    public interface IStepOptions<TState>
    {
        IStepOptions<TState> If(Func<TState, bool> condition);

        IStepOptions<TState> Send<TStepInput>(Func<TState, TStepInput> messageFactory);

        IStepOptions<TState> Save<TStepOutput>(Action<TState, TStepOutput> saveResult);
    }
}
