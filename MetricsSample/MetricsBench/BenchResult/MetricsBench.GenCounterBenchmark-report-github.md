``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Server=True  Toolchain=InProcessEmitToolchain  IterationCount=3  
LaunchCount=1  WarmupCount=3  

```
|                                    Method |       Mean |      Error |    StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-----------:|-----------:|----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                         Add_NullDimension |  0.8833 ns |  0.3984 ns | 0.0218 ns |  0.8640 ns |  0.9070 ns |      - |     - |     - |         - |
|                    Add_NoDimensionChanges |  0.8917 ns |  0.3062 ns | 0.0168 ns |  0.8726 ns |  0.9042 ns |      - |     - |     - |         - |
|   Add_Update1DimValueInEachCall_5DCounter | 29.4419 ns |  5.5660 ns | 0.3051 ns | 29.1231 ns | 29.7311 ns | 0.0063 |     - |     - |      40 B |
|   Add_Update3DimValueInEachCall_5DCounter | 32.6043 ns |  1.7887 ns | 0.0980 ns | 32.4911 ns | 32.6610 ns | 0.0063 |     - |     - |      40 B |
|  Add_Update3DimValueInEachCall_10DCounter | 33.6353 ns | 27.5205 ns | 1.5085 ns | 32.5020 ns | 35.3475 ns | 0.0063 |     - |     - |      40 B |
|  Add_Update5DimValueInEachCall_10DCounter | 35.5876 ns |  3.3634 ns | 0.1844 ns | 35.4182 ns | 35.7840 ns | 0.0063 |     - |     - |      40 B |
| Add_Update10DimValueInEachCall_10DCounter | 44.6458 ns |  3.9720 ns | 0.2177 ns | 44.4090 ns | 44.8373 ns | 0.0063 |     - |     - |      40 B |
