using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Matrix;
using EvilDicom.Components;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using EvilDicom.Helper;
using EvilDicom.VR;

namespace EvilDicom.Image
{
    /// <summary>
    /// The image matrix class holds the properties and methods necessary for handling the storage and display of image matrices.
    /// The matrix can be either a 2D or 3D image.
    /// </summary>
    public class ImageMatrix
    {
        #region PROPERTIES
        /// <summary>
        /// This contains all of the pixel data as float elements in a one dimensional array.
        /// </summary>
        public float[] Image { get; set; }
        /// <summary>
        /// This contains the bitmap image for displaying the image matrix with the proper window and level.
        /// </summary>
        private Bitmap BitImage { get; set; }
        /// <summary>
        /// The properties of the image file(s).
        /// </summary>
        public ImageProperties Properties { get; set; }
        #endregion

        public ImageMatrix(PixelData data, ImageProperties properties)
        {
            Properties = properties;
            Image = ImageHelper.GetPixels(data, properties);
        }

        /// <summary>
        /// This constructor takes a single dicom file path and parses out the image properties and the pixel data.
        /// </summary>
        /// <param name="dicomFile">a string containing the path to the DICOM file to create the matrix</param>
        public ImageMatrix(string dicomFile)
        {
            DICOMFile df = new DICOMFile(dicomFile);
            Properties = ImageHelper.PullProperties(df);
            Image = ImageHelper.GetPixels(df.PIXEL_DATA as PixelData, Properties);
        }

        /// <summary>
        /// This constructor takes a string of dicom file paths and parses out the image properties and the pixel data.
        /// </summary>
        /// <param name="dicomFiles">A string array of all of the paths of the dicom files of the image(s) to load into the matrix</param>
        public ImageMatrix(string[] dicomFiles)
        {
            InitializeMatrix(dicomFiles);

            if (Properties.IsDose)
            {
                DICOMFile df = new DICOMFile(dicomFiles[0]);
                PixelData data = df.PIXEL_DATA as PixelData;
                Buffer.BlockCopy(ImageHelper.GetPixels(data, Properties), 0, Image, 0, Properties.Size * Properties.NumberOfFrames * sizeof(float));
            }
            else
            {
                foreach (string file in dicomFiles)
                {
                    ImageProperties testProperty;
                    DICOMFile df = new DICOMFile(file);

                    if (TestImageDimensions(df, out testProperty))
                    {
                        AppendImageToMatrix(df, testProperty);
                    }
                }
            }
        }

        /// <summary>
        /// This constructor takes a string of dicom file paths and parses out the image properties and the pixel data. It also takes
        /// a progress bar and progress label for updating a UI.
        /// </summary>
        /// <param name="dicomFiles">A string array of all of the paths of the dicom files of the image(s) to load into the matrix</param>
        /// <param name="progressBar">The progress bar in the UI to be updated</param>
        /// <param name="progressLabel">The progress label in the UI to be updated</param>
        public ImageMatrix(string[] dicomFiles, System.Windows.Forms.ProgressBar progressBar, System.Windows.Forms.Label progressLabel)
        {
            //UPDATE UI
            progressLabel.Text = "Opening Files";
            progressLabel.Update();
            progressBar.Maximum = dicomFiles.Length;
            progressBar.Value = 0;

            InitializeMatrix(dicomFiles);

            if (Properties.IsDose)
            {
                progressLabel.Text = "Reading Dose File...";
                progressLabel.Update();
                DICOMFile df = new DICOMFile(dicomFiles[0]);
                PixelData data = df.PIXEL_DATA as PixelData;
                Buffer.BlockCopy(ImageHelper.GetPixels(data, Properties), 0, Image, 0, Properties.Size * Properties.NumberOfFrames * sizeof(float));
            }

            else
            {
                foreach (string file in dicomFiles)
                {
                    ImageProperties thisProperty;
                    DICOMFile df = new DICOMFile(file);

                    if (TestImageDimensions(df, out thisProperty))
                    {
                        AppendImageToMatrix(df, thisProperty);
                    }
                    //Update UI
                    progressLabel.Text = "Loading image number " + thisProperty.ImageNumber + " of " + dicomFiles.Length + "...";
                    progressLabel.Update();
                    progressBar.Value++;
                }
            }

            //Reset UI
            progressLabel.Text = "";
            progressLabel.Update();
            progressBar.Value = 0;
        }


        /// <summary>
        /// This method is designed to return a 24bit RGB bitmap image from one slice of the cube
        /// based on the window and level of the image.
        /// </summary>
        /// <param name="slice">the integer slice number to be drawn. The first slice is zero.</param>
        /// <returns>The 24bit RGB image for display</returns>
        public virtual System.Drawing.Image GetImage(int slice)
        {
            float[] imagePixels = GetPartialMatrix(slice);

            //If Window and Level is not set..Set to Default
            if (Properties.WindowAndLevel == null)
            {
                float max, min;
                ImageHelper.GetMaxAndMinFloats(Image, out max, out min);
                float window = max - min;
                float level = window / 2;
                WindowLevel wl = new WindowLevel(window, level);
                this.Properties.WindowAndLevel = wl;
            }

            return ImageHelper.GetBitmap(imagePixels, Properties);
        }

        /// <summary>
        /// Gets the pixels values for just one 2D image in the 3D matrix.
        /// </summary>
        /// <param name="slice">the slice of the image to return</param>
        /// <returns>an array of pixel values</returns>
        private float[] GetPartialMatrix(int slice)
        {
            return Image.Skip(Properties.Size * slice).Take(Properties.Size).ToArray();
        }

        private void AppendImageToMatrix(DICOMFile df, ImageProperties imageProperties)
        {
            int offset;//The offset of the first pixel of this image in the whole matrix
            if (imageProperties.ImageNumber < 1) { offset = 0; }
            else { offset = (imageProperties.ImageNumber - 1) * Properties.Size; }
            if (offset > Image.Length) { offset = 0; }
            float[] imagePixels = ImageHelper.GetPixels((PixelData)df.PIXEL_DATA, imageProperties);
            Array.Copy(imagePixels, 0, Image, offset, imagePixels.Length);
        }

        /// <summary>
        /// This method checks to see if the rows and columns of this file match the dimensions of the initial set
        /// </summary>
        /// <param name="file">the path to the DICOM file</param>
        /// <returns>boolean representing if this file belongs in the set</returns>
        private bool TestImageDimensions(DICOMFile df, out ImageProperties testProperty)
        {
            testProperty = ImageHelper.PullProperties(df);
            if (testProperty.Rows != Properties.Rows || testProperty.Columns != Properties.Columns)
            {
                Console.WriteLine("File {0} has incorrect number of rows or columns", df.Path);
                return false;
            }
            return true;
        }

        private void InitializeMatrix(string[] dicomFiles)
        {

            Properties = ImageHelper.PullProperties(new DICOMFile(dicomFiles[0]));

            //Check to see if Number of Frames in Known**
            if (Properties.NumberOfFrames == 0)
            {
                Properties.NumberOfFrames = dicomFiles.Length;
            }

            if (dicomFiles.Length > 1)
            {
                Image = new float[Properties.Size * dicomFiles.Length];
            }
            else
            {
                try { Image = new float[Properties.Size * Properties.NumberOfFrames]; }
                catch (Exception e) { Console.WriteLine("Could not initialize single dicom file frame data"); }
            }
        }
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


