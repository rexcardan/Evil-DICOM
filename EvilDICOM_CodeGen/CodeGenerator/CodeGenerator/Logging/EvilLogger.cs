using System;

namespace EvilDICOM.Core.Logging
{
    /// <summary>
    ///     The singleton instance of a logging system for the core Evil DICOM operations. Can subscribe, to
    ///     see this stream
    /// </summary>
    public sealed class EvilLogger : EventLogger
    {
        private static volatile EvilLogger instance;
        private static readonly object locker = new Object();

        private EvilLogger()
        {
        }

        public static EvilLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = new EvilLogger();
                    }
                }

                return instance;
            }
        }
    }
}