``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-PDOZAV : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |       Mean |     Error |    StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|----------:|----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |  0.8801 ns | 0.0237 ns | 0.0210 ns |  0.8538 ns |  0.9245 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |  0.8550 ns | 0.0241 ns | 0.0226 ns |  0.8158 ns |  0.8926 ns |      - |     - |     - |         - |
| Add_AlternateDimensionsBetweenCalls_5DCounter | 29.4672 ns | 0.5181 ns | 0.5544 ns | 28.4058 ns | 30.4196 ns | 0.0005 |     - |     - |      40 B |
|       Add_Update1DimValueInEachCall_5DCounter | 29.3036 ns | 0.5915 ns | 0.5533 ns | 28.4647 ns | 30.6742 ns | 0.0005 |     - |     - |      40 B |
|       Add_Update3DimValueInEachCall_5DCounter | 33.1961 ns | 0.6010 ns | 0.5328 ns | 32.4082 ns | 34.2451 ns | 0.0005 |     - |     - |      40 B |
|      Add_Update3DimValueInEachCall_10DCounter | 31.9791 ns | 0.6298 ns | 0.5260 ns | 31.4210 ns | 33.1072 ns | 0.0005 |     - |     - |      40 B |
|      Add_Update5DimValueInEachCall_10DCounter | 36.0656 ns | 0.7212 ns | 0.6393 ns | 35.1936 ns | 36.9633 ns | 0.0005 |     - |     - |      40 B |
