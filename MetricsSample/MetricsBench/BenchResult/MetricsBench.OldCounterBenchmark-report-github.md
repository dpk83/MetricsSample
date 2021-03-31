``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-YNWIYQ : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |         Mean |      Error |     StdDev |       Median |          Min |          Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-------------:|-----------:|-----------:|-------------:|-------------:|-------------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |     1.970 ns |  0.0168 ns |  0.0140 ns |     1.969 ns |     1.949 ns |     2.000 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   533.515 ns |  6.6799 ns |  6.2484 ns |   535.115 ns |   524.261 ns |   544.398 ns | 0.0010 |     - |     - |      96 B |
| Add_AlternateDimensionsBetweenCalls_5DCounter |   208.850 ns |  3.2284 ns |  2.6958 ns |   209.518 ns |   205.248 ns |   214.668 ns | 0.0012 |     - |     - |      96 B |
|       Add_Update1DimValueInEachCall_5DCounter |   280.133 ns |  5.5954 ns |  5.7461 ns |   279.421 ns |   271.489 ns |   292.624 ns | 0.0029 |     - |     - |     256 B |
|       Add_Update3DimValueInEachCall_5DCounter |   497.355 ns |  9.1052 ns |  7.6033 ns |   495.894 ns |   489.577 ns |   513.451 ns | 0.0038 |     - |     - |     336 B |
|      Add_Update3DimValueInEachCall_10DCounter | 1,175.372 ns | 27.9880 ns | 82.5232 ns | 1,145.878 ns | 1,081.511 ns | 1,379.708 ns | 0.0038 |     - |     - |     336 B |
|      Add_Update5DimValueInEachCall_10DCounter | 1,231.396 ns | 12.9752 ns | 10.8349 ns | 1,231.791 ns | 1,215.706 ns | 1,251.861 ns | 0.0057 |     - |     - |     568 B |
|      Add_Update5DimValueInEachCall_20DCounter | 2,019.326 ns | 23.3406 ns | 19.4904 ns | 2,019.314 ns | 1,988.731 ns | 2,057.450 ns | 0.0038 |     - |     - |     568 B |
|       Add_NoDimensionChanges_NewDimensionList |   660.704 ns |  3.7455 ns |  3.1277 ns |   660.366 ns |   656.207 ns |   665.947 ns | 0.0038 |     - |     - |     368 B |
