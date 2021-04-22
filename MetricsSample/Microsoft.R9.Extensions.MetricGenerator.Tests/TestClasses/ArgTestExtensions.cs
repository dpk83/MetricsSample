using Microsoft.R9.Extensions.Meter;
using Microsoft.R9.Extensions.MetricUtilities;

namespace Microsoft.R9.Extensions.MetricGenerator.Tests.TestClasses
{
    public static partial class ArgTestExtensions
    {
        [Int64CounterMetricAttribute("Counter")]
        public static partial Counter CreateCounter(IMeter meter);

        [Int64CounterMetricAttribute("CounterS1D0", "s1")]
        public static partial CounterS1D0 CreateCounterS1D0(IMeter meter, string s1);

        [Int64CounterMetricAttribute("CounterS2D0", "s1,s2")]
        public static partial CounterS2D0 CreateCounterS2D0(IMeter meter, string s1, string s2);

        [Int64CounterMetricAttribute("CounterS3D0", "s1,s2,s3")]
        public static partial CounterS3D0 CreateCounterS3D0(IMeter meter, string s1, string s2, string s3);

        [Int64CounterMetricAttribute("CounterS0D1", null, "d1")]
        public static partial CounterS0D1 CreateCounterS0D1(IMeter meter);

        [Int64CounterMetricAttribute("CounterS0D2", "", "d1, d2")]
        public static partial CounterS0D2 CreateCounterS0D2(IMeter meter);

        [Int64CounterMetricAttribute("CounterS1D1", "s1", "d1")]
        public static partial CounterS1D1 CreateCounterS1D1(IMeter meter, string s1);

        [Int64CounterMetricAttribute("CounterS3D2", "s1,s2,s3", "d1,d2")]
        public static partial CounterS3D2 CreateCounterS3D2(IMeter meter, string s1, string s2, string s3);

        [Int64CounterMetricAttribute("CounterS8D2", "s1,s2,s3,s4,s5,s6,s7,s8", "d1,d2")]
        public static partial CounterS8D2 CreateCounterS8D2(IMeter meter, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8);
    }
}
