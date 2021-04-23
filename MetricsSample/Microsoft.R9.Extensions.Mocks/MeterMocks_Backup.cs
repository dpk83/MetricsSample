#if OLD_CODE_ENABLED
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
        ICounterD CreateCounter(string name, string k1, string v1);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7, string k8, string v8);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7, string k8, string v8, string k9, string v9);
        ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7, string k8, string v8, string k9, string v9, string k10, string v10);
    }

    public interface ICounterD
    {
    }

    public interface ICounter1D : ICounterD
    {
        public void Add(long value);
        public void Add(long value, string value1);
    }

    public interface ICounter2D : ICounter1D
    {
        public void Add(long value, string value1, string value2);
    }

    public interface ICounter3D : ICounter2D
    {
        public void Add(long value, string value1, string value2, string value3);
    }
    public interface ICounter4D : ICounter3D
    {
        public void Add(long value, string value1, string value2, string value3, string value4);
    }
    public interface ICounter5D : ICounter4D
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5);
    }
    public interface ICounter6D : ICounter5D
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6);
    }
    public interface ICounter7D : ICounter6D
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7);
    }
    public interface ICounter8D : ICounter7D
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8);
    }
    public interface ICounter9D : ICounter8D
    {
        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9);
    }
    public interface ICounter10D : ICounter9D
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
            return new CounterD(cummulativeMetric);
        }

        public ICounterD CreateCounter(string name, string k1, string v1)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1);
            return new Counter1D(cummulativeMetric, v1);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2);
            return new Counter2D(cummulativeMetric, v1, v2);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3);
            return new Counter3D(cummulativeMetric, v1, v2, v3);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4);
            return new Counter4D(cummulativeMetric, v1, v2, v3, v4);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5);
            return new Counter5D(cummulativeMetric, v1, v2, v3, v4, v5);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6);
            return new Counter6D(cummulativeMetric, v1, v2, v3, v4, v5, v6);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7);
            return new Counter7D(cummulativeMetric, v1, v2, v3, v4, v5, v6, v7);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7, string k8, string v8)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7, k8);
            return new Counter8D(cummulativeMetric, v1, v2, v3, v4, v5, v6, v7, v8);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7, string k8, string v8, string k9, string v9)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7, k8, k9);
            return new Counter9D(cummulativeMetric, v1, v2, v3, v4, v5, v6, v7, v8, v9);
        }

        public ICounterD CreateCounter(string name, string k1, string v1, string k2, string v2, string k3, string v3, string k4, string v4, string k5, string v5, string k6, string v6, string k7, string v7, string k8, string v8, string k9, string v9, string k10, string v10)
        {
            var cummulativeMetric = MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        MonitoringAccount,
                                        MetricNamespace,
                                        name,
                                        k1, k2, k3, k4, k5, k6, k7, k8, k9, k10);
            return new Counter10D(cummulativeMetric, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
        }
    }

    public class CounterD : ICounterD 
    {
        internal IMdmCumulativeMetric<DimensionValues0D, ulong> CumulativeMetric { get; }

        public CounterD(
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
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues1D, ulong> CumulativeMetric { get; }

        public Counter1D(
            IMdmCumulativeMetric<DimensionValues1D, ulong> cumulativeMetric, 
            string v1)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[1];

            _valueArray[0] = v1;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter2D : ICounter2D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues2D, ulong> CumulativeMetric { get; }

        public Counter2D(
            IMdmCumulativeMetric<DimensionValues2D, ulong> cumulativeMetric,
            string v1, string v2)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[2];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter3D : ICounter3D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues3D, ulong> CumulativeMetric { get; }

        public Counter3D(
            IMdmCumulativeMetric<DimensionValues3D, ulong> cumulativeMetric,
            string v1, string v2, string v3)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[3];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[3]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter4D : ICounter4D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues4D, ulong> CumulativeMetric { get; }

        public Counter4D(
            IMdmCumulativeMetric<DimensionValues4D, ulong> cumulativeMetric,
            string v1, string v2, string v3, string v4)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[4];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
            _valueArray[3] = v4;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[3], _valueArray[4]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter5D : ICounter5D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues5D, ulong> CumulativeMetric { get; }

        public Counter5D(
            IMdmCumulativeMetric<DimensionValues5D, ulong> cumulativeMetric,
            string v1, string v2, string v3, string v4, string v5)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[5];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
            _valueArray[3] = v4;
            _valueArray[4] = v5;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter6D : ICounter6D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues6D, ulong> CumulativeMetric { get; }

        public Counter6D(
            IMdmCumulativeMetric<DimensionValues6D, ulong> cumulativeMetric,
            string v1, string v2, string v3, string v4, string v5, string v6)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[6];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
            _valueArray[3] = v4;
            _valueArray[4] = v5;
            _valueArray[5] = v6;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2, value3, value4, value5);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter7D : ICounter7D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues7D, ulong> CumulativeMetric { get; }

        public Counter7D(
            IMdmCumulativeMetric<DimensionValues7D, ulong> cumulativeMetric,
            string v1, string v2, string v3, string v4, string v5, string v6, string v7)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[7];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
            _valueArray[3] = v4;
            _valueArray[4] = v5;
            _valueArray[5] = v6;
            _valueArray[6] = v7;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1, value2, value3, value4, value5);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2, value3, value4, value5, value6);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter8D : ICounter8D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues8D, ulong> CumulativeMetric { get; }

        public Counter8D(
            IMdmCumulativeMetric<DimensionValues8D, ulong> cumulativeMetric,
            string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[8];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
            _valueArray[3] = v4;
            _valueArray[4] = v5;
            _valueArray[5] = v6;
            _valueArray[6] = v7;
            _valueArray[7] = v8;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], _valueArray[7]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], value1, value2, value3, value4, value5);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1, value2, value3, value4, value5, value6);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2, value3, value4, value5, value6, value7);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7, value8);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter9D : ICounter9D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues9D, ulong> CumulativeMetric { get; }

        public Counter9D(
            IMdmCumulativeMetric<DimensionValues9D, ulong> cumulativeMetric,
            string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[9];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
            _valueArray[3] = v4;
            _valueArray[4] = v5;
            _valueArray[5] = v6;
            _valueArray[6] = v7;
            _valueArray[7] = v8;
            _valueArray[8] = v9;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], _valueArray[7], _valueArray[8]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], _valueArray[7], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], value1, value2, value3, value4, value5);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], value1, value2, value3, value4, value5, value6);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1, value2, value3, value4, value5, value6, value7);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2, value3, value4, value5, value6, value7, value8);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7, value8, value9);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }

    public class Counter10D : ICounter10D
    {
        protected readonly string[] _valueArray;

        internal IMdmCumulativeMetric<DimensionValues10D, ulong> CumulativeMetric { get; }

        public Counter10D(
            IMdmCumulativeMetric<DimensionValues10D, ulong> cumulativeMetric,
            string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _valueArray = new string[10];

            _valueArray[0] = v1;
            _valueArray[1] = v2;
            _valueArray[2] = v3;
            _valueArray[3] = v4;
            _valueArray[4] = v5;
            _valueArray[5] = v6;
            _valueArray[6] = v7;
            _valueArray[7] = v8;
            _valueArray[8] = v9;
            _valueArray[9] = v10;
        }

        public void Add(long value)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], _valueArray[7], _valueArray[8], _valueArray[9]);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], _valueArray[7], _valueArray[8], value1);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], _valueArray[7], value1, value2);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], _valueArray[6], value1, value2, value3);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], _valueArray[5], value1, value2, value3, value4);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], _valueArray[4], value1, value2, value3, value4, value5);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], _valueArray[3], value1, value2, value3, value4, value5, value6);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], _valueArray[2], value1, value2, value3, value4, value5, value6, value7);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
        {
            var dim = DimensionValues.Create(_valueArray[0], _valueArray[1], value1, value2, value3, value4, value5, value6, value7, value8);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9)
        {
            var dim = DimensionValues.Create(_valueArray[0], value1, value2, value3, value4, value5, value6, value7, value8, value9);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }

        public void Add(long value, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9, string value10)
        {
            var dim = DimensionValues.Create(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10);
            CumulativeMetric.IncrementBy((ulong)value, dim);
        }
    }
}
#endif