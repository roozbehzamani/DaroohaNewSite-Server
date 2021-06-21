using DaroohaNewSite.Common.ErrorsAndMessages;
using DaroohaNewSite.Data.Dtos.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaroohaNewSite.Services.Upload.Interface
{
    public interface IUploadService
    {
        FileUploadedDTO UploadToCloudinary(IFormFile file, string folderName);
        FileUploadedDTO RemoveFileFromCloudinary(string publicID);
        FileUploadedDTO RemoveFileFromLocal(string FileName, string WebRootPath, string FilePath);
        Task<FileUploadedDTO> UploadPicToLocal(IFormFile file, string userID, string WebRootPath, string BaseUrl, string UrlUrl = "Files\\Pic\\Profile");
        Task<FileUploadedDTO> UploadPic(IFormFile file, string userID, string WebRootPath, string BaseUrl, string ImageID, string folderName);
        ReturnErrorMessage CreateDirectory(string WebRootPath, string UrlUrl);
    }
}
