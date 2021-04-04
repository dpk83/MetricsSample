``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-HRVWDO : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                    Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                         Add_NullDimension |   6.219 ns |  0.1657 ns |  0.3937 ns |   5.561 ns |   7.173 ns |      - |     - |     - |         - |
|                    Add_NoDimensionChanges |   6.920 ns |  0.1746 ns |  0.1548 ns |   6.765 ns |   7.287 ns |      - |     - |     - |         - |
|   Add_Update1DimValueInEachCall_5DCounter |  99.745 ns |  1.7290 ns |  1.9911 ns |  96.597 ns | 104.147 ns | 0.0002 |     - |     - |      24 B |
|   Add_Update3DimValueInEachCall_5DCounter | 197.307 ns |  3.1239 ns |  2.7692 ns | 192.367 ns | 202.116 ns | 0.0002 |     - |     - |      24 B |
|  Add_Update3DimValueInEachCall_10DCounter | 233.910 ns |  4.5323 ns |  6.2039 ns | 223.238 ns | 246.022 ns |      - |     - |     - |      24 B |
|  Add_Update5DimValueInEachCall_10DCounter | 353.487 ns |  6.9870 ns | 10.2414 ns | 337.305 ns | 369.245 ns |      - |     - |     - |      24 B |
| Add_Update10DimValueInEachCall_10DCounter | 709.169 ns | 14.1040 ns | 23.1733 ns | 675.972 ns | 757.615 ns |      - |     - |     - |      24 B |
