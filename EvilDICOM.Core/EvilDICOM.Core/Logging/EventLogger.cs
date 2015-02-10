using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Logging
{
    public class EventLogger
    {
        //LOG
        public delegate void LogHandler(string toLog, LogPriority priority);

        public event LogHandler LogRequested;

        public void Log(string toLogMessage, params object[] args)
        {
            Log(toLogMessage, LogPriority.NORMAL, args);
        }

        public void Log(object toLogMessage)
        {
            Log(toLogMessage.ToString(), LogPriority.NORMAL);
        }

        public void Log(string toLogMessage, LogPriority priority, params object[] args)
        {
            if (LogRequested != null)
            {
                LogRequested(string.Format(toLogMessage, args), priority);
            }
        }
    }
}