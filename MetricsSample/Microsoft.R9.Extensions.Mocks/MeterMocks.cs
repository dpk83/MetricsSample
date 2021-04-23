using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
using System;
using System.Collections.Generic;

namespace Microsoft.R9.Extensions.Meter
{
    public interface ICounterMetric<in T>
        where T : struct
    {
        void Add(T value, IList<(string key, string value)>? dimensions);
    }

    public interface IValueRecorderMetric<in T>
        where T : struct
    {
        void Record(T value, IList<(string key, string value)>? dimensions);
    }

    public interface IMeter
    {
        ICounterD CreateCounter(string name);
        ICounterD CreateCounter(string name, string k1);
        ICounterD CreateCounter(string name, string k1, string k2);
        ICounterD CreateCounter(string name, string k1, string k2, string k3);
        ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4);
        ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5);
        ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6);
        ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7);
        ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8);
        ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9);
        ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9, string k10);
    }

    public interface ICounterD
    {
    }

    public interface ICounter0D : ICounterD
    {
        public void Add(long value);
    }

    public interface ICounter1D : ICounterD
    {
        public void Add(long value, string value1);
    }

    public interface ICounter2D : ICounterD
    {
        public void Add(long value, string value1, string value2);
    }

    public interface ICounter3D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3);
    }
    public interface ICounter4D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3, string value4);
    }
    public interface ICounter5D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5);
    }
    public interface ICounter6D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6);
    }
    public interface ICounter7D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7);
    }
    public interface ICounter8D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8);
    }
    public interface ICounter9D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9);
    }
    public interface ICounter10D : ICounterD
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9, string value10);
    }
}

namespace Microsoft.R9.Extensions.Meter.Geneva
{
    public partial class GenevaMeter : IMeter
    {
        // private readonly GeneratedCounterMetricFactory generatedCounterMetricFactory;
        public MdmMetricFactory MdmMetricFactory { get; }
        public string MonitoringAccount { get; }
        public string MetricNamespace { get; }

        public GenevaMeter(string meterName, MdmMetricFactory mdmMetricFactory, string monitoringAccount)
        {
            MdmMetricFactory = mdmMetricFactory;
            MonitoringAccount = monitoringAccount;
            MetricNamespace = meterName;
        }

        public ICounterD CreateCounter(string name)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name);
            return new Counter0D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1);
            return new Counter1D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2);
            return new Counter2D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3);
            return new Counter3D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4);
            return new Counter4D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5);
            return new Counter5D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6);
            return new Counter6D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7);
            return new Counter7D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7, k8);
            return new Counter8D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7, k8, k9);
            return new Counter9D(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9, string k10)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7, k8, k9, k10);
            return new Counter10D(cummulativeMetric);
        }
    }

    public class Counter0D : ICounter0D 
    {
        internal IMdmCumulativeMetric<DimensionValues0D, ulong> CumulativeMetric { get; }

        public Counter0D(
            IMdmCumulativeMetric<DimensionValues0D, ulong> cumulativeMetric)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create();
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter1D : ICounter1D
    {
        internal IMdmCumulativeMetric<DimensionValues1D, ulong> CumulativeMetric { get; }
        public Counter1D(
            IMdmCumulativeMetric<DimensionValues1D, ulong> cumulativeMetric) 
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter2D : ICounter2D
    {
        internal IMdmCumulativeMetric<DimensionValues2D, ulong> CumulativeMetric { get; }
        public Counter2D(
            IMdmCumulativeMetric<DimensionValues2D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter3D : ICounter3D
    {
        internal IMdmCumulativeMetric<DimensionValues3D, ulong> CumulativeMetric { get; }
        public Counter3D(
            IMdmCumulativeMetric<DimensionValues3D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter4D : ICounter4D
    {
        internal IMdmCumulativeMetric<DimensionValues4D, ulong> CumulativeMetric { get; }
        public Counter4D(
            IMdmCumulativeMetric<DimensionValues4D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter5D : ICounter5D
    {
        internal IMdmCumulativeMetric<DimensionValues5D, ulong> CumulativeMetric { get; }
        public Counter5D(
            IMdmCumulativeMetric<DimensionValues5D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter6D : ICounter6D
    {
        internal IMdmCumulativeMetric<DimensionValues6D, ulong> CumulativeMetric { get; }
        public Counter6D(
            IMdmCumulativeMetric<DimensionValues6D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter7D : ICounter7D
    {
        internal IMdmCumulativeMetric<DimensionValues7D, ulong> CumulativeMetric { get; }
        public Counter7D(
            IMdmCumulativeMetric<DimensionValues7D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter8D : ICounter8D
    {
        internal IMdmCumulativeMetric<DimensionValues8D, ulong> CumulativeMetric { get; }
        public Counter8D(
            IMdmCumulativeMetric<DimensionValues8D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7, value8);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter9D : ICounter9D
    {
        internal IMdmCumulativeMetric<DimensionValues9D, ulong> CumulativeMetric { get; }
        public Counter9D(
            IMdmCumulativeMetric<DimensionValues9D, ulong> cumulativeMetric)
           
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7, value8, value9);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter10D : ICounter10D
    {
        internal IMdmCumulativeMetric<DimensionValues10D, ulong> CumulativeMetric { get; }
        public Counter10D(
            IMdmCumulativeMetric<DimensionValues10D, ulong> cumulativeMetric)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9, string value10)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }
}
