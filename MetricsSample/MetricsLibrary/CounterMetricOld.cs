using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
using Microsoft.R9.Extensions.Meter;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsLibrary
{
    public class CounterMetricOld<TDimensionValues>
        where TDimensionValues : IFixedStringsArray
    {
        private readonly (string key, string value)[] _fixedDimensions;
        private readonly Dictionary<string, int> _keyPositionMap = new();
        private readonly ISet<string> _allowedKeys;
        private readonly TDimensionValues _defaultDimensionValues;

        internal IMdmCumulativeMetric<TDimensionValues, ulong> CumulativeMetric { get; }

        public CounterMetricOld(IMdmCumulativeMetric<TDimensionValues, ulong> cumulativeMetric, 
            ISet<string> allowedKeys, IList<(string key, string value)>? fixedDimensions)
        {
            CumulativeMetric = cumulativeMetric ?? throw new ArgumentNullException(nameof(cumulativeMetric));
            _allowedKeys = allowedKeys ?? throw new ArgumentNullException(nameof(allowedKeys));
            var defaultDimensionsAreNullOrEmpty = fixedDimensions == null || fixedDimensions.Count == 0;

            if (!defaultDimensionsAreNullOrEmpty)
            {
                CheckIfNonAllowedKeysArePresentInDimensionSet(fixedDimensions!, nameof(fixedDimensions), allowedKeys);
            }

            var completeFixedDimensions = fixedDimensions.AddMissingValuesFromKeys(_allowedKeys, defaultDimensionsAreNullOrEmpty);
            Array.Sort(completeFixedDimensions);

            _defaultDimensionValues = (TDimensionValues)GetDimensionValues(completeFixedDimensions);
            _fixedDimensions = completeFixedDimensions;

            for (var i = 0; i < _fixedDimensions.Length; i++)
            {
                _keyPositionMap.Add(_fixedDimensions[i].key, i);
            }
        }

        public void Add(long value, IList<(string key, string value)>? dimensions)
        {
            if (dimensions == null || dimensions.Count == 0)
            {
                AddMetricWithDimensionValues(value, _defaultDimensionValues);
                return;
            }

            CheckIfNonAllowedKeysArePresentInDimensionSet(dimensions, nameof(dimensions), _allowedKeys);

            var buffer = ArrayPool<(string key, string value)>.Shared.Rent(_allowedKeys.Count);
            _ = _fixedDimensions!.MergeWith(dimensions, _keyPositionMap, buffer);
            var withoutNulls = buffer.AsSpan().Slice(0, _allowedKeys.Count);
            var dimensionValues = (TDimensionValues)GetDimensionValues(withoutNulls);

            // No need to clear buffer since we always overwrite the values we are going to read.
            ArrayPool<(string key, string value)>.Shared.Return(buffer);
            AddMetricWithDimensionValues(value, dimensionValues);
        }

        private static void CheckIfNonAllowedKeysArePresentInDimensionSet(IList<(string key, string value)> dimensionsToCheck,
            string nameOfArgumentToCheck, ISet<string> allowedKeys)
        {
            foreach (var (key, _) in dimensionsToCheck)
            {
                if (!allowedKeys.Contains(key))
                {
                    throw new ArgumentException($"{nameOfArgumentToCheck} contains a key: {key} that is not present in allowed keys set.");
                }
            }
        }

        /// <summary>
        /// Adds new value to associated metric.
        /// </summary>
        /// <param name="value">Value that metric will be incremented or decremented by.</param>
        /// <param name="dimensionValues">Readonly dimension values.</param>
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

        private IFixedStringsArray GetDimensionValues(Span<(string key, string value)> dimensions = new())
        {
            return dimensions.Length switch
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
