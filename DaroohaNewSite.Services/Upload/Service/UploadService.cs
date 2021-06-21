using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DaroohaNewSite.Common.ErrorsAndMessages;
using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Dtos.Services;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Services.Upload.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaroohaNewSite.Services.Upload.Service
{
    public class UploadService : IUploadService
    {
        private readonly IUnitOfWork<DaroohaDbContext> _db;
        private readonly Cloudinary _cloudinary;
        private readonly Tbl_Setting _setting;
        public UploadService(IUnitOfWork<DaroohaDbContext> dbContext)
        {
            _db = dbContext;
            _setting = _db.SettingRepository.GetAll().FirstOrDefault();

            Account acc = new Account(
                _setting.CloudinaryCloudName,
                _setting.CloudinaryAPIkey,
                _setting.CloudinaryAPISecret
                );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<FileUploadedDTO> UploadPic(IFormFile file, string userID, string WebRootPath, string BaseUrl, string ImageID, string folderName)
        {
            if (_setting.UploadLocal)
            {
                if (!ImageID.Equals("0"))
                {
                    var deleteFromCloude = RemoveFileFromCloudinary(ImageID);
                    if (deleteFromCloude.Status)
                    {
                        var uploadLocalRes = await UploadPicToLocal(file, userID, WebRootPath, BaseUrl);
                        return uploadLocalRes;
                    }
                    else
                    {
                        return deleteFromCloude;
                    }
                }
                else
                {
                    var uploadCloudeRes = UploadToCloudinary(file, folderName);
                    return uploadCloudeRes;

                }
            }
            else
            {
                if (!ImageID.Equals("0"))
                {
                    var deleteFromCloude = RemoveFileFromCloudinary(ImageID);
                    if (deleteFromCloude.Status)
                    {
                        var uploadCloudeRes = UploadToCloudinary(file, folderName);
                        return uploadCloudeRes;
                    }
                    else
                    {
                        return deleteFromCloude;
                    }
                }
                else
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtention = Path.GetExtension(fileName);
                    string fileNewName = string.Format("{0}{1}", userID, fileExtention);
                    RemoveFileFromLocal(fileNewName, WebRootPath, "Files\\Pic\\Profile");
                    var uploadCloudeRes = UploadToCloudinary(file, folderName);
                    return uploadCloudeRes;
                }
            }
        }

        public async Task<FileUploadedDTO> UploadPicToLocal(IFormFile file, string userID, string WebRootPath, string BaseUrl, string Url = "Files\\Pic\\Profile")
        {
            if (file.Length > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtention = Path.GetExtension(fileName);
                    string fileNewName = string.Format("{0}{1}", userID, fileExtention);
                    string path = Path.Combine(WebRootPath, Url);
                    string fullPath = Path.Combine(path, fileNewName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return new FileUploadedDTO()
                    {
                        Status = true,
                        Message = "فایل با موفقبت ارسال شد",
                        PublicID = "0",
                        Url = $"{BaseUrl}/{"wwwroot/" + Url.Split('\\').Aggregate("", (current, str) => current + (str + "/")) + fileNewName}"
                    };
                }
                catch (Exception ex)
                {

                    return new FileUploadedDTO()
                    {
                        Status = false,
                        Message = ex.Message
                    };
                }
            }
            else
            {
                return new FileUploadedDTO()
                {
                    Status = false,
                    Message = "فایلی برای ارسال یافت نشد"
                };
            }
        }

        public FileUploadedDTO UploadToCloudinary(IFormFile file, string folderName)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(250).Height(250).Crop("fill").Gravity("face"),
                        Folder = folderName
                    };
                    try
                    {
                        uploadResult = _cloudinary.Upload(uploadParams);
                        if (uploadResult.Error == null)
                        {
                            return new FileUploadedDTO()
                            {
                                Status = true,
                                Message = "فایل با موفقبت ارسال شد",
                                PublicID = uploadResult.PublicId,
                                Url = uploadResult.Uri.ToString()
                            };
                        }
                        else
                        {
                            return new FileUploadedDTO()
                            {
                                Status = false,
                                Message = uploadResult.Error.Message
                            };
                        }
                    }
                    catch (Exception ex)
                    {

                        return new FileUploadedDTO()
                        {
                            Status = false,
                            Message = ex.Message
                        };
                    }

                }
            }
            else
            {
                return new FileUploadedDTO()
                {
                    Status = false,
                    Message = "فایلی برای ارسال یافت نشد"
                };
            }
        }

        public FileUploadedDTO RemoveFileFromCloudinary(string publicID)
        {
            var deleteParams = new DeletionParams(publicID);
            var deleteResult = _cloudinary.Destroy(deleteParams);
            if (deleteResult.Result.ToLower().Equals("ok"))
            {
                return new FileUploadedDTO()
                {
                    Status = true,
                    Message = "فایل با موفقیت حذف شد"
                };
            }
            else
            {
                return new FileUploadedDTO()
                {
                    Status = false,
                    Message = deleteResult.Error.Message
                };
            }
        }

        public FileUploadedDTO RemoveFileFromLocal(string FileName, string WebRootPath, string FilePath)
        {
            string path = Path.Combine(WebRootPath, FilePath);
            string fullPath = Path.Combine(path, FileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return new FileUploadedDTO()
                {
                    Status = true,
                    Message = "فایل با موفقیت حذف شد"
                };
            }
            else
            {
                return new FileUploadedDTO()
                {
                    Status = true,
                    Message = "فایلی با این نام وجود ندارد"
                };
            }
        }

        public ReturnErrorMessage CreateDirectory(string WebRootPath, string Url)
        {
            try
            {
                var path = Path.Combine(WebRootPath, Url);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return new ReturnErrorMessage
                {
                    Status = true
                };

            }
            catch (Exception ex)
            {
                return new ReturnErrorMessage
                {
                    Status = false,
                    Message = ex.Message
                };
            }

        }
    }
}
