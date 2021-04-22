using Microsoft.R9.Extensions.Meter;
using Microsoft.R9.Extensions.MetricGenerator.Tests.TestClasses;
using Xunit;

namespace Microsoft.R9.Extensions.MetricGenerator.Tests
{
    public class MetricsGeneratorTests
    {
        [Fact]
        public void ArgTests()
        {
            string meterName = "meter";
            string monitoringAccount = "testAccout";
            MdmMetricFactory mdmMetricFactory = new MdmMetricFactory();

            IMeter meter = new GenevaMeter(meterName, mdmMetricFactory, monitoringAccount);

            Counter counter = ArgTestExtensions.CreateCounter(meter);

        }
    }
}
