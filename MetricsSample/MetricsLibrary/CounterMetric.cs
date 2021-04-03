using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
using Microsoft.R9.Extensions.Meter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsLibrary
{
    public class CounterMetric<TDimensionValues>
        where TDimensionValues : IFixedStringsArray
    {
        private string _keyHashStr;
        private string _valueHashStr;

        private TDimensionValues _defaultDimensionValues;

        internal IMdmCumulativeMetric<TDimensionValues, ulong> CumulativeMetric { get; }

        public CounterMetric(IMdmCumulativeMetric<TDimensionValues, ulong> cumulativeMetric, Dimensions dimensions)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            if (dimensions != null)
            {
                _keyHashStr = dimensions.KeyHashStr;
                _valueHashStr = dimensions.ValueHashStr;
            }
        }

        public void Add(int value, Dimensions? dimensions)
        {
            if (dimensions != null)
            {
                if (dimensions.KeyHashStr != _keyHashStr)
                {
                    throw new ArgumentException("keyset provided is different then the keys present for counter metrics");
                }

                if (dimensions.ValueHashStr != _valueHashStr)
                {
                    _valueHashStr = dimensions.ValueHashStr;
                    _defaultDimensionValues = (TDimensionValues)GetDimensionValues(dimensions);
                }
            }

            AddMetricWithDimensionValues(value, _defaultDimensionValues);
        }

        private void AddMetricWithDimensionValues(long value, TDimensionValues dimensionValues)
        {
            if (value == 0)
            {
                return;
            }

            _ = value > 0
                ? CumulativeMetric.IncrementBy((ulong)value, dimensionValues)
                : CumulativeMetric.DecrementBy((ulong)(value * -1), dimensionValues);
        }

        private IFixedStringsArray GetDimensionValues(Dimensions dimensions)
        {
            return dimensions.Count switch
            {
                3 => DimensionValues.Create(
                    dimensions[0].Item2,
                    dimensions[1].Item2,
                    dimensions[2].Item2),
                5 => DimensionValues.Create(
                    dimensions[0].Item2,
                    dimensions[1].Item2,
                    dimensions[2].Item2,
                    dimensions[3].Item2,
                    dimensions[4].Item2),
                10 => DimensionValues.Create(
                    dimensions[0].Item2,
                    dimensions[1].Item2,
                    dimensions[2].Item2,
                    dimensions[3].Item2,
                    dimensions[4].Item2,
                    dimensions[5].Item2,
                    dimensions[6].Item2,
                    dimensions[7].Item2,
                    dimensions[8].Item2,
                    dimensions[9].Item2),
                _ => throw new ArgumentOutOfRangeException(nameof(dimensions))
            };
        }
    }
}