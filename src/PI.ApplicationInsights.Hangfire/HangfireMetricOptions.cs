using Hangfire.Storage;
using Microsoft.ApplicationInsights;
using System;

namespace PI.ApplicationInsights.Hangfire
{
    public class HangfireMetricOptions
    {
        public TelemetryClient TelemetryClient { get; set; }
        public IMonitoringApi HangfireMonitoringApi { get; set; }

        public string MetricPrefix { get; set; }
        public TimeSpan PushInterval { get; set; }        
    }
}
