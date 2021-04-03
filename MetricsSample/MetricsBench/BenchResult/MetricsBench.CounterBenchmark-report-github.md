``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Server=True  Toolchain=InProcessEmitToolchain  IterationCount=3  
LaunchCount=1  WarmupCount=3  

```
|                                    Method |       Mean |     Error |    StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-----------:|----------:|----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                         Add_NullDimension |   6.475 ns |  1.389 ns | 0.0762 ns |   6.418 ns |   6.562 ns |      - |     - |     - |         - |
|                    Add_NoDimensionChanges |  10.919 ns |  2.202 ns | 0.1207 ns |  10.819 ns |  11.053 ns |      - |     - |     - |         - |
|   Add_Update1DimValueInEachCall_5DCounter | 191.468 ns | 76.946 ns | 4.2177 ns | 188.837 ns | 196.333 ns | 0.0203 |     - |     - |     128 B |
|   Add_Update3DimValueInEachCall_5DCounter | 243.517 ns | 32.106 ns | 1.7598 ns | 241.721 ns | 245.239 ns | 0.0238 |     - |     - |     152 B |
|  Add_Update3DimValueInEachCall_10DCounter | 331.917 ns | 61.496 ns | 3.3708 ns | 329.715 ns | 335.798 ns | 0.0291 |     - |     - |     184 B |
|  Add_Update5DimValueInEachCall_10DCounter | 396.412 ns | 77.066 ns | 4.2242 ns | 391.606 ns | 399.535 ns | 0.0329 |     - |     - |     208 B |
| Add_Update10DimValueInEachCall_10DCounter | 639.290 ns | 39.785 ns | 2.1807 ns | 637.741 ns | 641.784 ns | 0.0458 |     - |     - |     288 B |
