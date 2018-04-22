using Microsoft.ApplicationInsights;
using System;

namespace PI.ApplicationInsights.Hangfire
{
    public class HangfireMetricOptions
    {
        public string MetricPrefix { get; set; }
        public TelemetryClient TelemetryClient { get; set; }
        public TimeSpan PushInterval { get; set; }
    }
}
