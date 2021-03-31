# New Version

|                                        Method |       Mean |      Error |     StdDev |        Min |        Max |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|-----------:|-----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|                             Add_NullDimension |   2.055 ns |  0.0558 ns |  0.0522 ns |   2.006 ns |   2.188 ns |      - |     - |     - |         - |
|                        Add_NoDimensionChanges |   6.205 ns |  0.0676 ns |  0.0632 ns |   6.092 ns |   6.305 ns |      - |     - |     - |         - |
| Add_AlternateDimensionsBetweenCalls_5DCounter |  12.148 ns |  0.1046 ns |  0.0927 ns |  12.039 ns |  12.356 ns |      - |     - |     - |         - |
|       Add_Update1DimValueInEachCall_5DCounter | 140.568 ns |  2.5357 ns |  3.3851 ns | 135.321 ns | 148.494 ns | 0.0012 |     - |     - |     112 B |
|       Add_Update3DimValueInEachCall_5DCounter | 246.852 ns |  4.8292 ns |  6.4468 ns | 235.451 ns | 260.152 ns | 0.0024 |     - |     - |     216 B |
|      Add_Update3DimValueInEachCall_10DCounter | 380.734 ns |  7.5678 ns | 11.7821 ns | 361.913 ns | 410.724 ns | 0.0033 |     - |     - |     264 B |
|      Add_Update5DimValueInEachCall_10DCounter | 484.246 ns |  9.6288 ns | 13.8093 ns | 458.749 ns | 512.908 ns | 0.0038 |     - |     - |     368 B |
|      Add_Update5DimValueInEachCall_20DCounter | 724.577 ns |  9.7438 ns |  8.6376 ns | 709.730 ns | 741.732 ns | 0.0048 |     - |     - |     440 B |
|     Add_NoDimensionChanges_NewDimensionObject | 900.694 ns | 17.6260 ns | 20.9826 ns | 871.291 ns | 929.411 ns | 0.0095 |     - |     - |     768 B |

# Existing Version
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


