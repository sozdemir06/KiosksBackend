using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Photos
{
    public interface IUploadFile
    {
        Task<UploadedFileResultDto> Upload(IFormFile file, string uploadedLocationName);
        Task<UploadedFileResultDto> UploadVideo(IFormFile file, string uploadedLocationName);
        Task<List<UploadedFileResultDto>> UploadPdf(IFormFile file, string uploadedLocationName);
        Task<bool> DeleteFile(string fileName,string fullPath);
    }
}