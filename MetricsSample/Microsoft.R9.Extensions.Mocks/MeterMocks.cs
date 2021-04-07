using System;
using System.Collections.Generic;
using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;

namespace Microsoft.R9.Extensions.Meter
{
    public interface ICounterMetric<in T>
        where T : struct
    {
        /// <summary>
        /// Lets you add a value to a metric.
        /// </summary>
        /// <param name="value">Value by which the counter should be incremented or decremented.</param>
        /// <remarks>
        /// Repeated keys in variable dimensions will be resolved in a manner when last value wins.
        /// </remarks>
        void Add(T value);

        string this[string key] { set; }
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