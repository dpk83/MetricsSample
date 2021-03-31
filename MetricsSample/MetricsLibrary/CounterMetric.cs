using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsLibrary
{
    public class CounterMetric
    {
        private string _keyHashStr;
        private string _valueHashStr;

        public CounterMetric(Dimensions dimensions)
        {
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
