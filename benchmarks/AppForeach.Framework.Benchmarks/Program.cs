using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher
    .FromAssembly(typeof(Program).Assembly)
    .Run(args
#if DEBUG
    , new DebugInProcessConfig()
#endif
    );