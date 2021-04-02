using System;

namespace Microsoft.R9.Extensions.MetricUtilities
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Int64CounterMetricAttribute : Attribute
    {
        public Int64CounterMetricAttribute(int eventId)
        {
            EventId = eventId;
        }

        /// <summary>
        /// Gets the logging event id for the logging method.
        /// </summary>
        public int EventId { get; }

        /// <summary>
        /// Gets or sets the logging event name for the logging method.
        /// </summary>
        /// <remarks>
        /// This will equal the method name if not specified.
        /// </remarks>
        public string? EventName { get; set; }
    }
}
