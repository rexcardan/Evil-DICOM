using System.Reflection;

namespace EvilDICOM.Core.Helpers
{
    public static class Constants
    {
        public static string EVIL_DICOM_IMP_UID = "1.2.598.0.1.2851334.2.1865.1";
        public static string EVIL_DICOM_IMP_VERSION = new AssemblyName(Assembly.GetCallingAssembly().FullName).Version.ToString();
        //APPLICATION CONTEXT
        public static string DEFAULT_APPLICATION_CONTEXT = "1.2.840.10008.3.1.1.1";
    }
}