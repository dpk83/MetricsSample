using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsLibrary
{
    public class CounterMetricOld
    {
        private readonly (string key, string value)[] _fixedDimensions;
        private readonly Dictionary<string, int> _keyPositionMap = new();
        private readonly ISet<string> _allowedKeys;

        public CounterMetricOld(ISet<string> allowedKeys, IList<(string key, string value)>? fixedDimensions)
        {
            _allowedKeys = allowedKeys ?? throw new ArgumentNullException(nameof(allowedKeys));
            var defaultDimensionsAreNullOrEmpty = fixedDimensions == null || fixedDimensions.Count == 0;

            if (!defaultDimensionsAreNullOrEmpty)
            {
                CheckIfNonAllowedKeysArePresentInDimensionSet(fixedDimensions!, nameof(fixedDimensions), allowedKeys);
            }

            var completeFixedDimensions = fixedDimensions.AddMissingValuesFromKeys(_allowedKeys, defaultDimensionsAreNullOrEmpty);
            Array.Sort(completeFixedDimensions);

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
                AddMetricWithDimensionValues(value);
                return;
            }

            CheckIfNonAllowedKeysArePresentInDimensionSet(dimensions, nameof(dimensions), _allowedKeys);

            var buffer = ArrayPool<(string key, string value)>.Shared.Rent(_allowedKeys.Count);
            _ = _fixedDimensions!.MergeWith(dimensions, _keyPositionMap, buffer);
            var withoutNulls = buffer.AsSpan().Slice(0, _allowedKeys.Count);

            // No need to clear buffer since we always overwrite the values we are going to read.
            ArrayPool<(string key, string value)>.Shared.Return(buffer);
            AddMetricWithDimensionValues(value);
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
        private void AddMetricWithDimensionValues(long value)
        {
        }
    }
}
