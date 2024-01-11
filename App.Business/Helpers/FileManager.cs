using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Helpers
{
    public static class FileManager
    {
        public static bool CheckLength(this IFormFile file, int length)
        {
            return file.Length <= length;
        }

        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static string Upload(this IFormFile file, string webPath, string folderPath)
        {
            if(!Directory.Exists(webPath + folderPath))
            {
                Directory.CreateDirectory(webPath + folderPath);
            }

            string fileName = file.FileName;

            if(fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64);
            }

            fileName += Guid.NewGuid().ToString();

            string filePath = webPath + folderPath + fileName;

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.CopyTo(fileStream);
            }

            return fileName;
        }

        public static void Delete(string fileName, string webPath, string folderPath)
        {
            string filePath = webPath + folderPath + fileName;
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
