using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions
{
    public interface IMdmMetric<in TDim, in TValue>
        where TDim : IFixedStringsArray
        where TValue : struct
    { 
    
    }

    public interface IMdmCumulativeMetric<in TDim, TValue> : IMdmMetric<TDim, TValue>
        where TDim : IFixedStringsArray
        where TValue : struct
    {
        bool IncrementBy(TValue value, TDim dimensionValues);
        bool DecrementBy(TValue value, TDim dimensionValues);

        MdmMetricData<TValue> GetData(TDim dimensionValues);
    }

    public struct MdmMetricData<TValue> where TValue : struct
    { }

    public interface IMdmMetricFactory
    { 
    
    }

    public class MdmUInt64CumulativeMetric<TDim> : IMdmCumulativeMetric<TDim, ulong>
        where TDim : IFixedStringsArray
    {
        public bool DecrementBy(ulong value, TDim dimensionValues)
        { return true; }

        public MdmMetricData<ulong> GetData(TDim dimensionValues)
        { return new MdmMetricData<ulong>(); }

        public bool IncrementBy(ulong value, TDim dimensionValues)
        { return true; }
    }

    public sealed class MdmMetricFactory : IMdmMetricFactory
    {
        public IMdmCumulativeMetric<DimensionValues3D, ulong> CreateUInt64CumulativeMetric(MdmMetricFlags flags, string monitoringAccount, string metricNamespace, string metricName, string dim1, string dim2, string dim3)
        { return new MdmUInt64CumulativeMetric<DimensionValues3D>(); }
        public IMdmCumulativeMetric<DimensionValues5D, ulong> CreateUInt64CumulativeMetric(MdmMetricFlags flags, string monitoringAccount, string metricNamespace, string metricName, string dim1, string dim2, string dim3, string dim4, string dim5)
        { return new MdmUInt64CumulativeMetric<DimensionValues5D>(); }
        public IMdmCumulativeMetric<DimensionValues10D, ulong> CreateUInt64CumulativeMetric(MdmMetricFlags flags, string monitoringAccount, string metricNamespace, string metricName, string dim1, string dim2, string dim3, string dim4, string dim5, string dim6, string dim7, string dim8, string dim9, string dim10)
        { return new MdmUInt64CumulativeMetric<DimensionValues10D>(); }
    }

    public static class DimensionValues
    {
        public static DimensionValues3D Create(string dim1, string dim2, string dim3)
        {
            return default(DimensionValues3D);
        }

        public static DimensionValues5D Create(string dim1, string dim2, string dim3, string dim4, string dim5)
        {
            return default(DimensionValues5D);
        }

        public static DimensionValues10D Create(string dim1, string dim2, string dim3, string dim4, string dim5, string dim6, string dim7, string dim8, string dim9, string dim10)
        {
            return default(DimensionValues10D);
        }
    }

    public enum MdmMetricFlags
    {
        CumulativeMetricDefault = 0
    }

    public interface IFixedStringsArray
    {
    }

    public struct DimensionValues3D : IFixedStringsArray
    {
    }

    public struct DimensionValues5D : IFixedStringsArray
    {
    }

    public struct DimensionValues10D : IFixedStringsArray
    {
    }
}
