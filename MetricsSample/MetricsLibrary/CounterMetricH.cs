using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
using System;

namespace MetricsLibrary
{
    public class CounterMetricH<TDimensionValues>
        where TDimensionValues : IFixedStringsArray
    {
        private int _keyHashCode;
        private int _valueHashCode;

        private TDimensionValues _defaultDimensionValues;

        internal IMdmCumulativeMetric<TDimensionValues, ulong> CumulativeMetric { get; }

        public CounterMetricH(IMdmCumulativeMetric<TDimensionValues, ulong> cumulativeMetric, HDimensions dimensions)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            if (dimensions != null)
            {
                _keyHashCode = dimensions.KeyHashCode;
                _valueHashCode = dimensions.HashCode;
            }
        }

        public void Add(int value, HDimensions? dimensions)
        {
            if (dimensions != null)
            {
                if (dimensions.KeyHashCode != _keyHashCode)
                {
                    throw new ArgumentException("keyset provided is different then the keys present for counter metrics");
                }

                if (dimensions.HashCode != _valueHashCode)
                {
                    _valueHashCode = dimensions.HashCode;
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

        private IFixedStringsArray GetDimensionValues(HDimensions dimensions)
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
