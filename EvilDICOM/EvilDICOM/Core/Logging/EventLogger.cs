using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Logging
{
    /// <summary>
    /// Class EventLogger.
    /// </summary>
    public class EventLogger
    {
        //LOG
        /// <summary>
        /// Delegate LogHandler
        /// </summary>
        /// <param name="toLog">To log.</param>
        /// <param name="priority">The priority.</param>
        public delegate void LogHandler(string toLog, LogPriority priority);

        /// <summary>
        /// Occurs when [log requested].
        /// </summary>
        public event LogHandler LogRequested;

        /// <summary>
        /// Logs the specified to log message.
        /// </summary>
        /// <param name="toLogMessage">To log message.</param>
        /// <param name="args">The arguments.</param>
        public void Log(string toLogMessage, params object[] args)
        {
            Log(toLogMessage, LogPriority.NORMAL, args);
        }

        /// <summary>
        /// Logs the specified to log message.
        /// </summary>
        /// <param name="toLogMessage">To log message.</param>
        public void Log(object toLogMessage)
        {
            Log(toLogMessage.ToString(), LogPriority.NORMAL);
        }

        /// <summary>
        /// Logs the specified to log message.
        /// </summary>
        /// <param name="toLogMessage">To log message.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="args">The arguments.</param>
        public void Log(string toLogMessage, LogPriority priority, params object[] args)
        {
            if (LogRequested != null)
            {
                LogRequested(string.Format(toLogMessage, args), priority);
            }
        }
    }
}