﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.R9.Extensions.MetricGenerator
{
    public partial class MetricGenerator
    {
        internal class Emitter
        {
            private readonly Stack<StringBuilder> _builders = new();
            private readonly string _fieldName = "_meter";

            public Emitter()
            {
            }

            public string EmitGenevaMeter(IReadOnlyList<MetricInstrumentClass> metricInstrumentClasses, CancellationToken cancellationToken)
            {
                var sb = GetStringBuilder();
                try
                {
                    _ = sb.Append("// <auto-generated/>\n");
                    _ = sb.Append("#nullable enable\n");
                    _ = sb.Append("    using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;\n");
                    _ = sb.Append("    using Microsoft.R9.Extensions.Meter;\n");

                    foreach (var metricInstrumentClass in metricInstrumentClasses)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        _ = sb.Append(GenMetricInstrumentFactory(metricInstrumentClass));
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            public string EmitMetricInstruments(IReadOnlyList<MetricInstrumentClass> metricInstrumentClasses, CancellationToken cancellationToken)
            {
                var sb = GetStringBuilder();
                try
                {
                    _ = sb.Append("// <auto-generated/>\n");
                    _ = sb.Append("#nullable enable\n");

                    foreach (var metricInstrumentClass in metricInstrumentClasses)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        _ = sb.Append(GenType(metricInstrumentClass));
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenType(MetricInstrumentClass metricInstrumentClass)
            {
                var sb = GetStringBuilder();
                try
                {
                    _ = sb.Append(@"
    using System;
    using System.Collections.Generic;
    using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
    using Microsoft.R9.Extensions.Meter;
    using Microsoft.R9.Extensions.Meter.Geneva;
");
                    _ = sb.Append(GenCounterCreateMethods(metricInstrumentClass));

                    foreach (var metricInstrumentMethod in metricInstrumentClass.Methods)
                    {
                        _ = sb.Append(GenCounterClass(metricInstrumentMethod));
                    }

                    if (string.IsNullOrWhiteSpace(metricInstrumentClass.Namespace))
                    {
                        return sb.ToString();
                    }

                    return $@"

    namespace {metricInstrumentClass.Namespace}
    {{
        {sb}
    }}
    ";
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenMetricInstrumentFactory(MetricInstrumentClass metricInstrumentClass)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var metricInstrumentMethod in metricInstrumentClass.Methods)
                    {
                        _ = sb.Append(GenMetricInstrumentFactoryMethods(metricInstrumentMethod));
                    }

                    string str = $@"
        [global::System.Runtime.CompilerServices.CompilerGenerated]
        public static partial class GeneratedCounterMetricFactory
        {{
            {sb}
        }}
    ";
                    if (string.IsNullOrWhiteSpace(metricInstrumentClass.Namespace))
                    {
                        return str;
                    }

                    return $@"
    namespace {metricInstrumentClass.Namespace}
    {{
        {str}
    }}
    ";
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenMetricInstrumentFactoryMethods(MetricInstrumentMethod metricInstrumentMethod)
            {
                return $@"
            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static {metricInstrumentMethod.InstrumentClassType} Create{metricInstrumentMethod.InstrumentClassType}(MeterOptions meterOptions, string metricName, {GenRegularParameters(metricInstrumentMethod)})
            {{
                var cumulativeMetric = meterOptions.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                            MdmMetricFlags.CumulativeMetricDefault,
                                            meterOptions.MonitoringAccount,
                                            meterOptions.MetricNamespace,
                                            metricName,
                                            {GenRegularParametersNames(metricInstrumentMethod)});

                return new {metricInstrumentMethod.InstrumentClassType}(cumulativeMetric, {GenRegularParameters(metricInstrumentMethod, false)});
            }}
";
            }

            private string GenCounterCreateMethods(MetricInstrumentClass metricInstrumentClass)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var metricInstrumentMethod in metricInstrumentClass.Methods)
                    {
                        // _ = sb.Append($"[DEEPAK] In metricInstrumentMethod loop {metricInstrumentMethod.Name}\n");
                        _ = sb.Append(GenCounterMethod(metricInstrumentMethod));
                    }

                    return $@"
        [global::System.Runtime.CompilerServices.CompilerGenerated]
        public static partial class {metricInstrumentClass.Name} {metricInstrumentClass.Constraints}
        {{
            {sb}
        }}
        ";
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenCounterClass(MetricInstrumentMethod metricInstrumentMethod)
            {
                return $@"
        [global::System.Runtime.CompilerServices.CompilerGenerated]
        public class {metricInstrumentMethod.InstrumentClassType} : ICounterMetric<long>
        {{
            private string[] _keyArray;
            private string[] _valArray;

            private {GetDimensionsValueType(metricInstrumentMethod.RegularParameters.Count)} _defaultDimensionValues;
            private bool _isDirty = true;

            internal IMdmCumulativeMetric<{GetDimensionsValueType(metricInstrumentMethod.RegularParameters.Count)}, ulong> CumulativeMetric {{ get; }}

            public {metricInstrumentMethod.InstrumentClassType}
                (IMdmCumulativeMetric<{GetDimensionsValueType(metricInstrumentMethod.RegularParameters.Count)}, ulong> cumulativeMetric, 
                {GenRegularParameters(metricInstrumentMethod)})
            {{
                CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
                int count = {metricInstrumentMethod.RegularParameters.Count};
                _keyArray = new string[count];
                _valArray = new string[count];

                {GenConstructorBody(metricInstrumentMethod)}
            }}
            {GenClassProperties(metricInstrumentMethod)}
            {GenClassMethods(metricInstrumentMethod)}
        }}
        ";
            }

            private string GenCounterMethod(MetricInstrumentMethod metricInstrumentMethod)
            {
                string meterArg = _fieldName;
                foreach (var p in metricInstrumentMethod.AllParameters)
                {
                    if (p.IsMeter)
                    {
                        meterArg = p.Name;
                        break;
                    }
                }

                return $@"
            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static partial {metricInstrumentMethod.InstrumentClassType} {metricInstrumentMethod.Name}({GenParameters(metricInstrumentMethod)})
            {{
                GenevaMeter genevaMeter = meter as GenevaMeter;
                MeterOptions meterOptions = genevaMeter.MeterOptions;
                return GeneratedCounterMetricFactory.Create{metricInstrumentMethod.InstrumentClassType}(meterOptions, ""{metricInstrumentMethod.MetricName}"", {GenRegularParameters(metricInstrumentMethod, false)});
            }}
            ";
            }

            private string GenParameters(MetricInstrumentMethod metricInstrumentMethod, bool includeType = true)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var p in metricInstrumentMethod.AllParameters)
                    {
                        if (p != metricInstrumentMethod.AllParameters[0])
                        {
                            _ = sb.Append(", ");
                        }

                        if (includeType)
                        {
                            _ = sb.Append($"{p.Type} {p.Name}");
                        }
                        else
                        {
                            _ = sb.Append($"{p.Name}");
                        }
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenRegularParameters(MetricInstrumentMethod metricInstrumentMethod, bool includeType = true)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var p in metricInstrumentMethod.RegularParameters)
                    {
                        if (p != metricInstrumentMethod.RegularParameters[0])
                        {
                            _ = sb.Append(", ");
                        }

                        if (includeType)
                        {
                            _ = sb.Append($"{p.Type} {p.Name}");
                        }
                        else
                        {
                            _ = sb.Append($"{p.Name}");
                        }
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenRegularParametersNames(MetricInstrumentMethod metricInstrumentMethod)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var p in metricInstrumentMethod.RegularParameters)
                    {
                        if (p != metricInstrumentMethod.RegularParameters[0])
                        {
                            _ = sb.Append(", ");
                        }

                        _ = sb.Append($"nameof({p.Name})");
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenConstructorBody(MetricInstrumentMethod metricInstrumentMethod)
            {
                var sb = GetStringBuilder();
                try
                {
                    int index = 0;
                    foreach (var p in metricInstrumentMethod.RegularParameters)
                    {
                        sb.Append($@"
                _keyArray[{index}] = ""{p.Name}"";
                _valArray[{index}] = {p.Name};"
                                );
                        index++;
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenClassProperties(MetricInstrumentMethod metricInstrumentMethod)
            {
                var sb = GetStringBuilder();
                try
                {
                    int index = 0;
                    foreach (var p in metricInstrumentMethod.RegularParameters)
                    {
                        _ = sb.Append($@"
            public string {p.Name}
            {{
                get => _valArray[{index}];
                set {{ _valArray[{index}] = value; _isDirty = true; }}
            }}");

                        index++;
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenClassMethods(MetricInstrumentMethod metricInstrumentMethod)
            {
                var sb = GetStringBuilder();
                try
                {
                    for (int i = 0; i < metricInstrumentMethod.RegularParameters.Count; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append($"_valArray[{i}]");
                    }
                    return $@"
            public void Add(long value)
            {{
                if (value != 0)
                {{
                    if (_isDirty)
                    {{
                        _defaultDimensionValues = DimensionValues.Create({sb});
                        _isDirty = false;
                    }}

                    _ = value > 0
                        ? CumulativeMetric.IncrementBy((ulong)value, _defaultDimensionValues)
                        : CumulativeMetric.DecrementBy((ulong)value, _defaultDimensionValues);
                }}
            }}
            ";
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private StringBuilder GetStringBuilder()
            {
                const int DefaultStringBuilderCapacity = 1024;

                if (_builders.Count == 0)
                {
                    return new StringBuilder(DefaultStringBuilderCapacity);
                }

                var sb = _builders.Pop();
                _ = sb.Clear();
                return sb;
            }

            private void ReturnStringBuilder(StringBuilder sb)
            {
                _builders.Push(sb);
            }

            private string GetDimensionsValueType(int count)
            {
                return string.Format($"DimensionValues{count}D");
            }
        }
    }
}
