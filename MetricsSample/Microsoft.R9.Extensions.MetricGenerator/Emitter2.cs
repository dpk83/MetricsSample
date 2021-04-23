﻿// © Microsoft Corporation. All rights reserved.

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.R9.Generators.Metric
{
    internal class Emitter2
    {
        private static readonly Regex _regex = new Regex("[:.-]+", RegexOptions.Compiled);

        private readonly string _generatedCodeAttribute =
            $"global::System.CodeDom.Compiler.GeneratedCodeAttribute(" +
            $"\"{typeof(Emitter).Assembly.GetName().Name}\"," +
            $"\"{typeof(Emitter).Assembly.GetName().Version}\")";
        private readonly Stack<StringBuilder> _builders = new();

        public string EmitGenevaMeter(IReadOnlyList<MetricInstrumentClass> metricInstrumentClasses, CancellationToken cancellationToken)
        {
            Dictionary<string, List<MetricInstrumentClass>> metricInstrumentClassesDict = new();
            foreach (var cl in metricInstrumentClasses)
            {
                if (!metricInstrumentClassesDict.ContainsKey(cl.Namespace))
                {
                    metricInstrumentClassesDict.Add(cl.Namespace, new List<MetricInstrumentClass>());
                }

                metricInstrumentClassesDict[cl.Namespace].Add(cl);
            }

            var sb = GetStringBuilder();
            try
            {
                _ = sb.Append("// <auto-generated/>\n");
                _ = sb.Append("#nullable enable\n");

                foreach (var entry in metricInstrumentClassesDict)
                {
                    _ = sb.Append(GenMetricInstrumentFactoryByNamespace(entry.Key, entry.Value, cancellationToken));
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
            Dictionary<string, List<MetricInstrumentClass>> metricInstrumentClassesDict = new();
            foreach (var cl in metricInstrumentClasses)
            {
                if (!metricInstrumentClassesDict.ContainsKey(cl.Namespace))
                {
                    metricInstrumentClassesDict.Add(cl.Namespace, new List<MetricInstrumentClass>());
                }

                metricInstrumentClassesDict[cl.Namespace].Add(cl);
            }

            var sb = GetStringBuilder();
            try
            {
                _ = sb.Append("// <auto-generated/>\n");
                _ = sb.Append("#nullable enable\n");

                foreach (var entry in metricInstrumentClassesDict)
                {
                    _ = sb.Append(GenTypeByNamespace(entry.Key, entry.Value, cancellationToken));
                }

                return sb.ToString();
            }
            finally
            {
                ReturnStringBuilder(sb);
            }
        }

        private static string GetDimensionsValueType(MetricInstrumentMethod metricInstrumentMethod)
        {
            int count = metricInstrumentMethod.StaticDimensionsKeys.Count + metricInstrumentMethod.DynamicDimensionsKeys.Count;
            return $"global::Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions.DimensionValues{count}D";
        }

        private static string GetSanitizedParamName(string paramName)
        {
            return _regex.Replace(paramName, "_");
        }

        private string GenTypeByNamespace(string nspace, IReadOnlyList<MetricInstrumentClass> metricInstrumentClasses, CancellationToken cancellationToken)
        {
            var sb = GetStringBuilder();
            try
            {
                foreach (var metricInstrumentClass in metricInstrumentClasses)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    _ = sb.Append(GenType(metricInstrumentClass));
                }

                if (string.IsNullOrWhiteSpace(nspace))
                {
                    return $@"
{sb}
";
                }

                return $@"
namespace {nspace}
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S103:Lines should not be too long", Justification = "The line will be within limits in generated code")]
        private string GenMetricInstrumentFactoryByNamespace(string nspace, IReadOnlyList<MetricInstrumentClass> metricInstrumentClasses, CancellationToken cancellationToken)
        {
            var sb = GetStringBuilder();
            try
            {
                foreach (var metricInstrumentClass in metricInstrumentClasses)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    _ = sb.Append(GenMetricInstrumentFactory(metricInstrumentClass));
                }

                string str = $@"
    public static partial class Metric
    {{
        private static readonly global::Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions.MdmMetricFlags MdmFlags =
            global::Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions.MdmMetricFlags.CumulativeMetricDefault |
            global::Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions.MdmMetricFlags.IncludeDefaultDimensions;

        private static readonly global::System.Collections.Concurrent.ConcurrentDictionary<string, global::Microsoft.R9.Extensions.Meter.ICounterD> _longCounterMetrics = new ();
        {sb}
    }}
";
                if (string.IsNullOrWhiteSpace(nspace))
                {
                    return str;
                }

                return $@"
namespace {nspace}
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

        private string GenType(MetricInstrumentClass metricInstrumentClass)
        {
            var sb = GetStringBuilder();
            try
            {
                foreach (var metricInstrumentMethod in metricInstrumentClass.Methods)
                {
                    _ = sb.Append(GenCounterClass(metricInstrumentMethod));
                }

                return sb.ToString();
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

                return sb.ToString();
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
        [{_generatedCodeAttribute}]
        public static partial {metricInstrumentMethod.MetricName} Create{metricInstrumentMethod.MetricName}(global::{meterParam.Type} {meterParam.Name}{GenStaticParameters(metricInstrumentMethod)})
        {{
            string metricName = ""{metricInstrumentMethod.MetricName}"";
            if (_longCounterMetrics.TryGetValue(metricName, out var counterMetric))
            {{
                return (counterMetric as {metricInstrumentMethod.MetricName})!;
            }}

            var metric = _longCounterMetrics.GetOrAdd(metricName, (key) => 
                {{
                    var counter = meter.CreateCounter(metricName{GenStaticAndDynamicKeysAndValues(metricInstrumentMethod)});
                    return new {metricInstrumentMethod.MetricName}(counter as Microsoft.R9.Extensions.Meter.ICounter{metricInstrumentMethod.StaticDimensionsKeys.Count + metricInstrumentMethod.DynamicDimensionsKeys.Count}D);
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
                    _ = sb.Append(GenCounterMethod(metricInstrumentMethod));
                }

                return $@"
    [{_generatedCodeAttribute}]
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
            string counterInterfaceName = $"Microsoft.R9.Extensions.Meter.ICounter{metricInstrumentMethod.StaticDimensionsKeys.Count + metricInstrumentMethod.DynamicDimensionsKeys.Count}D";
            return $@"
    [{_generatedCodeAttribute}]
    public class {metricInstrumentMethod.MetricName} : Microsoft.R9.Extensions.Meter.ICounterD
    {{
        {counterInterfaceName} _counter;
        public {metricInstrumentMethod.MetricName}({counterInterfaceName} counter)
        {{
            _counter = counter;
        }}
        {GenClassMethods(metricInstrumentMethod)}
    }}
    ";
        }

        private string GenCounterMethod(MetricInstrumentMethod metricInstrumentMethod)
        {
            var p = metricInstrumentMethod.AllParameters[0];
            string meterArg = p.Name;

            return $@"
        [{_generatedCodeAttribute}]
        public static partial {metricInstrumentMethod.InstrumentClassType} {metricInstrumentMethod.Name}({GenParameters(metricInstrumentMethod)})
        {{
            return GeneratedCounterMetricFactory.Create{metricInstrumentMethod.MetricName}({meterArg}{GenStaticParameters(metricInstrumentMethod, false)});
        }}
        ";
        }

        private string GenParameters(MetricInstrumentMethod metricInstrumentMethod)
        {
            var sb = GetStringBuilder();
            try
            {
                foreach (var p in metricInstrumentMethod.AllParameters)
                {
                    if (p == metricInstrumentMethod.AllParameters[0])
                    {
                        _ = sb.Append("global::");
                    }
                    else
                    {
                        _ = sb.Append(", ");
                    }

                    _ = sb.Append($"{p.Type} {p.Name}");
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
                foreach (var dimensionParam in metricInstrumentMethod.StaticDimensionParameters)
                {
                    _ = sb.Append(", ");

                    if (includeType)
                    {
                        _ = sb.Append($"string ");
                    }

                    _ = sb.Append(dimensionParam.Name);
                }

                return sb.ToString();
            }
            finally
            {
                ReturnStringBuilder(sb);
            }
        }

        private string GenStaticAndDynamicKeysAndValues(MetricInstrumentMethod metricInstrumentMethod)
        {
            var sb = GetStringBuilder();
            try
            {
                string[] dimensionKeys = new string[metricInstrumentMethod.StaticDimensionsKeys.Count + 1];
                metricInstrumentMethod.StaticDimensionsKeys.CopyTo(dimensionKeys);
                int i = 0;
                foreach(var dimParam in metricInstrumentMethod.StaticDimensionParameters)
                {
                    _ = sb.Append(", ");
                    _ = sb.Append(@$"""{dimensionKeys[i]}"", {dimParam.Name}");
                    i++;
                }

                foreach (var param in metricInstrumentMethod.DynamicDimensionsKeys)
                {
                    _ = sb.Append(", ");
                    _ = sb.Append(@$"""{param}"", string.Empty");
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
                foreach (var dimension in metricInstrumentMethod.DynamicDimensionsKeys)
                {
                    if (includeType)
                    {
                        _ = sb.Append($", string {GetSanitizedParamName(dimension)}");
                    }
                    else
                    {
                        _ = sb.Append($", {GetSanitizedParamName(dimension)}");
                    }
                }

                return sb.ToString();
            }
            finally
            {
                ReturnStringBuilder(sb);
            }
        }

        private string GenStaticKeyNames(MetricInstrumentMethod metricInstrumentMethod)
        {
            var sb = GetStringBuilder();
            try
            {
                foreach (var dimension in metricInstrumentMethod.StaticDimensionsKeys)
                {
                    _ = sb.Append(", ");
                    _ = sb.Append(@$"""{dimension}""");
                }

                return sb.ToString();
            }
            finally
            {
                ReturnStringBuilder(sb);
            }
        }

        private string GenDynamicKeyNames(MetricInstrumentMethod metricInstrumentMethod)
        {
            var sb = GetStringBuilder();
            try
            {
                foreach (var dimension in metricInstrumentMethod.DynamicDimensionsKeys)
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
                foreach (var staticDimensionParam in metricInstrumentMethod.StaticDimensionParameters)
                {
                    _ = sb.Append($@"
            _staticValArray[{index}] = {staticDimensionParam.Name};");

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
                int i = 0;
                for (; i < metricInstrumentMethod.StaticDimensionsKeys.Count; i++)
                {
                    if (i > 0)
                    {
                        _ = sb.Append(", ");
                    }

                    _ = sb.Append($"_staticValArray[{i}]");
                }

                foreach (var dimension in metricInstrumentMethod.DynamicDimensionsKeys)
                {
                    if (i > 0)
                    {
                        _ = sb.Append(", ");
                    }

                    _ = sb.Append(GetSanitizedParamName(dimension));
                    i++;
                }

                string methodName = metricInstrumentMethod.InstrumentType == InstrumentType.Counter ? "Add" : "Record";

                return $@"
        public void {methodName}(long value{GenDynamicParameters(metricInstrumentMethod)})
        {{
            _counter.{methodName}(value{GenDynamicParameters(metricInstrumentMethod, false)});
        }}";
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
    }
}
