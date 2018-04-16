using Microsoft.ApplicationInsights;

namespace PI.ApplicationInsights.Hangfire
{
    public class HangfireMetricOptions
    {
        public string MetricPrefix { get; set; }
        public TelemetryClient TelemetryClient { get; set; }
        public int? PushInterval { get; set; }
    }
}
