# PI.ApplicationInsights.Hangfire

![Build status from VSTS](https://pi-applications-dk.visualstudio.com/_apis/public/build/definitions/8c43066a-ced2-41f9-822b-b5a7154a9b31/56/badge)
![NuGet](https://img.shields.io/nuget/v/PI.ApplicationInsights.Hangfire.svg)
![NuGet Pre Release](https://img.shields.io/nuget/vpre/PI.ApplicationInsights.Hangfire.svg)

Use this package if you want to monitor status for a Hangfire instance with Azure Application Insights.

The following metrics will be pushed to Azure Application Insights:
- Enqueued
- Scheduled
- Failed
- Processing
- Servers

## Example of usage
```csharp
HangfireMetric.Use(new HangfireMetricOptions
{
    MetricPrefix = "hangfire",
    PushInterval = new TimeSpan(0, 0, 30),
    TelemetryClient = aiInstance,
    HangfireMonitoringApi = hangfireInstance
});
```

## Options
Property | Required | Description
--- | --- | ---
TelemetryClient | **yes** | Application Insights instance
HangfireMonitoringApi  | **yes** | Hangfire MonitoringApi instance
MetricPrefix  | no | Prefix for metrics names. Default is *hangfire*
PushInterval | no | Push interval. Default is every minute

## Metrics
Name | Description
--- | ---
[*MetricPrefix*]-enqueued | Count of enqueued jobs
[*MetricPrefix*]-scheduled | Count of scheduled jobs
[*MetricPrefix*]-failed | Count of failed jobs
[*MetricPrefix*]-processing | Count of processing jobs
[*MetricPrefix*]-servers | Count of servers

### Example of metrics
If you use this package with a prefix called **hangfire** then you will get the following metrics:
- *hangfire*-enqueued
- *hangfire*-scheduled
- *hangfire*-failed
- *hangfire*-processing
- *hangfire*-servers

## Azure Monitor Alert
To setup an Azure Monitor Alert the following search query can be used as inspiration.

````sql
customMetrics 
| where name == "hangfire-failed" 
| project AggregatedValue = value, bin(timestamp, 1m)  
````