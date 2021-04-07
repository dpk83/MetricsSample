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
                    _ = sb.Append("    using Microsoft.R9.Extensions.Meter.Geneva;\n");
                    _ = sb.Append("    using System.Collections.Concurrent;\n");

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
                    _ = sb.Append(GenCounterCreateMethods(metricInstrumentClass));

                    foreach (var metricInstrumentMethod in metricInstrumentClass.Methods)
                    {
                        _ = sb.Append(GenCounterClass(metricInstrumentMethod));
                    }

                    if (string.IsNullOrWhiteSpace(metricInstrumentClass.Namespace))
                    {
                        return $@"
    using System;
    using System.Collections.Generic;
    using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
    using Microsoft.R9.Extensions.Meter;
    using Microsoft.R9.Extensions.Meter.Geneva;

    {sb}
    ";
                    }

                    return $@"
    using System;
    using System.Collections.Generic;
    using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
    using Microsoft.R9.Extensions.Meter;
    using Microsoft.R9.Extensions.Meter.Geneva;

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
            private static readonly ConcurrentDictionary<string, ICounterMetric<long>> _longCounterMetrics = new ();
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
                var meterParam = metricInstrumentMethod.AllParameters[0];

                return $@"
            [global::System.Runtime.CompilerServices.CompilerGenerated]
            public static {metricInstrumentMethod.MetricName} Create{metricInstrumentMethod.MetricName}({meterParam.Type} {meterParam.Name}{GenStaticParameters(metricInstrumentMethod)})
            {{
                string metricName = ""{metricInstrumentMethod.MetricName}"";
                if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
                {{
                    return counterMetric as {metricInstrumentMethod.MetricName};
                }}

                GenevaMeter genevaMeter = meter as GenevaMeter;

                var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => {{
                    var cumulativeMetric = genevaMeter.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                                MdmMetricFlags.CumulativeMetricDefault,
                                                genevaMeter.MonitoringAccount,
                                                genevaMeter.MetricNamespace,
                                                metricName
                                                {GenStaticParametersNames(metricInstrumentMethod)}
                                                {GenDynamicParametersNames(metricInstrumentMethod)});

                    return new {metricInstrumentMethod.MetricName}(cumulativeMetric{GenStaticParameters(metricInstrumentMethod, false)});
                }});

                return metric as {metricInstrumentMethod.MetricName};
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
        public partial class {metricInstrumentMethod.MetricName} : ICounterMetric<long>
        {{
            private string[] _staticKeyArray;
            private string[] _staticValArray;
            private string[] _dynamicKeyArray;
            private string[] _dynamicValArray;            

            private {GetDimensionsValueType(metricInstrumentMethod)} _defaultDimensionValues;
            private bool _isDirty = true;

            internal IMdmCumulativeMetric<{GetDimensionsValueType(metricInstrumentMethod)}, ulong> CumulativeMetric {{ get; }}

            public {metricInstrumentMethod.MetricName}
                (IMdmCumulativeMetric<{GetDimensionsValueType(metricInstrumentMethod)}, ulong> cumulativeMetric 
                {GenStaticParameters(metricInstrumentMethod)})
            {{
                CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
                _staticKeyArray = new string[{metricInstrumentMethod.StaticDimensions.Count}]; 
                _staticValArray = new string[{metricInstrumentMethod.StaticDimensions.Count}];

                _dynamicKeyArray = new string[{metricInstrumentMethod.DynamicDimensions.Count}];  
                _dynamicValArray = new string[{metricInstrumentMethod.DynamicDimensions.Count}];

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
                return GeneratedCounterMetricFactory.Create{metricInstrumentMethod.MetricName}({meterArg}{GenStaticParameters(metricInstrumentMethod, false)});
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

            private string GenStaticParameters(MetricInstrumentMethod metricInstrumentMethod, bool includeType = true)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var dimension in metricInstrumentMethod.StaticDimensions)
                    {
                        _ = sb.Append(", ");

                        if (includeType)
                        {
                            _ = sb.Append($"string ");  //sb.Append($"{dimension.Type}");
                        }
                        _ = sb.Append($"{dimension}");
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenDynamicParameters(MetricInstrumentMethod metricInstrumentMethod, bool includeType = true)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var dimension in metricInstrumentMethod.DynamicDimensions)
                    {
                        _ = sb.Append(", ");

                        if (includeType)
                        {
                            _ = sb.Append($"string ");  //sb.Append($"{dimension.Type}");
                        }
                        _ = sb.Append($"{dimension}");
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenStaticParametersNames(MetricInstrumentMethod metricInstrumentMethod)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var dimension in metricInstrumentMethod.StaticDimensions)
                    {
                        _ = sb.Append(@$", ""{dimension}""");
                    }

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(sb);
                }
            }

            private string GenDynamicParametersNames(MetricInstrumentMethod metricInstrumentMethod, bool includeType = true)
            {
                var sb = GetStringBuilder();
                try
                {
                    foreach (var dimension in metricInstrumentMethod.DynamicDimensions)
                    {
                        _ = sb.Append(@$", ""{dimension}""");
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
                    foreach (var staticDimension in metricInstrumentMethod.StaticDimensions)
                    {
                        sb.Append($@"
                _staticKeyArray[{index}] = ""{staticDimension}"";
                _staticValArray[{index}] = {staticDimension};"
                                );
                        index++;
                    }

                    index = 0;
                    foreach (var dynamicDimension in metricInstrumentMethod.DynamicDimensions)
                    {
                        sb.Append($@"

                _dynamicKeyArray[{index}] = ""{dynamicDimension}"";
                _dynamicValArray[{index}] = {dynamicDimension};"
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
                var subsb = GetStringBuilder();
                try
                {
                    int index = 0;
                    foreach (var dimension in metricInstrumentMethod.DynamicDimensions)
                    {
                        _ = sb.Append($@"
            public string {dimension}
            {{
                get => _dynamicValArray[{index}];
                set {{ if (_dynamicValArray[{index}] != value) {{ _dynamicValArray[{index}] = value; _isDirty = true; }} }}
            }}");

                        index++;
                    }

                    foreach (var dimension in metricInstrumentMethod.DynamicDimensions)
                    {
                        subsb.Append($@"
                        case ""{dimension}"": {dimension} = value;return;");
                    }
                    _ = sb.Append($@"
            public string this[string key]
            {{
                set
                {{
                    switch(key)
                    {{
{subsb}
                        default: throw new ArgumentOutOfRangeException(nameof(key));
                    }}
                }}
            }}
            ");

                    return sb.ToString();
                }
                finally
                {
                    ReturnStringBuilder(subsb);
                    ReturnStringBuilder(sb);
                }
            }

            private string GenClassMethods(MetricInstrumentMethod metricInstrumentMethod)
            {
                var sb = GetStringBuilder();
                var subsb = GetStringBuilder();
                try
                {
                    int i = 0;
                    for (; i < metricInstrumentMethod.StaticDimensions.Count; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append($"_staticValArray[{i}]");
                    }

                    for (i = 0; i < metricInstrumentMethod.DynamicDimensions.Count; i++)
                    {
                        if (i > 0 || metricInstrumentMethod.StaticDimensions.Count > 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append($"_dynamicValArray[{i}]");
                    }

                    foreach (var dimension in metricInstrumentMethod.DynamicDimensions)
                    {
                        subsb.Append($@"
                this.{dimension} = {dimension};");
                    }

                    string str = string.Empty;
                    if (metricInstrumentMethod.DynamicDimensions.Count > 0)
                    {
                        str = $@"
            public void Add(long value{GenDynamicParameters(metricInstrumentMethod)})
            {{
                {subsb}
                Add(value);
            }}";
                    }

                    return $@"{str}

            public void Add(long value, IList<(string key, string value)>? dimensions)
            {{
                throw new NotImplementedException();
            }}

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
                    ReturnStringBuilder(subsb);
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

            private string GetDimensionsValueType(MetricInstrumentMethod metricInstrumentMethod)
            {
                int count = metricInstrumentMethod.StaticDimensions.Count + metricInstrumentMethod.DynamicDimensions.Count;
                return string.Format($"DimensionValues{count}D");
            }
        }
    }
}
