using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Extensions;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace Core.Utilities.Photos
{
    public class UploadFile : IUploadFile
    {
        private readonly IHostingEnvironment host;
        public UploadFile(IHostingEnvironment host)
        {
            this.host = host;

        }

        public async Task<bool> DeleteFile(string fileName, string folderName)
        {
            var checkDelete = true;
            var filePath = Path.Combine(host.WebRootPath, folderName, fileName);
            if (!File.Exists(filePath))
            {
                checkDelete = false;
                throw new RestException(HttpStatusCode.BadRequest, new { CantDeleteFile = "Fotoğraf dosya altından silinemedi..." });
            }

            File.Delete(filePath);
            return await Task.FromResult(checkDelete);

        }

        public async Task<UploadedFileResultDto> Upload(IFormFile file, string uploadedLocationName)
        {


            if (file.Length == 0 || file == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { EmtyFile = "Litfen Dosya Seçiniz.." });
            }

            var fileExtensions = Path.GetExtension(file.FileName);
            string[] accepted_file_types = new[] { ".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG", ".GIF",".gif"};
            if (!accepted_file_types.Any(s => s == fileExtensions))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { InCorrectFileTypes = "Yüklenen dosya tipi desteklenmiyor.Sadece .jpg,.jpeg,.png desteklenmektedir." });
            }
            var allowedFileSize = 5 * 1024 * 1024;


            if (file.Length > allowedFileSize)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { FileSizeExceed = "Dosya boyutu fotopraf için 5MB video için 40MB olmalıdır." });
            }

            var uploadsFolderPath = Path.Combine(host.WebRootPath, uploadedLocationName);
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);



            using (var image = new MagickImage(file.OpenReadStream()))
            {
                image.Write(filePath);
            }

            var imageToCompress = new FileInfo(host.WebRootPath + "/" + uploadedLocationName + "/" + fileName);
            var optimizer = new ImageOptimizer();
            optimizer.Compress(imageToCompress);
            imageToCompress.Refresh();

            var result = new UploadedFileResultDto()
            {
                FullPath = "http://localhost:5000/" + uploadedLocationName + "/" + fileName,
                Name = fileName,
                FileType = "image"
            };

            return await Task.FromResult(result);

        }

        public async Task<List<UploadedFileResultDto>> UploadPdf(IFormFile file, string uploadedLocationName)
        {
            if (file.Length == 0 || file == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { EmtyFile = "Litfen Dosya Seçiniz.." });
            }

            var fileExtensions = Path.GetExtension(file.FileName);
            string[] accepted_file_types = new[] { ".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG", ".pdf" };
            if (!accepted_file_types.Any(s => s == fileExtensions))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { InCorrectFileTypes = "Yüklenen dosya tipi desteklenmiyor.Sadece .jpg,.jpeg,.png desteklenmektedir." });
            }
            var allowedFileSize = 5 * 1024 * 1024;


            if (file.Length > allowedFileSize)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { FileSizeExceed = "Dosya boyutu fotopraf için 5MB video için 40MB olmalıdır." });
            }

            var uploadsFolderPath = Path.Combine(host.WebRootPath, uploadedLocationName);
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            List<string> fileNames = new List<string>();

            var settings = new MagickReadSettings();
            settings.Density = new Density(300, 300);
            using (var image = new MagickImage(file.OpenReadStream()))
            {
                using (var images = new MagickImageCollection())
                {
                    images.Read(image.FileName, settings);
                    var page = 1;
                    foreach (var item in images)
                    {
                        var fileName = Guid.NewGuid().ToString() + ".png";
                        var filePath = Path.Combine(uploadsFolderPath, fileName);
                        item.Write(filePath);
                        fileNames.Add(fileName);
                        page++;
                    }
                }
            }

            List<UploadedFileResultDto> result = new List<UploadedFileResultDto>();
            foreach (var fileName in fileNames)
            {
                var uploadResult = new UploadedFileResultDto()
                {
                    FullPath = "http://localhost:5000/" + uploadedLocationName + "/" + fileName,
                    Name = fileName,
                    FileType = "image"
                };
                result.Add(uploadResult);
            }


            return await Task.FromResult(result);
        }

        public async Task<UploadedFileResultDto> UploadVideo(IFormFile file, string uploadedLocationName)
        {
            if (file == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NullFile = "Video Dosyası seçiniz..." });
            }

            if (file.Length > 50 * 1024 * 1024)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { ExeedFileSize = "Video boyutu 50 MB'tan büyük olamaz" });

            }

            var fileExtensions = Path.GetExtension(file.FileName);
            string[] accepted_file_types = new[] { ".mp4", ".MP4", ".webm", ".WEBM", ".ogg", ".OGG" };
            if (!accepted_file_types.Any(s => s == fileExtensions))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { InCorrectFileTypes = "Yüklenen dosya tipi desteklenmiyor.Sadece (mp4,webm,ogg) uzantılı videolar desteklenmektedir." });
            }

            if (file.Length > 0)
            {


                var uploadsFolderPath = Path.Combine(host.WebRootPath, uploadedLocationName);
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolderPath, fileName);



                using (FileStream fileStream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();


                }

                return new UploadedFileResultDto
                {
                    FullPath = "http://localhost:5000/" + uploadedLocationName + "/" + fileName,
                    Name = fileName,
                    FileType = "video"
                };

            }

            throw new RestException(HttpStatusCode.BadRequest, new { UserPhoto = "Lütfen (mp4,webm,ogg) dosya uzantılarından birini yükleyiniz." });
        }
    }
}