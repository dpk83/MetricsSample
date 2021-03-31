``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-YNWIYQ : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |   1.958 ns |  0.0259 ns |  0.0230 ns |   1.933 ns |   2.004 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   6.499 ns |  0.1196 ns |  0.1119 ns |   6.352 ns |   6.752 ns |      - |     - |     - |         - |
| Add_AlternateDimensionsBetweenCalls_5DCounter |  12.070 ns |  0.1288 ns |  0.1142 ns |  11.947 ns |  12.340 ns |      - |     - |     - |         - |
|       Add_Update1DimValueInEachCall_5DCounter | 133.174 ns |  1.5382 ns |  1.3636 ns | 131.698 ns | 136.284 ns | 0.0012 |     - |     - |     112 B |
|       Add_Update3DimValueInEachCall_5DCounter | 247.013 ns |  4.8881 ns |  6.3559 ns | 237.263 ns | 259.356 ns | 0.0024 |     - |     - |     216 B |
|      Add_Update3DimValueInEachCall_10DCounter | 376.217 ns |  7.5577 ns |  8.4003 ns | 364.584 ns | 394.305 ns | 0.0029 |     - |     - |     264 B |
|      Add_Update5DimValueInEachCall_10DCounter | 489.918 ns |  9.0157 ns |  8.4333 ns | 478.030 ns | 509.480 ns | 0.0043 |     - |     - |     368 B |
|      Add_Update5DimValueInEachCall_20DCounter | 735.022 ns |  7.6993 ns |  7.2020 ns | 721.234 ns | 747.792 ns | 0.0048 |     - |     - |     440 B |
|     Add_NoDimensionChanges_NewDimensionObject | 866.785 ns | 15.5553 ns | 13.7894 ns | 843.592 ns | 897.220 ns | 0.0095 |     - |     - |     768 B |
