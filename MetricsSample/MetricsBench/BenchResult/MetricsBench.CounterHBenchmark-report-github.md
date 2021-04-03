``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-PDOZAV : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                                Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------ |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                                     Add_NullDimension |   2.296 ns |  0.0783 ns |  0.0838 ns |   2.192 ns |   2.496 ns |      - |     - |     - |         - |
|                                Add_NoDimensionChanges |   2.318 ns |  0.0545 ns |  0.0484 ns |   2.274 ns |   2.436 ns |      - |     - |     - |         - |
| Add_AlternateUnsortedDimensionsBetweenCalls_5DCounter |   3.262 ns |  0.0476 ns |  0.0446 ns |   3.175 ns |   3.364 ns |      - |     - |     - |         - |
|               Add_Update1DimValueInEachCall_5DCounter |  79.445 ns |  1.4808 ns |  1.3851 ns |  76.586 ns |  82.042 ns | 0.0005 |     - |     - |      40 B |
|               Add_Update3DimValueInEachCall_5DCounter | 242.810 ns |  4.8374 ns |  6.1178 ns | 235.269 ns | 257.164 ns | 0.0014 |     - |     - |     120 B |
|              Add_Update3DimValueInEachCall_10DCounter | 281.509 ns |  5.3213 ns |  5.2262 ns | 270.853 ns | 288.623 ns | 0.0014 |     - |     - |     120 B |
|              Add_Update5DimValueInEachCall_10DCounter | 471.825 ns |  9.1513 ns |  8.5602 ns | 451.503 ns | 483.368 ns | 0.0024 |     - |     - |     200 B |
|              Add_Update5DimValueInEachCall_20DCounter | 561.665 ns |  9.4207 ns |  8.8121 ns | 549.493 ns | 576.598 ns | 0.0019 |     - |     - |     200 B |
|             Add_NoDimensionChanges_NewDimensionObject | 863.131 ns | 16.4157 ns | 14.5521 ns | 837.403 ns | 885.944 ns | 0.0067 |     - |     - |     552 B |
