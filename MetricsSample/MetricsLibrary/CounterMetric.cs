using System;

namespace MetricsLibrary
{
    public class CounterMetric
    {
        Dimensions _dimensions;

        public CounterMetric(Dimensions dimensions)
        {
            _dimensions = dimensions;
        }

        public void AddCounter(int value, Dimensions dimensions)
        {
            if (dimensions.KeysHashCode != _dimensions.KeysHashCode)
            {
                throw new ArgumentException("keyset provided is different then the keys present for counter metrics");
            }

            if (dimensions.HashCode == _dimensions.HashCode)
            {
                Add(value);
            }
            else
            {
                // Lookup the counter with the dimension in the dictionary
                // if doesn't exists add it
                // Call the Add method on that counter object
            }
        }

        internal void Add(int value)
        {
            // Perform internal logic of incrementing the counter by the value
        }
    }
}
