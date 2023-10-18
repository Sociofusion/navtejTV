using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace navtezcms.adminw.Admin
{
    public class AssetUploader
    {
        public string CloudFolderName { get; set; }

        public string UploadAsset(string fileName, HttpPostedFile oPostedFile, string _folderName, out string fileContentType)
        {
            CloudFolderName = _folderName;

            if (oPostedFile.ContentLength <= 0)
                throw new Exception("File does not contain any content");


            if(oPostedFile.ContentLength > 20971520)
            {
                throw new Exception("File size exceed 20 MB");
            }

            byte[] input = new byte[oPostedFile.ContentLength];
            System.IO.Stream fileStr = oPostedFile.InputStream;
            fileStr.Read(input, 0, oPostedFile.ContentLength);


            string[] mediaExtensions_video = {
                ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA",
                ".AVI", ".MP4", ".DIVX", ".WMV",".MOV",".M4V",".M4A",".M3U",".M1V",".MP2",".MP4V",".3GP", ".MKV"
                };

            string[] mediaExtensions_image = {
                ".HEIC",".JPEG",".PNG",".TIFF",".GIF",".JPG",".BMP",".WPG", ".WEBP",".SVG",
                };

            string filename = DateTime.Now.ToString("ddMMyyyyHHMMss") + Path.GetExtension(fileName);
            string localFolderPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LocalAssetPath"]);
            string fullFileName = localFolderPath + "\\" + filename;
            oPostedFile.SaveAs(fullFileName);

            if (mediaExtensions_video.Contains(Path.GetExtension(fileName).ToUpper(), StringComparer.OrdinalIgnoreCase))
            {
                fileContentType = "audio/video";
                UploadFileToCloud(fullFileName);
            }
            else if (mediaExtensions_image.Contains(Path.GetExtension(fileName).ToUpper(), StringComparer.OrdinalIgnoreCase))
            {
                fileContentType = "image";
                UploadFileToCloud(fullFileName);
            }
            else
            {
                throw new Exception("File does not contain any content");
            }

            return filename;
        }

        private void ReduceImageSize(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);

                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);

                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                System.Drawing.Imaging.Encoder myEncoder =
               System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                thumbnailImg.Save(HttpContext.Current.Server.MapPath(targetPath), jpgEncoder, myEncoderParameters);

                string baseFilePath = HttpContext.Current.Server.MapPath("~"); //ConfigurationManager.AppSettings["BaseFilePath"];
                string completeFileName = baseFilePath + targetPath.Replace("~", "");

                UploadFileToCloud(completeFileName);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void UploadFileToCloud(string fileToUpload)
        {
            try
            {
                string baseFilePath = HttpContext.Current.Server.MapPath("~");

                string sharedkeyFilePath = baseFilePath + "navtejcms-2c878a12b0be.json";
                string bucketName = "navtejcms";
                GoogleCredential credential = null;
                using (var jsonStream = new FileStream(sharedkeyFilePath, FileMode.Open,
                    FileAccess.Read, FileShare.Read))
                {
                    credential = GoogleCredential.FromStream(jsonStream);
                }
                var storageClient = StorageClient.Create(credential);

                using (var fileStream = new FileStream(fileToUpload, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    string onlyFileName = Path.GetFileName(fileToUpload);
                    string mimeType = MimeMapping.GetMimeMapping(onlyFileName);
                    UploadObjectOptions options = new UploadObjectOptions();
                    Google.Apis.Storage.v1.Data.Object fileData = storageClient.UploadObject(bucketName, CloudFolderName + "/" + onlyFileName, mimeType, fileStream);
                    fileData.CacheControl = "no-cache";
                }
            }
            catch (Exception ex)
            {
                string baseFilePath = HttpContext.Current.Server.MapPath("~");
                string errorFile = baseFilePath + "log.txt";
                //string errorFile = ConfigurationManager.AppSettings["ErrorLogFile"];
                File.AppendAllText(errorFile, "UploadFileToCloud:" + ex.Message);
            }
        }
    }
}