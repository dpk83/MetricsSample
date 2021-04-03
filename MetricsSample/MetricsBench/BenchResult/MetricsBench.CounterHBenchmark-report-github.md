``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Server=True  Toolchain=InProcessEmitToolchain  IterationCount=3  
LaunchCount=1  WarmupCount=3  

```
|                                    Method |       Mean |       Error |    StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-----------:|------------:|----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                         Add_NullDimension |   7.078 ns |   1.7398 ns | 0.0954 ns |   6.987 ns |   7.177 ns |      - |     - |     - |         - |
|                    Add_NoDimensionChanges |   7.308 ns |   0.5629 ns | 0.0309 ns |   7.272 ns |   7.326 ns |      - |     - |     - |         - |
|   Add_Update1DimValueInEachCall_5DCounter | 130.520 ns |   3.7523 ns | 0.2057 ns | 130.347 ns | 130.748 ns | 0.0100 |     - |     - |      64 B |
|   Add_Update3DimValueInEachCall_5DCounter | 238.546 ns |  27.7053 ns | 1.5186 ns | 236.870 ns | 239.830 ns | 0.0100 |     - |     - |      64 B |
|  Add_Update3DimValueInEachCall_10DCounter | 272.515 ns |  46.5206 ns | 2.5499 ns | 270.311 ns | 275.308 ns | 0.0100 |     - |     - |      64 B |
|  Add_Update5DimValueInEachCall_10DCounter | 390.543 ns |  73.1680 ns | 4.0106 ns | 385.912 ns | 392.861 ns | 0.0100 |     - |     - |      64 B |
| Add_Update10DimValueInEachCall_10DCounter | 741.667 ns | 100.7750 ns | 5.5238 ns | 738.429 ns | 748.045 ns | 0.0095 |     - |     - |      64 B |
