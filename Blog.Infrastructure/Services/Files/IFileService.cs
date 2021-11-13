using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Blog.Shared.Enums.Sizes;
using Microsoft.AspNetCore.Http;

namespace Blog.Infrastructure.Services.Files
{
    public interface IFileService
    {
        bool Exists(string path);
        string GetExtension(string fileName);
        bool CheckMimeTypes(string fileMimeType, List<string> mimeTypes);
        bool CheckExtension(string fileExtension, List<string> extensions);
        bool CheckLength(long fileLengthBytes, long checkedLength, LengthType sizeType);
        bool CheckLength(long fileLengthBytes);
        Task<bool> SaveFile(IFormFile file, string path);
        string WwwrootPath();

        string CombinePath(string path, string folderName,string fileName);

        bool DeleteFileOnSystem(string path);
        (long width,long height) GetDimensionOfImage(IFormFile file);
        bool CheckDimension((long width , long heigth) file, (long width, long heigth) checkedDimension);

        string CombinePath(string directory, string folderName);
        string GetPath(string path);
        string EnsureExistDirectory(string folderName, string path);
        string GetFileName(string file);

        void ResizeImage(IFormFile file, int width, int height);
        void ResizeImageByWidth(IFormFile file,string path,int width);

        void WaterMark(FileInfo info, string watermarkPath);
        void OptimizeImage(FileInfo info);
        Task<string> ConvertToBase64(string path,CancellationToken cancellationToken);

        FileInfo GetFileInfo(string path);
        string RootContentPath();
        Task<string> ReadAllTextAsync(string file,CancellationToken cancellationToken);
        string GetFileNameWithoutExtension(string fileName);
        string GetFileExtension(string fileName);
    }
}
