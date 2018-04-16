# PI.ApplicationInsights.Hangfire

![Build status from VSTS](https://pi-applications-dk.visualstudio.com/_apis/public/build/definitions/8c43066a-ced2-41f9-822b-b5a7154a9b31/56/badge)

Use this package is you want to monitor status for a Hangfire instance.

The following metrics will be pushed to AI:
* Enqueued
* Scheduled
* Failed
* Processing
* Servers

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
