using System.Reflection;

namespace EvilDICOM.Core.Helpers
{
    /// <summary>
    /// Class Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The evil_dicom_imp_ uid
        /// </summary>
        public static string EVIL_DICOM_IMP_UID = "1.2.598.0.1.2851334.2.1865.1";

        /// <summary>
        /// The evil_ dicom_imp_version
        /// </summary>
        public static string EVIL_DICOM_IMP_VERSION =
            new AssemblyName(Assembly.GetCallingAssembly().FullName).Version.ToString();

        //APPLICATION CONTEXT
        /// <summary>
        /// The default_application_ context
        /// </summary>
        public static string DEFAULT_APPLICATION_CONTEXT = "1.2.840.10008.3.1.1.1";
    }
}