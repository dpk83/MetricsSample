``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-YNWIYQ : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                        Method |       Mean |     Error |    StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|----------:|----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |   2.252 ns | 0.0448 ns | 0.0397 ns |   2.208 ns |   2.333 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   2.341 ns | 0.0811 ns | 0.0902 ns |   2.238 ns |   2.519 ns |      - |     - |     - |         - |
| Add_AlternateDimensionsBetweenCalls_5DCounter |   3.208 ns | 0.0433 ns | 0.0405 ns |   3.147 ns |   3.276 ns |      - |     - |     - |         - |
|       Add_Update1DimValueInEachCall_5DCounter |  79.042 ns | 1.5634 ns | 1.4624 ns |  76.687 ns |  81.609 ns | 0.0005 |     - |     - |      40 B |
|       Add_Update3DimValueInEachCall_5DCounter | 242.495 ns | 4.6849 ns | 4.6012 ns | 235.768 ns | 251.034 ns | 0.0014 |     - |     - |     120 B |
|      Add_Update3DimValueInEachCall_10DCounter | 279.034 ns | 5.1257 ns | 4.5438 ns | 270.047 ns | 286.147 ns | 0.0014 |     - |     - |     120 B |
|      Add_Update5DimValueInEachCall_10DCounter | 455.269 ns | 6.1932 ns | 5.1716 ns | 444.758 ns | 462.086 ns | 0.0024 |     - |     - |     200 B |
|      Add_Update5DimValueInEachCall_20DCounter | 562.071 ns | 8.8397 ns | 8.2687 ns | 550.984 ns | 579.346 ns | 0.0019 |     - |     - |     200 B |
|     Add_NoDimensionChanges_NewDimensionObject | 833.138 ns | 9.1077 ns | 8.0738 ns | 817.788 ns | 843.001 ns | 0.0067 |     - |     - |     552 B |
