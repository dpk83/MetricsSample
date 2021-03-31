``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-TFDEWI : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |         Mean |      Error |     StdDev |          Min |          Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-------------:|-----------:|-----------:|-------------:|-------------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |     2.010 ns |  0.0500 ns |  0.0417 ns |     1.956 ns |     2.097 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   532.766 ns |  6.9601 ns |  6.1699 ns |   521.713 ns |   543.106 ns | 0.0010 |     - |     - |      96 B |
| Add_AlternateDimensionsBetweenCalls_5DCounter |   205.228 ns |  2.5276 ns |  2.3643 ns |   202.206 ns |   209.474 ns | 0.0012 |     - |     - |      96 B |
|       Add_Update1DimValueInEachCall_5DCounter |   283.302 ns |  5.2662 ns |  6.6601 ns |   273.194 ns |   296.726 ns | 0.0029 |     - |     - |     256 B |
|       Add_Update3DimValueInEachCall_5DCounter |   496.530 ns |  6.0356 ns |  4.7122 ns |   487.837 ns |   502.717 ns | 0.0038 |     - |     - |     336 B |
|      Add_Update3DimValueInEachCall_10DCounter | 1,125.330 ns | 19.8558 ns | 17.6016 ns | 1,099.126 ns | 1,152.862 ns | 0.0038 |     - |     - |     336 B |
|      Add_Update5DimValueInEachCall_10DCounter | 1,233.675 ns | 18.7100 ns | 16.5860 ns | 1,213.684 ns | 1,273.128 ns | 0.0057 |     - |     - |     568 B |
|      Add_Update5DimValueInEachCall_20DCounter | 2,020.348 ns | 33.7714 ns | 28.2007 ns | 1,982.434 ns | 2,091.508 ns | 0.0038 |     - |     - |     568 B |
|       Add_NoDimensionChanges_NewDimensionList |   657.133 ns | 10.1743 ns |  9.0193 ns |   644.888 ns |   677.452 ns | 0.0038 |     - |     - |     368 B |
