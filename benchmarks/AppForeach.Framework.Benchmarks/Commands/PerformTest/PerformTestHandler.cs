namespace AppForeach.Framework.Benchmarks.Commands.PerformTest;

public class PerformTestHandler
{
    public Task<PerformTestResult> Handle(PerformTestCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new PerformTestResult());
    }
}
