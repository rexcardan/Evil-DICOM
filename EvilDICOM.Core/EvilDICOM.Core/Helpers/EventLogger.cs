using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Helpers
{
    public class EventLogger
    {
        //SINGLETON pattern
        private static EventLogger instance;

        private EventLogger() { }

        public static EventLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventLogger();
                }
                return instance;
            }
        }

        //LOG
        public delegate void LogHandler(string toLog);
        public event LogHandler LogRequested;

        public void RaiseToLogEvent(string toLogMessage, params object[] args)
        {
            if (LogRequested != null) { LogRequested(string.Format(toLogMessage, args)); }
        }
    }
}
