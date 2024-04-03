using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;

namespace MVC_FirstProject.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1. Get Located Folder Path
            //  string folderPath = $"C:\\Users\\Tarak\\OneDrive\\Documents\\Nahla_Visual\\C#\\MVC_FirstProject\\MVC_FirstProject.PL\\wwwroot\\files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //2.  Get file name ond make it unique
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            //3. Get File Parth
            String filePath = Path.Combine(folderPath, fileName);

            //4. Save File As streams[Date Per Time]
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);
            return fileName;
        }

        public static void DeleteFile(string fileName, string FolderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
