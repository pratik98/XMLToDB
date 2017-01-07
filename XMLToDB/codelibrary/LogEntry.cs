using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLToDB.codelibrary
{
    public enum LoggingEventType { Debug, Information, Warning, Error, Fatal };
     class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {
            //Requires.IsNotNullOrEmpty(message, "message");
            //Requires.IsValidEnum(severity, "severity");
            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }
    }
}
