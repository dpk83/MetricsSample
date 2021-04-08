using System;
using System.Collections.Generic;
using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;

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
    }
}


namespace Temp
{
    public class T1
    { 
        public string this[string key]
        {
            set
            { 
            
            }
        }
    }
}