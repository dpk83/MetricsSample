``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-PDOZAV : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |         Mean |      Error |     StdDev |          Min |          Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-------------:|-----------:|-----------:|-------------:|-------------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |     1.961 ns |  0.0261 ns |  0.0231 ns |     1.930 ns |     2.007 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   533.736 ns |  8.1058 ns |  7.5821 ns |   522.515 ns |   547.371 ns | 0.0010 |     - |     - |      96 B |
| Add_AlternateDimensionsBetweenCalls_5DCounter |   203.255 ns |  2.4446 ns |  2.2867 ns |   200.525 ns |   208.033 ns | 0.0012 |     - |     - |      96 B |
|       Add_Update1DimValueInEachCall_5DCounter |   274.754 ns |  3.1023 ns |  2.9019 ns |   269.379 ns |   279.709 ns | 0.0029 |     - |     - |     256 B |
|       Add_Update3DimValueInEachCall_5DCounter |   505.473 ns |  8.0663 ns |  7.1505 ns |   492.116 ns |   516.384 ns | 0.0038 |     - |     - |     336 B |
|      Add_Update3DimValueInEachCall_10DCounter | 1,110.981 ns | 15.7047 ns | 13.9218 ns | 1,094.639 ns | 1,135.868 ns | 0.0038 |     - |     - |     336 B |
|      Add_Update5DimValueInEachCall_10DCounter | 1,233.738 ns | 21.7783 ns | 18.1859 ns | 1,211.230 ns | 1,272.081 ns | 0.0057 |     - |     - |     568 B |
|      Add_Update5DimValueInEachCall_20DCounter | 1,990.789 ns | 32.2297 ns | 30.1477 ns | 1,947.796 ns | 2,042.917 ns | 0.0038 |     - |     - |     568 B |
|       Add_NoDimensionChanges_NewDimensionList |   670.857 ns |  8.4137 ns |  7.8702 ns |   659.780 ns |   687.297 ns | 0.0038 |     - |     - |     368 B |
