# PI.ApplicationInsights.Hangfire

![Build status from VSTS](https://pi-applications-dk.visualstudio.com/_apis/public/build/definitions/8c43066a-ced2-41f9-822b-b5a7154a9b31/56/badge)

Use this package if you want to monitor status for a Hangfire instance with Azure Application Insights.

The following metrics will be pushed to Azure Application Insights:
- Enqueued
- Scheduled
- Failed
- Processing
- Servers

## Example
```
HangfireMetric.Use(new HangfireMetricOptions
{
    MetricPrefix = "myprefix",
    PushInterval = 120000,
    TelemetryClient = aiClient
});
```

## Options
Property | Required | Description
--- | --- | ---
TelemetryClient | **yes** | Application Insights instance
MetricPrefix  | no | Prefix for metrics name
PushInterval | no | Push interval in milliseconds. Default is 60000 (every minute)

## Metrics
Name | Description
--- | ---
[*MetricPrefix*]Enqueued | Count of enqueued jobs
[*MetricPrefix*]Scheduled | Count of scheduled jobs
[*MetricPrefix*]Failed | Count of failed jobs
[*MetricPrefix*]Processing | Count of processing jobs
[*MetricPrefix*]Servers | Count of servers

### Example of metrics
If you use this package with a prefix called **MyWebshop** then you will get the following metrics:
- *MyWebshop*Enqueued
- *MyWebshop*Scheduled
- *MyWebshop*Failed
- *MyWebshop*Processing
- *MyWebshop*Servers