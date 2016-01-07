using System;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.Helpers
{
    public class DicomInfoHelper
    {
        /// <summary>
        /// 获取AccessionNumber
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetAccessionNumber(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.ACCESSION_NUMBER);
        }

        /// <summary>
        /// 获取OtherPatientNames属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetOtherPatientName(DICOMObject dcm)
        {
            string strName = GetStringTypeTag(dcm, TagHelper.OTHER_PATIENT_NAMES);
            return GetStringTypeTag(dcm, TagHelper.OTHER_PATIENT_NAMES);
        }

        /// <summary>
        /// 获取PatientAge属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetPatientAge(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.PATIENT_AGE);
        }


        /// <summary>
        /// 获取PatientID属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetPatientID(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.PATIENT_ID);
        }

        /// <summary>
        /// 获取PatientName属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetPatientName(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.PATIENT_NAME);
        }

        /// <summary>
        /// 获取PatientSex属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetPatientSex(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.PATIENT_SEX);
        }

        /// <summary>
        /// 获取StudyDate属性，格式:yyyyMMdd
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetStudyDate(DICOMObject dcm)
        {
            return GetDateTypeTag(dcm, TagHelper.STUDY_DATE);
        }

        /// <summary>
        /// 获取StudyTime属性，格式：HHmmss.ffffff
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetStudyTime(DICOMObject dcm)
        {
            return GetTimeTypeTag(dcm, TagHelper.STUDY_TIME);
        }

        /// <summary>
        /// 获取SeriesNumber属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSeriesNumber(DICOMObject dcm)
        {
            return GetInt32TypeTag(dcm, TagHelper.SERIES_NUMBER);
        }

        /// <summary>
        /// 获取SeriesDescription属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSeriesDescription(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.SERIES_DESCRIPTION);
        }

        /// <summary>
        /// 获取SeriesDate属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSeriesDate(DICOMObject dcm)
        {
            return GetDateTypeTag(dcm, TagHelper.SERIES_DATE);
        }

        /// <summary>
        /// 获取SeriesTime属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSeriesTime(DICOMObject dcm)
        {
            return GetTimeTypeTag(dcm, TagHelper.SERIES_TIME);
        }

        /// <summary>
        /// 获取检查设备类型
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetModality(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.MODALITY);
        }

        /// <summary>
        /// 获取AcquisitionTime属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetAcquisitionTime(DICOMObject dcm)
        {
            return GetTimeTypeTag(dcm, TagHelper.ACQUISITION_TIME);
        }

        /// <summary>
        /// 获取Rows
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetRows(DICOMObject dcm)
        {
            return GetUInt16TypeTag(dcm, TagHelper.ROWS);
 
        }

        /// <summary>
        /// 获取Columns
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetColumns(DICOMObject dcm)
        {
            return GetUInt16TypeTag(dcm, TagHelper.COLUMNS);
        }

        /// <summary>
        /// 获取BitsStored属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetBitsStored(DICOMObject dcm)
        {
            return GetUInt16TypeTag(dcm, TagHelper.BITS_STORED);
        }

        /// <summary>
        /// 获取ImagePositionPatient属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetImagePositionPatient(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.IMAGE_POSITION_PATIENT);
        }

        /// <summary>
        /// 获取ImageOrientationPatient属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetImageOrientationPatient(DICOMObject dcm)
        {
            
            return GetDoubleTypeTag(dcm, TagHelper.IMAGE_ORIENTATION_PATIENT);
        }

        /// <summary>
        /// 获取ImagerPixelSpacing属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetImagerPixelSpacing(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.IMAGER_PIXEL_SPACING);
        }

        /// <summary>
        /// 获取PixelSpacing属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetPixelSpacing(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.PIXEL_SPACING);
        }

        /// <summary>
        /// 获取 PhotometricInterpretation属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetPhotometricInterpretation(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.PHOTOMETRIC_INTERPRETATION);
        }

        /// <summary>
        /// 获取RescaleIntercept属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetRescaleIntercept(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.RESCALE_INTERCEPT);
        }

        /// <summary>
        /// 获取RescaleSlope属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetRescaleSlope(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.RESCALE_SLOPE);
        }

        /// <summary>
        /// 获取SliceLocation属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSliceLocation(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.SLICE_LOCATION);
        }

        /// <summary>
        /// 获取SliceThickness属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSliceThickness(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.SLICE_THICKNESS);
        }

        /// <summary>
        /// 获取StationName属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetStationName(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.STATION_NAME);
        }

        /// <summary>
        /// 获取WindowCenter属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetWindowCenter(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.WINDOW_CENTER);
        }

        /// <summary>
        /// 获取WindowWidth属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetWindowWidth(DICOMObject dcm)
        {
            return GetDoubleTypeTag(dcm, TagHelper.WINDOW_WIDTH);
        }

        /// <summary>
        /// 获取NumberOfFrames属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetNumberOfFrames(DICOMObject dcm)
        {
            return GetInt32TypeTag(dcm, TagHelper.NUMBER_OF_FRAMES);
        }

        /// <summary>
        /// 获取各帧之间的时间间隔
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetFrameTime(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.FRAME_TIME);
        }

        /// <summary>
        /// 获取InstanceNumber属性
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetInstanceNumber(DICOMObject dcm)
        {
            return GetInt32TypeTag(dcm, TagHelper.INSTANCE_NUMBER);
        }

        /// <summary>
        /// 获取 InstitutionName属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetInstitutionName(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.INSTITUTION_NAME);
        }

        /// <summary>
        /// 获取SOPInstanceUID的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSOPInstanceUID(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.SOPINSTANCE_UID);
        }

        /// <summary>
        /// 获取SeriesInstanceUID的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetSeriesInstanceUID(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.SERIES_INSTANCE_UID);
        }

        /// <summary>
        /// 获取StudyInstanceUID的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetStudyInstanceUID(DICOMObject dcm)
        {
            return GetStringTypeTag(dcm, TagHelper.STUDY_INSTANCE_UID);
        }

        /// <summary>
        /// 获取PixelRepresentation属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetPixelRepresentation(DICOMObject dcm)
        {
            return GetUInt16TypeTag(dcm, TagHelper.PIXEL_REPRESENTATION);
        }

        /// <summary>
        /// 获取HighBit属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetHighBit(DICOMObject dcm)
        {
            return GetUInt16TypeTag(dcm, TagHelper.HIGH_BIT);
        }

        /// <summary>
        /// 获取BitsAllocated属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string GetBitsAllocated(DICOMObject dcm)
        {
            return GetUInt16TypeTag(dcm, TagHelper.BITS_ALLOCATED);
        }

        /// <summary>
        /// 获取日期类型的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetDateTypeTag(DICOMObject dcm, Tag tag)
        {
            string strReturn = string.Empty;

            try
            {
                DICOMData<System.DateTime?> date =
                            dcm.TryGetPublicDataValue<System.DateTime?>(tag, System.DateTime.MinValue);

                if (null != date && null != date.SingleValue)
                {
                    strReturn = date.SingleValue.Value.ToString("yyyyMMdd");
                }

            }
            catch (System.Exception) { }

            return strReturn;
        }

        /// <summary>
        /// 获取时间类型的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetTimeTypeTag(DICOMObject dcm, Tag tag)
        {
            string strReturn = string.Empty;

            try
            {
                DICOMData<System.DateTime?> time =
                            dcm.TryGetPublicDataValue<System.DateTime?>(tag, System.DateTime.MinValue);

                if (null != time && null != time.SingleValue)
                {
                    strReturn = time.SingleValue.Value.ToString("HHmmss.ffffff");
                }

            }
            catch (System.Exception) { }

            return strReturn;
        }

        /// <summary>
        /// 获取Int32类型的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetInt32TypeTag(DICOMObject dcm, Tag tag)
        {
            string strReturn = string.Empty;

            try
            {
                DICOMData<Int32> data =
                    dcm.TryGetPublicDataValue<Int32>(tag, String.Empty);

                strReturn = data.ToString();
            }
            catch (System.Exception) { }

            return strReturn;
        }

        /// <summary>
        /// 获取UInt16类型的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetUInt16TypeTag(DICOMObject dcm, Tag tag)
        {
            string strReturn = string.Empty;

            try
            {
                DICOMData<UInt16> data =
                    dcm.TryGetPublicDataValue<UInt16>(tag, String.Empty);

                strReturn = data.ToString();
            }
            catch (System.Exception) { }

            return strReturn;
        }

        /// <summary>
        /// 获取Double类型的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetDoubleTypeTag(DICOMObject dcm, Tag tag)
        {
            string strReturn = string.Empty;

            try
            {
                DICOMData<Double> data =
                    dcm.TryGetPublicDataValue<Double>(tag, String.Empty);

                strReturn = data.ToString("\\");
            }
            catch (System.Exception) { }

            return strReturn;
        }

        /// <summary>
        /// 获取String类型的属性值
        /// </summary>
        /// <param name="dcm"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetStringTypeTag(DICOMObject dcm, Tag tag)
        {
            string strReturn = string.Empty;

            try
            {
                DICOMData<String> data =
                    dcm.TryGetPublicDataValue<String>(tag, String.Empty);

                strReturn = data.ToString();
            }
            catch (System.Exception) { }

            return strReturn;
        }
    }
}
