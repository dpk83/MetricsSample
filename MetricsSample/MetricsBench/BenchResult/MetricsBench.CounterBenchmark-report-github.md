``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-HRVWDO : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Runtime=.NET Core 5.0  Server=True  

```
|                                    Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                         Add_NullDimension |   6.697 ns |  0.1756 ns |  0.4000 ns |   5.903 ns |   7.587 ns |      - |     - |     - |         - |
|                    Add_NoDimensionChanges |  11.114 ns |  0.2617 ns |  0.5520 ns |   9.896 ns |  12.463 ns |      - |     - |     - |         - |
|   Add_Update1DimValueInEachCall_5DCounter | 189.995 ns |  3.8391 ns |  5.8627 ns | 171.791 ns | 199.520 ns | 0.0010 |     - |     - |      88 B |
|   Add_Update3DimValueInEachCall_5DCounter | 247.342 ns |  4.9443 ns |  9.1646 ns | 225.374 ns | 262.879 ns | 0.0012 |     - |     - |      96 B |
|  Add_Update3DimValueInEachCall_10DCounter | 340.552 ns |  6.6841 ns |  8.9230 ns | 327.730 ns | 359.039 ns | 0.0014 |     - |     - |     128 B |
|  Add_Update5DimValueInEachCall_10DCounter | 414.353 ns |  8.2217 ns |  9.1384 ns | 399.646 ns | 431.650 ns | 0.0014 |     - |     - |     136 B |
| Add_Update10DimValueInEachCall_10DCounter | 665.843 ns | 13.0747 ns | 19.5696 ns | 633.139 ns | 712.240 ns | 0.0019 |     - |     - |     168 B |
