# Perf Comparision of different Metrics solution
This sample project implements multiple different approaches of exposing metrics API in order to achieve a solution for best performing metrics API.

## Breif description of approaches
### **V1**: Version 1 of metrics API.
_Benchmark Name_: **Not shared in this sample repo**

Implemented exactly as per OpenTelemetry specs. Benchmark numbers of this version is not shared here. V2 numbers are much better improvement over V1 which can be used as reference.

### **V2**: Version 2 of metrics API.
_Benchmark Name_: [**OldCounterBenchmark**](https://github.com/dpk83/MetricsSample/blob/main/MetricsSample/MetricsBench/OldCounterBenchmark.cs)

This is a more performant version of original metrics API. In this version BoundCounter as specified as per OTel spec is removed and the functionality is merged into normal Counter object. This version tries to optimize by removing unnecessary allocations and sorting by caching the keys and default dimensions and using rented pools to avoid allocations. 

### **V3.1**: Version 3 of metrics API.
_Benchmark Name_: [**CounterBenchmark**](https://github.com/dpk83/MetricsSample/blob/main/MetricsSample/MetricsBench/CounterBenchmark.cs)

This approach introduced a new type [_**Dimensions**_](https://github.com/dpk83/MetricsSample/blob/main/MetricsSample/MetricsLibrary/Dimensions.cs). This object keeps an array of key/value paris for the counter which is sorted at creation time. It implements set on this[string key] to direclty allow setting value. All the operations and paths in this object are highly optimized. It keeps 2 hashes, one containing hash of all keys and one for hash of all keys+values. The hashes are built by joining all the keys & value strings. Value hash is updated everytime any dimension is updated. 

### **V3.2**: Minor variation of Version 3 of metrics API.
_Benchmark Name_: [**CounterHBenchmark**](https://github.com/dpk83/MetricsSample/blob/main/MetricsSample/MetricsBench/CounterHBenchmark.cs)

This is a minor variation of the above version 3.1. This introduces type [_**HDimension**_](https://github.com/dpk83/MetricsSample/blob/main/MetricsSample/MetricsLibrary/HDimension.cs). The only difference between _**Dimension**_ and _**HDimension**_ is the way hashes are calculated. In this version hashes are actually a combination of hash codes of all keys & values hash codes instead of the strings themselves. 

### **V4**: Version 4 of metrics API.
_Benchmark Name_: [**GenCounterBenchmark**](https://github.com/dpk83/MetricsSample/blob/main/MetricsSample/MetricsBench/GenCounterBenchmark_3.cs)

This approach utilizes SourceGenerators. Developer defines a partial method for Creating counter. SourceGenerators then generates the required code with strong types. 

Currently all above version implements the UpDownCounter from OpenTelemetry spec assuming IfxMetricsExtension as the backend API to Geneva to get a more realistic benchmark results.


## Comparision of _Mean_ values of different approaches from Benchmark results 

|                                    Method |         V2   |     V3.1   |       V3.2 |       V4   |
|------------------------------------------ |-------------:|-----------:|-----------:|-----------:|
|                         Add_NullDimension |     7.232 ns |   6.475 ns |   7.078 ns |  0.8833 ns |
|                    Add_NoDimensionChanges |   560.405 ns |  10.919 ns |   7.308 ns |  0.8917 ns |
|   Add_Update1DimValueInEachCall_5DCounter |   300.636 ns | 191.468 ns | 130.520 ns | 29.4419 ns |
|   Add_Update3DimValueInEachCall_5DCounter |   510.433 ns | 243.517 ns | 238.546 ns | 32.6043 ns |
|  Add_Update3DimValueInEachCall_10DCounter | 1,098.694 ns | 331.917 ns | 272.515 ns | 33.6353 ns |
|  Add_Update5DimValueInEachCall_10DCounter | 1,159.036 ns | 396.412 ns | 390.543 ns | 35.5876 ns |
| Add_Update10DimValueInEachCall_10DCounter | 1,260.543 ns | 639.290 ns | 741.667 ns | 44.6458 ns |


## Comparision of _Allocated_ values of different approaches from Benchmark results 
40B is the benchmark overhead which should be deducted from all results below. Source Generator approach really has no allocations


|                                    Method |        V2 |      V3.1 |      V3.2 |        V4 |
|------------------------------------------ |----------:|----------:|----------:|----------:|
|                         Add_NullDimension |         - |         - |         - |         - |
|                    Add_NoDimensionChanges |     120 B |         - |         - |         - |
|   Add_Update1DimValueInEachCall_5DCounter |     240 B |      88 B |      24 B |         - |
|   Add_Update3DimValueInEachCall_5DCounter |     240 B |      96 B |      24 B |         - |
|  Add_Update3DimValueInEachCall_10DCounter |     240 B |     128 B |      24 B |         - |
|  Add_Update5DimValueInEachCall_10DCounter |     392 B |     136 B |      24 B |         - |
| Add_Update10DimValueInEachCall_10DCounter |     672 B |     168 B |      24 B |         - |


Full Benchmark reports for all four versions can be found [here](https://github.com/dpk83/MetricsSample/tree/main/MetricsSample/MetricsBench/BenchResult)


### Benchmark methods description.
Here is breif description. For more details refer to the actual benchmark code.

**Add_NullDimension**: A counter with static dimensions. None of the dimensions change in any call. Developers call Add method with null dimensions i.e. _**counter.Add(value, null);**_

**Add_NoDimensionChanges**: Same as above except that dimenions are passed in the Add call. _**counter.Add(value, sameStaticDimensions)**__

**Add_Update1DimValueInEachCall_5DCounter**: A counter with 5 Dimensions. 1 dimension changes each time the counter needs to call an Add. 

**Add_Update3DimValueInEachCall_5DCounter**: A counter with 5 Dimensions. 3 dimensions are updated each time the counter needs to call an Add. 

**Add_Update3DimValueInEachCall_10DCounter**: A counter with 10 Dimensions. 3 dimensions are updated each time the counter needs to call an Add. 

**Add_Update5DimValueInEachCall_10DCounter**: A counter with 10 Dimensions. 5 dimensions are updated each time the counter needs to call an Add. 

**Add_Update10DimValueInEachCall_10DCounter**: A counter with 10 Dimensions. All 10 dimensions are updated each time the counter needs to call an Add. 
