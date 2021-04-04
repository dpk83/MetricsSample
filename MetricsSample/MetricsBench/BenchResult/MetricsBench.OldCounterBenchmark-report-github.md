``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-HRVWDO : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                    Method |         Mean |      Error |     StdDev |          Min |          Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-------------:|-----------:|-----------:|-------------:|-------------:|-------:|------:|------:|----------:|
|                         Add_NullDimension |     7.597 ns |  0.1892 ns |  0.3950 ns |     6.639 ns |     8.406 ns |      - |     - |     - |         - |
|                    Add_NoDimensionChanges |   609.011 ns | 12.2189 ns | 29.7424 ns |   536.867 ns |   662.659 ns | 0.0010 |     - |     - |     120 B |
|   Add_Update1DimValueInEachCall_5DCounter |   302.108 ns |  6.3343 ns | 18.5774 ns |   263.622 ns |   349.759 ns | 0.0029 |     - |     - |     240 B |
|   Add_Update3DimValueInEachCall_5DCounter |   487.490 ns |  9.7854 ns | 18.6177 ns |   455.283 ns |   531.646 ns | 0.0029 |     - |     - |     240 B |
|  Add_Update3DimValueInEachCall_10DCounter | 1,115.538 ns | 22.3057 ns | 39.0665 ns | 1,044.995 ns | 1,204.716 ns | 0.0019 |     - |     - |     240 B |
|  Add_Update5DimValueInEachCall_10DCounter | 1,231.782 ns | 24.5319 ns | 61.9953 ns | 1,109.451 ns | 1,368.731 ns | 0.0038 |     - |     - |     392 B |
| Add_Update10DimValueInEachCall_10DCounter | 1,366.006 ns | 27.3448 ns | 57.0788 ns | 1,252.119 ns | 1,487.631 ns | 0.0076 |     - |     - |     672 B |
