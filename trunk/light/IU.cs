using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace light
{
   /// <summary>
   ///  Image Utilities
   /// </summary>
   public sealed class IU
   {
      /// <summary>
      ///  调整图片文件的宽和高以适应新的宽高，如果原始尺寸小于最大宽和最大高，最不调整，否则按比例调整，缩小的以适应最大宽和最大高。
      /// </summary>
      /// <param name="originalFile"></param>
      /// <param name="newFile"></param>
      /// <param name="maxWidth"></param>
      /// <param name="maxHeight"></param>
      public static bool Resize(string originalFile, string newFile, int maxWidth, int maxHeight,out int width,out int height)
      {
         try
         {
            Image imgOriginal = Image.FromFile(originalFile);

            // Prevent using images internal thumbnail
            imgOriginal.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            imgOriginal.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (imgOriginal.Width < maxWidth && imgOriginal.Height < maxHeight)
            {
               width = imgOriginal.Width;
               height = imgOriginal.Height;
               imgOriginal.Save(newFile, ImageFormat.Png);
               imgOriginal.Dispose();
               return true;
            }

            int newHeight = imgOriginal.Height * maxWidth / imgOriginal.Width;
            if (newHeight > maxHeight)
            {
               maxWidth = imgOriginal.Width * maxHeight / imgOriginal.Height;
               newHeight = maxHeight;
            }
            width = maxWidth; height = newHeight;
            Image imgResize = imgOriginal.GetThumbnailImage(maxWidth, newHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            imgOriginal.Dispose();

            // Save resized picture
            string dir = Path.GetDirectoryName(newFile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            imgResize.Save(newFile, ImageFormat.Png);
            imgResize.Dispose();
            return true;
         }
         catch 
         {
            width = 0; height = 0;
            return false; 
         }
      }

      public static bool Resize(string originalFile, string newFile, int maxWidth, int maxHeight)
      {
         int width, height;
         return Resize(originalFile,newFile,maxWidth,maxHeight,out width,out height);
      }

      public static bool Crop(string originalFile, string newFile, Rectangle cropRect, int width, int height)
      {
         try
         {
            using (Image imgOriginal = Image.FromFile(originalFile))
            {
               Bitmap bmpOriginal = new Bitmap(imgOriginal);
               Bitmap bmpCrop = bmpOriginal.Clone(cropRect, bmpOriginal.PixelFormat);
               Image bmpSave = bmpCrop.GetThumbnailImage(width, height, null, IntPtr.Zero);
               
               bmpSave.Save(newFile, ImageFormat.Png);

               bmpOriginal.Dispose();
               bmpCrop.Dispose();
               bmpSave.Dispose();
            }

            return true;
         }
         catch { return false; }
      }

      private Image cropImage(Image img, Rectangle cropArea)
      {
         Bitmap bmpImage = new Bitmap(img);
         Bitmap bmpCrop = bmpImage.Clone(cropArea,
                                         bmpImage.PixelFormat);
         return (Image)(bmpCrop);
      }

      private Image resizeImage(Image imgToResize, Size size)
      {
         int sourceWidth = imgToResize.Width;
         int sourceHeight = imgToResize.Height;

         float nPercent = 0;
         float nPercentW = 0;
         float nPercentH = 0;

         nPercentW = ((float)size.Width / (float)sourceWidth);
         nPercentH = ((float)size.Height / (float)sourceHeight);

         if (nPercentH < nPercentW)
            nPercent = nPercentH;
         else
            nPercent = nPercentW;

         int destWidth = (int)(sourceWidth * nPercent);
         int destHeight = (int)(sourceHeight * nPercent);

         Bitmap b = new Bitmap(destWidth, destHeight);
         Graphics g = Graphics.FromImage((Image)b);
         g.InterpolationMode = InterpolationMode.HighQualityBicubic;

         g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
         g.Dispose();

         return (Image)b;
      }

      private void saveJpeg(string path, Bitmap img, long quality)
      {
         // Encoder parameter for image quality
         EncoderParameter qualityParam =
             new EncoderParameter(Encoder.Quality, quality);

         // Jpeg image codec
         ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

         if (jpegCodec == null)
            return;

         EncoderParameters encoderParams = new EncoderParameters(1);
         encoderParams.Param[0] = qualityParam;

         img.Save(path, jpegCodec, encoderParams);
      }

      private ImageCodecInfo getEncoderInfo(string mimeType)
      {
         // Get image codecs for all image formats
         ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

         // Find the correct image codec
         for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
               return codecs[i];
         return null;
      }
   }
}
