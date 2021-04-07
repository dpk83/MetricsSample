﻿// <auto-generated/>
#nullable enable
    using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
    using Microsoft.R9.Extensions.Meter;
    using Microsoft.R9.Extensions.Meter.Geneva;
    using System.Collections.Concurrent;

    namespace MetricsBench
    {
        
        [global::System.Runtime.CompilerServices.CompilerGenerated]
        public static partial class GeneratedCounterMetricFactory
        {
            private static readonly ConcurrentDictionary<string, ICounterMetric<long>> _longCounterMetrics = new ();
            
            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter5D CreateCounter5D(Microsoft.R9.Extensions.Meter.IMeter meter, string k1, string k2, string k3, string k4, string k5)
            {
                string metricName = "Counter5D";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter5D;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                , "k1", "k2", "k3", "k4", "k5"
                                                );

                    return new Counter5D(cumulativeMetric, k1, k2, k3, k4, k5);
                });

                return metric as Counter5D;
            }

            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter10D CreateCounter10D(Microsoft.R9.Extensions.Meter.IMeter meter, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9, string k10)
            {
                string metricName = "Counter10D";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter10D;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                , "k1", "k2", "k3", "k4", "k5", "k6", "k7", "k8", "k9", "k10"
                                                );

                    return new Counter10D(cumulativeMetric, k1, k2, k3, k4, k5, k6, k7, k8, k9, k10);
                });

                return metric as Counter10D;
            }

            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter5DNullDim CreateCounter5DNullDim(Microsoft.R9.Extensions.Meter.IMeter meter, string k1, string k2, string k3, string k4, string k5)
            {
                string metricName = "Counter5DNullDim";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter5DNullDim;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                , "k1", "k2", "k3", "k4", "k5"
                                                );

                    return new Counter5DNullDim(cumulativeMetric, k1, k2, k3, k4, k5);
                });

                return metric as Counter5DNullDim;
            }

            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter5D1Change CreateCounter5D1Change(Microsoft.R9.Extensions.Meter.IMeter meter, string k1, string k2, string k4, string k5)
            {
                string metricName = "Counter5D1Change";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter5D1Change;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                , "k1", "k2", "k4", "k5"
                                                , "k3");

                    return new Counter5D1Change(cumulativeMetric, k1, k2, k4, k5);
                });

                return metric as Counter5D1Change;
            }

            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter5D3Change CreateCounter5D3Change(Microsoft.R9.Extensions.Meter.IMeter meter, string k1, string k4)
            {
                string metricName = "Counter5D3Change";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter5D3Change;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                , "k1", "k4"
                                                , "k2", "k3", "k5");

                    return new Counter5D3Change(cumulativeMetric, k1, k4);
                });

                return metric as Counter5D3Change;
            }

            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter10D3Change CreateCounter10D3Change(Microsoft.R9.Extensions.Meter.IMeter meter, string k1, string k4, string k6, string k7, string k8, string k9, string k10)
            {
                string metricName = "Counter10D3Change";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter10D3Change;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                , "k1", "k4", "k6", "k7", "k8", "k9", "k10"
                                                , "k2", "k3", "k5");

                    return new Counter10D3Change(cumulativeMetric, k1, k4, k6, k7, k8, k9, k10);
                });

                return metric as Counter10D3Change;
            }

            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter10D5Change CreateCounter10D5Change(Microsoft.R9.Extensions.Meter.IMeter meter, string k1, string k4, string k6, string k8, string k10)
            {
                string metricName = "Counter10D5Change";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter10D5Change;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                , "k1", "k4", "k6", "k8", "k10"
                                                , "k2", "k3", "k5", "k7", "k9");

                    return new Counter10D5Change(cumulativeMetric, k1, k4, k6, k8, k10);
                });

                return metric as Counter10D5Change;
            }

            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static Counter10D10Change CreateCounter10D10Change(Microsoft.R9.Extensions.Meter.IMeter meter)
            {
                string metricName = "Counter10D10Change";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {
                    return counterMetric as Counter10D10Change;
                }

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                
                                                , "k1", "k4", "k6", "k8", "k10", "k2", "k3", "k5", "k7", "k9");

                    return new Counter10D10Change(cumulativeMetric);
                });

                return metric as Counter10D10Change;
            }

        }
    
    }
    