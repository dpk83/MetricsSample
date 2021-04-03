``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Xeon CPU E5-1650 v2 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Server=True  Toolchain=InProcessEmitToolchain  IterationCount=3  
LaunchCount=1  WarmupCount=3  

```
|                                    Method |         Mean |       Error |     StdDev |          Min |          Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------ |-------------:|------------:|-----------:|-------------:|-------------:|-------:|------:|------:|----------:|
|                         Add_NullDimension |     7.232 ns |   0.5139 ns |  0.0282 ns |     7.208 ns |     7.263 ns |      - |     - |     - |         - |
|                    Add_NoDimensionChanges |   560.405 ns | 158.1480 ns |  8.6686 ns |   554.390 ns |   570.342 ns | 0.0191 |     - |     - |     120 B |
|   Add_Update1DimValueInEachCall_5DCounter |   300.636 ns | 144.3990 ns |  7.9150 ns |   291.546 ns |   306.001 ns | 0.0443 |     - |     - |     280 B |
|   Add_Update3DimValueInEachCall_5DCounter |   510.433 ns | 155.9419 ns |  8.5477 ns |   500.802 ns |   517.117 ns | 0.0443 |     - |     - |     280 B |
|  Add_Update3DimValueInEachCall_10DCounter | 1,098.694 ns | 625.2962 ns | 34.2746 ns | 1,075.604 ns | 1,138.076 ns | 0.0439 |     - |     - |     280 B |
|  Add_Update5DimValueInEachCall_10DCounter | 1,159.036 ns | 174.6375 ns |  9.5725 ns | 1,151.362 ns | 1,169.762 ns | 0.0687 |     - |     - |     432 B |
| Add_Update10DimValueInEachCall_10DCounter | 1,260.543 ns | 263.1524 ns | 14.4243 ns | 1,245.674 ns | 1,274.477 ns | 0.1125 |     - |     - |     712 B |
