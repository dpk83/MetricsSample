using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsLibrary
{
    public class CounterMetricWithSortedDimensions
    {
        SortedDimensions _dimensions;

        public CounterMetricWithSortedDimensions(SortedDimensions dimensions)
        {
            _dimensions = dimensions;
        }

        public void AddCounter(int value, SortedDimensions dimensions)
        {
            if (dimensions.KeyHashStr != _dimensions.KeyHashStr)
            {
                throw new ArgumentException("keyset provided is different then the keys present for counter metrics");
            }

            if (dimensions.ValueHashStr == _dimensions.ValueHashStr)
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
