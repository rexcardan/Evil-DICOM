using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Image;
using EvilDicom.Components;
using EvilDicom.VR;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace EvilDicom.Helper
{
    public class ImageHelper
    {
        /// <summary>
        /// This method retrieves the relevant image properties from a DICOM file for rendering
        /// the image
        /// </summary>
        /// <param name="df">the DICOM file to be processed</param>
        /// <returns>the image properties of the image</returns>
        public static ImageProperties PullProperties(DICOMFile df)
        {
            ImageProperties props = new ImageProperties();

            //Get Number of Rows
            try
            {
                UnsignedShort rowsObject = df.ROWS as UnsignedShort;
                props.Rows = rowsObject.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find number of rows");
            }

            //Get Number of Columns
            try
            {
                UnsignedShort columnsObject = df.COLUMNS as UnsignedShort;
                props.Columns = columnsObject.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find number of columns");
            }

            //Get Bit Depth
            try
            {
                UnsignedShort bitsAllocated = df.BITS_ALLOCATED as UnsignedShort;
                props.BitsAllocated = bitsAllocated.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find bits allocated");
            }

            //Get Pixel Height and Width  
            try
            {
                DecimalString pixelSpacing = df.PIXEL_SPACING as DecimalString;
                props.PixelHeight = pixelSpacing.Data[0];
                props.PixelWidth = pixelSpacing.Data[1];
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find pixel spacing");
            }

            //Get SliceThickness
            try
            {
                DecimalString sliceThickness = df.SLICE_THICKNESS as DecimalString;
                props.SliceThickness = sliceThickness.Data[0];
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find slice thickness");
            }

            //Get Image Number
            try
            {
                IntegerString imageNumber = df.INSTANCE_NUMBER as IntegerString;
                props.ImageNumber = imageNumber.Data[0];
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find slice thickness");
            }

            //Get Window and Level
            try
            {
                DecimalString window = df.WINDOW_WIDTH as DecimalString;
                DecimalString level = df.WINDOW_CENTER as DecimalString;
                props.WindowAndLevel = new WindowLevel(window.Data[0], level.Data[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find window and level");
            }
            //Is This A Dose File
            try
            {
                if (df.DOSE_UNITS.Data != null) { props.IsDose = true; }
                else { props.IsDose = false; }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not determine if file was a dose file");
            }

            //Read In Scaling Function
            try
            {
                DecimalString slope = props.IsDose ? df.DOSE_GRID_SCALING as DecimalString : df.RESCALE_SLOPE as DecimalString;
                DecimalString intercept = props.IsDose ? null : df.RESCALE_INTERCEPT as DecimalString;
                if (intercept == null) { intercept = new DecimalString(); intercept.Data = new double[] { 0 }; }
                props.Function = new ScalingFunction(slope.Data[0], intercept.Data[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find scaling function");
            }

            //Get Samples Per Pixel
            try
            {
                UnsignedShort samples = df.SAMPLES_PER_PIXEL as UnsignedShort;
                props.SamplesPerPixel = samples.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find samples per pixel");
            }

            //Get Transfer Syntax
            try
            {
                UniqueIdentifier syntax = df.TRANSFER_SYNTAX_UID as UniqueIdentifier;
                string stringSyntax = syntax.Data;

                switch (stringSyntax)
                {
                    case Constants.EXPLICIT_VR_BIG_ENDIAN: props.TransferSyntax = Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN; break;
                    case Constants.EXPLICIT_VR_LITTLE_ENDIAN: props.TransferSyntax = Constants.TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN; break;
                    case Constants.IMPLICIT_VR_LITTLE_ENDIAN: props.TransferSyntax = Constants.TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN; break;
                    case Constants.JPEG_2000: props.TransferSyntax = Constants.TransferSyntax.JPEG_2000; break;
                    case Constants.JPEG_2000_LOSSLESS: props.TransferSyntax = Constants.TransferSyntax.JPEG_2000_LOSSLESS; break;
                    case Constants.JPEG_BASELINE: props.TransferSyntax = Constants.TransferSyntax.JPEG_BASELINE; break;
                    case Constants.JPEG_EXTENDED: props.TransferSyntax = Constants.TransferSyntax.JPEG_EXTENDED; break;
                    case Constants.JPEG_LOSSLESS_14: props.TransferSyntax = Constants.TransferSyntax.JPEG_LOSSLESS_14; break;
                    case Constants.JPEG_LOSSLESS_14_S1: props.TransferSyntax = Constants.TransferSyntax.JPEG_LOSSLESS_14_S1; break;
                    case Constants.JPEG_LOSSLESS_15: props.TransferSyntax = Constants.TransferSyntax.JPEG_LOSSLESS_15; break;
                    case Constants.JPEG_LS_LOSSLESS: props.TransferSyntax = Constants.TransferSyntax.JPEG_LS_LOSSLESS; break;
                    case Constants.JPEG_LS_NEAR_LOSSLESS: props.TransferSyntax = Constants.TransferSyntax.JPEG_LS_NEAR_LOSSLESS; break;
                    case Constants.JPEG_PROGRESSIVE: props.TransferSyntax = Constants.TransferSyntax.JPEG_PROGRESSIVE; break;
                    case Constants.RLE_LOSSLESS: props.TransferSyntax = Constants.TransferSyntax.RLE_LOSSLESS; break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find transfer syntax");
            }

            //Get Image Position
            try
            {
                DecimalString imagePosition = df.IMAGE_POSITION as DecimalString;
                props.ImagePosition = new Position(imagePosition.Data[0], imagePosition.Data[1], imagePosition.Data[3]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find image position");
            }

            //Set Number of Frames
            try
            {
                IntegerString numberOfFrames = df.NUMBER_OF_FRAMES as IntegerString;
                props.NumberOfFrames = numberOfFrames.Data[0];
            }
            catch (Exception e) { Console.WriteLine("Could not find number of frames"); }

            //Set Grid Frame Offset Vector
            try
            {
                DecimalString offsetVector = df.GRID_FRAME_OFFSET_VECTOR as DecimalString;
                props.OffsetVector = offsetVector.Data;
            }
            catch (Exception e) { Console.WriteLine("Could not find grid frame offset vector"); }

            return props;
        }

        /// <summary>
        /// Takes each raw pixel value and scales it with the slope and intercept specified in the 
        /// DICOM header properties.
        /// </summary>
        /// <param name="data">the pixel data from the DICOM file</param>
        /// <param name="properties">the image properties from the DICOM header</param>
        /// <returns></returns>
        public static float[] GetPixels(PixelData data, ImageProperties properties)
        {

            //Set up matrix
            int frames = properties.NumberOfFrames > 0 ? properties.NumberOfFrames : 1;
            float[] values = new float[properties.Rows * properties.Columns * frames];

            //Check encapsulation
            if (data.Format == FrameDataFormat.NATIVE)
            {
                using (BinaryReader r = new BinaryReader(new MemoryStream(data.ByteData)))
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = GetNativePixels(r, properties);
                    }
                }
            }
            else
            {
                //This is not implemented yet
                CompressionHelper.Decompress(data, properties, ref values);
            }

            return values;
        }

        public static float[] GetUnscaledPixels(PixelData data, ImageProperties properties)
        {

            //Set up matrix
            int frames = properties.NumberOfFrames > 0 ? properties.NumberOfFrames : 1;
            float[] values = new float[properties.Rows * properties.Columns * frames];

            //Check encapsulation
            if (data.Format == FrameDataFormat.NATIVE)
            {
                using (BinaryReader r = new BinaryReader(new MemoryStream(data.ByteData)))
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = GetNativePixelsUnscaled(r, properties);
                    }
                }
            }
            else
            {
                //This is not implemented yet
                CompressionHelper.Decompress(data, properties, ref values);
            }

            return values;
        }

        //Working on for v0.6
        //public static void SetPixels(PixelData data, ImageProperties properties, float[] values)
        //{
        //    byte[] toBytes = new byte[values.Length * (int)Math.Ceiling((double)properties.BitsAllocated / 8)*properties.SamplesPerPixel];
        //    float min, max;
        //    GetMaxAndMinFloats(values, out max, out min);
        //    data.Format = FrameDataFormat.NATIVE;
        //    if (properties.SamplesPerPixel == 1)
        //    {
        //        using (BinaryWriter w = new BinaryWriter(new MemoryStream(toBytes)))
        //        {
        //            switch (properties.BitsAllocated)
        //            {
        //                case 8:
        //                    properties.Function = new ScalingFunction();
        //                    properties.Function.Slope = 
        //                    w.Write(= properties.Function.RescaledValue(r.ReadByte());
        //                    break;

        //                case 16: byte[] bytes16 = new byte[] { r.ReadByte(), r.ReadByte() };
        //                    if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
        //                    {
        //                        bytes16 = ArrayHelper.ReverseArray(bytes16);
        //                    }
        //                    value = properties.Function.RescaledValue(BitConverter.ToInt16(bytes16, 0));
        //                    break;


        //                case 24: byte[] bytes24 = new byte[] { 0x0, r.ReadByte(), r.ReadByte(), r.ReadByte() };
        //                    if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
        //                    {
        //                        bytes24 = ArrayHelper.ReverseArray(bytes24);
        //                    }
        //                    value = properties.Function.RescaledValue(BitConverter.ToInt32(bytes24, 0));
        //                    break;


        //                case 32: byte[] bytes32 = new byte[] { r.ReadByte(), r.ReadByte(), r.ReadByte(), r.ReadByte() };
        //                    if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
        //                    {
        //                        bytes32 = ArrayHelper.ReverseArray(bytes32);
        //                    }
        //                    value = properties.Function.RescaledValue(BitConverter.ToInt32(bytes32, 0));
        //                    break;
        //            }
        //        }
        //    }
        //}

        public static void GetMaxAndMinFloats(float[] values, out float max, out float min)
        {
            min = max = values[0];
            foreach (float f in values)
            {
                min = f < min ? f : min;
                max = f > max ? f : max;
            }
        }
        public static ColorPalette GetGrayScalePalette()
        {
            using (var bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                var cp = bmp.Palette;
                var entries = cp.Entries;
                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i] = Color.FromArgb(i, i, i);
                }
                return cp;
            }
        }

        public static Bitmap GetBitmap(PixelData data, ImageProperties properties)
        {
            float[] values = GetPixels(data, properties);
            byte[] bytes = new byte[values.Length];
            for(int i=0;i<values.Length;i++)
            {
                bytes[i] = properties.WindowAndLevel.GetValue(values[i]);
            }

            //Here create the Bitmap to the know height, width and format
            Bitmap bmp = new Bitmap(properties.Rows, properties.Columns, PixelFormat.Format8bppIndexed);
            bmp.Palette = GetGrayScalePalette();

            //Create a BitmapData and Lock all pixels to be written
            BitmapData bmpData = bmp.LockBits(
                                 new Rectangle(0, 0, bmp.Width, bmp.Height),
                                 ImageLockMode.WriteOnly, bmp.PixelFormat);

            //Copy the data from the byte array into BitmapData.Scan0
            Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);

            //Unlock the pixels
            bmp.UnlockBits(bmpData);

            //Return the bitmap
            return bmp;
        }

        public static Bitmap GetBitmap(float[] pixels, ImageProperties properties)
        {
            byte[] bytes = new byte[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                bytes[i] = properties.WindowAndLevel.GetValue(pixels[i]);
            }

            //Here create the Bitmap to the know height, width and format
            Bitmap bmp = new Bitmap(properties.Rows, properties.Columns, PixelFormat.Format8bppIndexed);
            bmp.Palette = GetGrayScalePalette();

            //Create a BitmapData and Lock all pixels to be written
            BitmapData bmpData = bmp.LockBits(
                                 new Rectangle(0, 0, bmp.Width, bmp.Height),
                                 ImageLockMode.WriteOnly, bmp.PixelFormat);

            //Copy the data from the byte array into BitmapData.Scan0
            Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);

            //Unlock the pixels
            bmp.UnlockBits(bmpData);

            //Return the bitmap
            return bmp;

        }

        private static float GetNativePixels(BinaryReader r, ImageProperties properties)
        {
            float value = 0;
            //GRAYSCALE IMAGES
            if (properties.SamplesPerPixel == 1)
            {
                switch (properties.BitsAllocated)
                {

                    case 8:
                        value = properties.Function.RescaledValue(r.ReadByte());
                        break;

                    case 16: byte[] bytes16 = new byte[] { r.ReadByte(), r.ReadByte() };
                        if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                        {
                            bytes16 = ArrayHelper.ReverseArray(bytes16);
                        }
                        ushort s = 39132;
                        byte[] bytes = BitConverter.GetBytes(s);
                        if ((bytes[0] == bytes16[0] && bytes[1] == bytes16[1]) || (bytes[0] == bytes16[1] && bytes[1] == bytes16[0]))
                        {
                            Console.Write("Stop!");
                        }
                        value = properties.Function.RescaledValue(BitConverter.ToInt16(bytes16, 0));
                        break;


                    case 24: byte[] bytes24 = new byte[] { 0x0, r.ReadByte(), r.ReadByte(), r.ReadByte() };
                        if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                        {
                            bytes24 = ArrayHelper.ReverseArray(bytes24);
                        }
                        value = properties.Function.RescaledValue(BitConverter.ToInt32(bytes24, 0));
                        break;


                    case 32: byte[] bytes32 = new byte[] { r.ReadByte(), r.ReadByte(), r.ReadByte(), r.ReadByte() };
                        if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                        {
                            bytes32 = ArrayHelper.ReverseArray(bytes32);
                        }
                        value = properties.Function.RescaledValue(BitConverter.ToInt32(bytes32, 0));
                        break;
                }
            }
            return value;
        }

        private static float GetNativePixelsUnscaled(BinaryReader r, ImageProperties properties)
        {
            float value = 0;
            //GRAYSCALE IMAGES
            if (properties.SamplesPerPixel == 1)
            {
                switch (properties.BitsAllocated)
                {

                    case 8:
                        value = r.ReadByte();
                        break;

                    case 16: byte[] bytes16 = new byte[] { r.ReadByte(), r.ReadByte() };
                        if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                        {
                            bytes16 = ArrayHelper.ReverseArray(bytes16);
                        }
                        value = BitConverter.ToInt16(bytes16, 0);
                        break;


                    case 24: byte[] bytes24 = new byte[] { 0x0, r.ReadByte(), r.ReadByte(), r.ReadByte() };
                        if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                        {
                            bytes24 = ArrayHelper.ReverseArray(bytes24);
                        }
                        value = BitConverter.ToInt32(bytes24, 0);
                        break;


                    case 32: byte[] bytes32 = new byte[] { r.ReadByte(), r.ReadByte(), r.ReadByte(), r.ReadByte() };
                        if (properties.TransferSyntax == Constants.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                        {
                            bytes32 = ArrayHelper.ReverseArray(bytes32);
                        }
                        value = BitConverter.ToInt32(bytes32, 0);
                        break;
                }
            }
            return value;
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


