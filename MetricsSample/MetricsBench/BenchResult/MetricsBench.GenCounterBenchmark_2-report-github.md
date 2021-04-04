``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-HRVWDO : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                    Method |       Mean |     Error |    StdDev |        Min |        Max | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-----------:|----------:|----------:|-----------:|-----------:|------:|------:|------:|----------:|
|                         Add_NullDimension |   5.182 ns | 0.1402 ns | 0.3643 ns |   4.453 ns |   6.056 ns |     - |     - |     - |         - |
|                    Add_NoDimensionChanges |   4.578 ns | 0.1386 ns | 0.4065 ns |   3.826 ns |   5.655 ns |     - |     - |     - |         - |
|   Add_Update1DimValueInEachCall_5DCounter |  35.226 ns | 0.7340 ns | 1.1852 ns |  33.275 ns |  37.941 ns |     - |     - |     - |         - |
|   Add_Update3DimValueInEachCall_5DCounter |  93.023 ns | 1.8861 ns | 3.3034 ns |  85.765 ns | 100.856 ns |     - |     - |     - |         - |
|  Add_Update3DimValueInEachCall_10DCounter |  67.008 ns | 1.3856 ns | 3.2661 ns |  60.933 ns |  76.514 ns |     - |     - |     - |         - |
|  Add_Update5DimValueInEachCall_10DCounter | 102.294 ns | 2.0826 ns | 3.9117 ns |  94.248 ns | 110.016 ns |     - |     - |     - |         - |
| Add_Update10DimValueInEachCall_10DCounter | 199.548 ns | 4.0240 ns | 4.4727 ns | 190.672 ns | 208.159 ns |     - |     - |     - |         - |
