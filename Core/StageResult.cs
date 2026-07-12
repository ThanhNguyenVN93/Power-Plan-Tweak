using System;

namespace frm_PowerPlanTweak.Core
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Success
    }

    public sealed class LogEntry
    {
        public DateTime Timestamp { get; }
        public LogLevel Level { get; }
        public string Message { get; }

        public LogEntry(LogLevel level, string message)
        {
            Timestamp = DateTime.Now;
            Level = level;
            Message = message;
        }
    }

    public sealed class TweakResult
    {
        public bool Success { get; set; }
        public bool RolledBack { get; set; }
        public string FailureReason { get; set; }
        public double? IdlePerformancePercent { get; set; }
        public double? LoadPerformancePercent { get; set; }
    }
}
