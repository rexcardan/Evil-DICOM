#region

using EvilDICOM.Core.Enums;
using System;

#endregion

namespace EvilDICOM.Core.Logging
{
    public class EventLogger
    {
        //LOG
        public delegate void LogHandler(string toLog, LogPriority priority);

        public event LogHandler LogRequested;

        public void Log(string toLogMessage, params object[] args)
        {
            try
            {
                Log(toLogMessage, LogPriority.NORMAL, args);
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
        }

        public void Log(object toLogMessage)
        {
            Log(toLogMessage.ToString(), LogPriority.NORMAL);
        }

        public void Log(string toLogMessage, LogPriority priority, params object[] args)
        {
            try
            {
                if (LogRequested != null)
                    LogRequested(string.Format(toLogMessage, args), priority);
            }
            catch (Exception e)
            {
                Log(e.Message);
            }

        }
    }
}