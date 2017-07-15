#region

using System;
using EvilDICOM.Core.Enums;

#endregion

namespace EvilDICOM.Core.Logging
{
    /// <summary>
    ///     A simple class for logging to the console. Wraps the Event Logger/EvilLogger class
    /// </summary>
    public class ConsoleLogger
    {
        private readonly ConsoleColor _defaultColor;
        private EventLogger eventLogger;

        public ConsoleLogger(EventLogger eventLogger, ConsoleColor color = ConsoleColor.White)
        {
            this.eventLogger = eventLogger;
            eventLogger.LogRequested += eventLogger_LogRequested;
            _defaultColor = color;
        }

        private void eventLogger_LogRequested(string toLog, LogPriority priority)
        {
            switch (priority)
            {
                case LogPriority.NORMAL:
                    Console.ForegroundColor = _defaultColor;
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