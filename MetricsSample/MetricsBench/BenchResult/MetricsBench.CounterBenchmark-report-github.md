``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-TFDEWI : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |   2.055 ns |  0.0558 ns |  0.0522 ns |   2.006 ns |   2.188 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   6.205 ns |  0.0676 ns |  0.0632 ns |   6.092 ns |   6.305 ns |      - |     - |     - |         - |
| Add_AlternateDimensionsBetweenCalls_5DCounter |  12.148 ns |  0.1046 ns |  0.0927 ns |  12.039 ns |  12.356 ns |      - |     - |     - |         - |
|       Add_Update1DimValueInEachCall_5DCounter | 140.568 ns |  2.5357 ns |  3.3851 ns | 135.321 ns | 148.494 ns | 0.0012 |     - |     - |     112 B |
|       Add_Update3DimValueInEachCall_5DCounter | 246.852 ns |  4.8292 ns |  6.4468 ns | 235.451 ns | 260.152 ns | 0.0024 |     - |     - |     216 B |
|      Add_Update3DimValueInEachCall_10DCounter | 380.734 ns |  7.5678 ns | 11.7821 ns | 361.913 ns | 410.724 ns | 0.0033 |     - |     - |     264 B |
|      Add_Update5DimValueInEachCall_10DCounter | 484.246 ns |  9.6288 ns | 13.8093 ns | 458.749 ns | 512.908 ns | 0.0038 |     - |     - |     368 B |
|      Add_Update5DimValueInEachCall_20DCounter | 724.577 ns |  9.7438 ns |  8.6376 ns | 709.730 ns | 741.732 ns | 0.0048 |     - |     - |     440 B |
|     Add_NoDimensionChanges_NewDimensionObject | 900.694 ns | 17.6260 ns | 20.9826 ns | 871.291 ns | 929.411 ns | 0.0095 |     - |     - |     768 B |
