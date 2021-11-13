using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Shared.Enums.Sizes;
using Blog.Shared.Helpers;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Blog.Infrastructure.Services.Files
{
    public class FileService : IFileService, IScopedLifeTime
    {

        #region Fields
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region Constructor

        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Methods

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public bool CheckMimeTypes(string fileMimeType, List<string> mimeTypes)
        {
            if (string.IsNullOrEmpty(fileMimeType) || mimeTypes.Count <= 0)
                return false;

            return mimeTypes.Any(mimeType => fileMimeType.ToLower().Equals(mimeType.ToLower()));
        }

        public bool CheckExtension(string fileExtension, List<string> extensions)
        {
            if (string.IsNullOrEmpty(fileExtension) || extensions.Count <= 0)
                return false;


            return extensions.Any(extension => fileExtension.ToLower().Equals(extension.ToLower()));
        }

        public bool CheckLength(long fileLengthBytes, long checkedLength, LengthType sizeType)
        {
            if (fileLengthBytes <= 0 || checkedLength <= 0)
                return false;

            switch (sizeType)
            {
                case LengthType.Byte:

                    return fileLengthBytes <= checkedLength;

                case LengthType.KByte:
                    return fileLengthBytes / 1024 <= checkedLength;
                case LengthType.MBye:
                    return fileLengthBytes / (1024 * 1024) <= checkedLength;
                case LengthType.GByte:
                    return fileLengthBytes / (1024 * 1024 * 1024) <= checkedLength;
                default:
                    return false;
            }





        }

        public bool CheckLength(long fileLengthBytes)
        {
            return fileLengthBytes > 0;
        }

        public async Task<bool> SaveFile(IFormFile file, string path)
        {
            try
            {


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string WwwrootPath()
        {
            return _hostingEnvironment.WebRootPath;
        }

        public string CombinePath(string path, string folderName, string fileName)
        {
            return Path.Combine(path, folderName, fileName);
        }

        public bool DeleteFileOnSystem(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return false;
            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public (long width, long height) GetDimensionOfImage(IFormFile file)
        {
            long height;
            long width;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                stream.Seek(0, SeekOrigin.Begin);
                using (var img = new MagickImage(stream))
                {
                    width = img.Width;
                    height = img.Height;
                }

            }

            return (width, height);

        }

        public bool CheckDimension((long width, long heigth) file, (long width, long heigth) checkedDimension)
        {
            return file.width == checkedDimension.width && file.heigth == checkedDimension.heigth;
        }

        public string CombinePath(string directory, string folderName)
        {
            return Path.Combine(directory, folderName);
        }

        public string GetPath(string path)
        {
            return CommonHelper.MapPath(path);
        }

        public string EnsureExistDirectory(string folderName, string path)
        {
            var directories = Directory.GetDirectories(path);
            var exist = directories.Any(directory => GetUserNameDirectory(directory).ToLower().Equals(folderName.ToLower()));//false;
            if (!exist)
            {
                var directory = string.Concat(path, "\\" + folderName);
                var directoryInfo = Directory.CreateDirectory(directory);
                return directoryInfo.FullName;
            }
            var dir = Path.GetFullPath(string.Concat(path, "\\" + folderName));// string.Concat( path, "\\" + folderName);
            return dir;


        }

        private string GetUserNameDirectory(string directory)
        {
            var temp = directory.Split("\\");
            return temp[1];
        }

        public string GetFileName(string file)
        {
            return Path.GetFileName(file);
        }

        public void ResizeImage(IFormFile file, int width, int height)
        {

            using (var image = new MagickImage(file.FileName))
            {
                image.Resize(width, height);
                // This is the Compression level.
                image.Quality = 10;
                // Save the result
                image.Write(file.FileName);
            }
        }

        public void ResizeImageByWidth(IFormFile file, string path,int width)
        {
         
                
                using (var image = new MagickImage(path))
                {
                    var dimension = GetDimensionOfImage(file);
                    var ratio = dimension.width / width;
                    var height = Convert.ToInt32(dimension.height / ratio);
                    image.Resize(width, height);
                    // Save the result
                    image.Write(path);
                }
           
        }

        public void WaterMark(FileInfo info, string watermarkPath)
        {
            using (var image = new MagickImage(info))
            {
                // Read the watermark that will be put on top of the image
                using (var watermark = new MagickImage(watermarkPath))
                {

                    // Optionally make the watermark more transparent
                    watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 95);
                    // Draw the watermark in the bottom right corner
                    image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);
                    // Or draw the watermark at a specific location
                    //   image.Composite(watermark, 200, 50, CompositeOperator.Over);
                }

                try
                {
                    // Save the result
                    image.Write(info);
                }
                catch (Exception e)
                {
                   //nothings
                }
                

            }

        }

        public void OptimizeImage(FileInfo info)
        {
            var optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(info);
            info.Refresh();
        }

        public async Task<string> ConvertToBase64(string path, CancellationToken cancellationToken)
        {
            var result = string.Empty;
            if (!Exists(path)) return result;
            var bytes = await File.ReadAllBytesAsync(path, cancellationToken);
            result = Convert.ToBase64String(bytes);

            return result;
        }

        public FileInfo GetFileInfo(string path)
        {
            return new FileInfo(path);
        }

        public string RootContentPath()
        {

            return _hostingEnvironment.ContentRootPath;

        }

        public async Task<string> ReadAllTextAsync(string file, CancellationToken cancellationToken)
        {
            var txt = string.Empty;
            if (Exists(file))
            {
                txt = await File.ReadAllTextAsync(file, cancellationToken);
            }

            return txt;
        }

        public string GetFileNameWithoutExtension(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        public string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public static IEnumerable<string> DirectoryFiles(string path)
        {
            return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
        }

        #endregion

    }
}