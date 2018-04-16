using Hangfire;
using Hangfire.Storage;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PI.ApplicationInsights.Hangfire
{
    public sealed class HangfireMetric : IDisposable
    {
        private bool _isDisposed = false;        
        private TelemetryClient _telemetryClient { get; set; }
        private IMonitoringApi _hangfireApi { get; set; }
        private string _metricPrefix { get; set; }

        public int PushInterval { get; set; }

        public HangfireMetric(HangfireMetricOptions options)
        {
            _metricPrefix = options.MetricPrefix;
            _telemetryClient = options.TelemetryClient;

            _hangfireApi = JobStorage.Current.GetMonitoringApi();

            PushInterval = options.PushInterval ?? 60000;

            Task.Run(MetricLoopAsync);
        }

        private async Task MetricLoopAsync()
        {
            while (!_isDisposed)
            {
                try
                {
                    // Fetch stats from Hangfire
                    var stats = _hangfireApi.GetStatistics();

                    // Create metrics and push to server
                    var telemetryEnqueued = new MetricTelemetry(_metricPrefix + "Enqueued", stats.Enqueued);
                    _telemetryClient.TrackMetric(telemetryEnqueued);

                    var telemetryScheduled = new MetricTelemetry(_metricPrefix + "Scheduled", stats.Scheduled);
                    _telemetryClient.TrackMetric(telemetryScheduled);

                    var telemetryFailed = new MetricTelemetry(_metricPrefix + "Failed", stats.Failed);
                    _telemetryClient.TrackMetric(telemetryFailed);

                    var telemetryProcessing = new MetricTelemetry(_metricPrefix + "Processing", stats.Processing);
                    _telemetryClient.TrackMetric(telemetryProcessing);

                    var telemetryServers = new MetricTelemetry(_metricPrefix + "Servers", stats.Servers);
                    _telemetryClient.TrackMetric(telemetryServers);

                    // Wait for next push
                    await Task.Delay(PushInterval).ConfigureAwait(continueOnCapturedContext: false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    // Wait for next push
                    await Task.Delay(PushInterval).ConfigureAwait(continueOnCapturedContext: false);
                }
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
        }

        public static HangfireMetric Use(HangfireMetricOptions options)
        {
            return new HangfireMetric(options);
        }
    }
}
