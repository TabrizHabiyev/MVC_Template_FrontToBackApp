using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MVC_TemplateApp.Aplication.Abstraction.Services;
using System.Reflection;

namespace MVC_TemplateApp.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _env;

        public FileService(IHostingEnvironment env)
        {
            _env = env;
        }

        public Task<bool> DeleteFile(string filePath)
        {
            try
            {
                // file path example : /images/adjkadfjk.png
                var path = _env.WebRootPath + filePath;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
               
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> FileValidation(IFormFile file ,string FileType, int fileSize)
        {
            
            try
            {
               var fileExtension = Path.GetExtension(file.FileName);
               string contentType = file.ContentType;

                if (contentType != FileType)
                {
                     return Task.FromResult(false);
                }
                if (file.Length > fileSize)
                {
                    return Task.FromResult(false);
                }
                return Task.FromResult(true);

               
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            

            
        }

        public Task<string> UploadFile(IFormFile file ,string fileType ,int fileSize)
        {

           try{

            var fileValidation = FileValidation(file, fileType, fileSize).Result;
            if (fileValidation)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Task.FromResult("/images/" + fileName);
            }
            else
            {
                return Task.FromResult("File is not valid");
            }

           }catch(Exception){
                return Task.FromResult("File is not valid");
           }
           
        }
    }
}


