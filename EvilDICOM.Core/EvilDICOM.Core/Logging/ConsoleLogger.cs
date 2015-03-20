using System;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Logging
{
    /// <summary>
    /// A simple class for logging to the console. Wraps the Event Logger/EvilLogger class
    /// </summary>
    public class ConsoleLogger
    {
        private EventLogger eventLogger;

        public ConsoleLogger(EventLogger eventLogger)
        {
            this.eventLogger = eventLogger;
            eventLogger.LogRequested += eventLogger_LogRequested;
        }

        private void eventLogger_LogRequested(string toLog, LogPriority priority)
        {
            switch (priority)
            {
                case LogPriority.NORMAL:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(toLog);
                    break;
                case LogPriority.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(toLog);
                    break;
                case LogPriority.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(toLog);
                    break;
            }
        }
    }
}