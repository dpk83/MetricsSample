``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-PDOZAV : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |   1.977 ns |  0.0402 ns |  0.0376 ns |   1.919 ns |   2.063 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   6.219 ns |  0.1264 ns |  0.1182 ns |   6.062 ns |   6.469 ns |      - |     - |     - |         - |
| Add_AlternateDimensionsBetweenCalls_5DCounter |  12.372 ns |  0.0776 ns |  0.0648 ns |  12.302 ns |  12.533 ns |      - |     - |     - |         - |
|       Add_Update1DimValueInEachCall_5DCounter | 137.584 ns |  2.5413 ns |  2.3771 ns | 134.895 ns | 141.976 ns | 0.0012 |     - |     - |     112 B |
|       Add_Update3DimValueInEachCall_5DCounter | 250.218 ns |  4.9492 ns |  8.6681 ns | 234.249 ns | 267.436 ns | 0.0024 |     - |     - |     216 B |
|      Add_Update3DimValueInEachCall_10DCounter | 377.851 ns |  6.2277 ns |  5.8254 ns | 365.734 ns | 389.192 ns | 0.0033 |     - |     - |     264 B |
|      Add_Update5DimValueInEachCall_10DCounter | 480.668 ns |  5.5283 ns |  4.6164 ns | 476.215 ns | 491.280 ns | 0.0043 |     - |     - |     368 B |
|      Add_Update5DimValueInEachCall_20DCounter | 762.005 ns | 11.9081 ns |  9.9438 ns | 745.851 ns | 776.402 ns | 0.0048 |     - |     - |     440 B |
|     Add_NoDimensionChanges_NewDimensionObject | 875.716 ns | 17.3501 ns | 15.3804 ns | 852.707 ns | 896.924 ns | 0.0095 |     - |     - |     768 B |
