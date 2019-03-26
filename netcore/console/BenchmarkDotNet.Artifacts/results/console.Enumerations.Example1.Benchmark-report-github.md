``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.648 (1803/April2018Update/Redstone4)
Intel Core i5-7440HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
Frequency=2742193 Hz, Resolution=364.6716 ns, Timer=TSC
.NET Core SDK=2.2.105
  [Host] : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT
  Core   : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
| Method |     N |     Mean |     Error |    StdDev | Ratio | Rank |
|------- |------ |---------:|----------:|----------:|------:|-----:|
| **Sha256** |  **1000** | **9.124 ns** | **0.2182 ns** | **0.2335 ns** |  **1.00** |    **1** |
|        |       |          |           |           |       |      |
| **Sha256** | **10000** | **9.561 ns** | **0.2206 ns** | **0.2945 ns** |  **1.00** |    **1** |
