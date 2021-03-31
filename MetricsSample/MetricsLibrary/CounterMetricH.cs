using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsLibrary
{
    public class CounterMetricH
    {
        private int _keyHashCode;
        private int _valueHashCode;

        public CounterMetricH(HDimensions dimensions)
        {
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

                    // Do any other necessary updates
                }
            }

            AddInternal(value);
        }

        private void AddInternal(int value)
        {
            // Perform internal logic of incrementing the counter by the value
        }
    }
}
