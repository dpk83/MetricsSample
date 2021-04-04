``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-HRVWDO : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                    Method |      Mean |     Error |    StdDev |       Min |        Max | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |----------:|----------:|----------:|----------:|-----------:|------:|------:|------:|----------:|
|                         Add_NullDimension |  3.992 ns | 0.1168 ns | 0.1818 ns |  3.623 ns |   4.363 ns |     - |     - |     - |         - |
|                    Add_NoDimensionChanges |  4.165 ns | 0.1232 ns | 0.2545 ns |  3.703 ns |   4.672 ns |     - |     - |     - |         - |
|   Add_Update1DimValueInEachCall_5DCounter | 15.869 ns | 0.3560 ns | 0.6419 ns | 14.648 ns |  17.134 ns |     - |     - |     - |         - |
|   Add_Update3DimValueInEachCall_5DCounter | 34.939 ns | 0.7404 ns | 1.2371 ns | 32.421 ns |  37.359 ns |     - |     - |     - |         - |
|  Add_Update3DimValueInEachCall_10DCounter | 35.027 ns | 0.7401 ns | 1.3155 ns | 32.467 ns |  38.051 ns |     - |     - |     - |         - |
|  Add_Update5DimValueInEachCall_10DCounter | 54.655 ns | 1.1265 ns | 2.1971 ns | 49.827 ns |  59.447 ns |     - |     - |     - |         - |
| Add_Update10DimValueInEachCall_10DCounter | 98.901 ns | 1.9439 ns | 2.3873 ns | 93.051 ns | 103.384 ns |     - |     - |     - |         - |
