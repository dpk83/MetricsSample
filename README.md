# MetricsSample
Metrics sample for initial api feedback

This sample has 3 different implementation for countermetric. Benchmark result of those are shown below, which indicate the version with HDimensions object performs the best

# New Version 1 (Dimensions object: This object joins string to generate hash keys)
|                                        Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |   1.958 ns |  0.0259 ns |  0.0230 ns |   1.933 ns |   2.004 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   6.499 ns |  0.1196 ns |  0.1119 ns |   6.352 ns |   6.752 ns |      - |     - |     - |         - |
| Add_AlternateDimensionsBetweenCalls_5DCounter |  12.070 ns |  0.1288 ns |  0.1142 ns |  11.947 ns |  12.340 ns |      - |     - |     - |         - |
|       Add_Update1DimValueInEachCall_5DCounter | 133.174 ns |  1.5382 ns |  1.3636 ns | 131.698 ns | 136.284 ns | 0.0012 |     - |     - |     112 B |
|       Add_Update3DimValueInEachCall_5DCounter | 247.013 ns |  4.8881 ns |  6.3559 ns | 237.263 ns | 259.356 ns | 0.0024 |     - |     - |     216 B |
|      Add_Update3DimValueInEachCall_10DCounter | 376.217 ns |  7.5577 ns |  8.4003 ns | 364.584 ns | 394.305 ns | 0.0029 |     - |     - |     264 B |
|      Add_Update5DimValueInEachCall_10DCounter | 489.918 ns |  9.0157 ns |  8.4333 ns | 478.030 ns | 509.480 ns | 0.0043 |     - |     - |     368 B |
|      Add_Update5DimValueInEachCall_20DCounter | 735.022 ns |  7.6993 ns |  7.2020 ns | 721.234 ns | 747.792 ns | 0.0048 |     - |     - |     440 B |
|     Add_NoDimensionChanges_NewDimensionObject | 866.785 ns | 15.5553 ns | 13.7894 ns | 843.592 ns | 897.220 ns | 0.0095 |     - |     - |     768 B |

# New Version 2 (HDimensions object: This object uses hashCodes to generte final hashcodes)
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


# Existing Version (The V2 metrics checked in version which was already an improved version over 1st metrics version)
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





