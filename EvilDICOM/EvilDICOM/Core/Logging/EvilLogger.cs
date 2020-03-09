using Microsoft.Extensions.Logging;

namespace EvilDICOM.Core.Logging
{
    /// <summary>
    ///     The singleton instance of a logging system for the core Evil DICOM operations. Can subscribe, to
    ///     see this stream
    /// </summary>
    public sealed class EvilLogger
    {
        private static ILoggerFactory _Factory = null;

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_Factory == null)
                {
                    _Factory = new LoggerFactory();
                }
                return _Factory;
            }
            set { _Factory = value; }
        }
    }
}