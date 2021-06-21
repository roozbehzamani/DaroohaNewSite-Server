using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Dtos.Site.Panel.Admin.Brand;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Presentation.Helpers.Filters;
using DaroohaNewSite.Presentation.Routes.V1;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Services.Upload.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DaroohaNewSite.Presentation.Controllers.Site.V1.Admin
{
    [ApiExplorerSettings(GroupName = "v1_Site_Panel")]
    [ApiController]
    public class AdminBrandController : ControllerBase
    {
        private readonly IUnitOfWork<DaroohaDbContext> _db;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IWebHostEnvironment _env;


        public AdminBrandController(IUnitOfWork<DaroohaDbContext> dbContext, IMapper mapper, IUploadService uploadService,
            IWebHostEnvironment env)
        {
            _db = dbContext;
            _mapper = mapper;
            _uploadService = uploadService;
            _env = env;
        }


        [Authorize(Policy = "RequireAdminRole")]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        [HttpGet(ApiV1Routes.AdminBrands.AllBrandList)]
        public async Task<IActionResult> AllBrandList(string userId)
        {
            var brandLists = await _db.BrandRepository.GetAllAsync();
            var brandListForReturn = _mapper.Map<IEnumerable<Tbl_Brand>, List<BrandListForReturnDto>>(brandLists);
            return Ok(brandListForReturn);
        }


        [Authorize(Policy = "RequireAdminRole")]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        [HttpPost(ApiV1Routes.AdminBrands.AddBrand)]
        public async Task<IActionResult> AddBrand(string userId, [FromForm] BrandForCreateDto brandForCreateDto)
        {
            var brandExist = await _db.BrandRepository.GetAsync(p => p.BrandName == brandForCreateDto.BrandName);
            if (brandExist == null)
            {
                var uploadRes = await _uploadService.UploadPic(
                    brandForCreateDto.File,
                        userId,
                        _env.WebRootPath,
                        $"{Request.Scheme ?? ""}://{Request.Host.Value ?? ""}{Request.PathBase.Value ?? ""}",
                        "0",
                        "BrandLogo"
                    );
                if (uploadRes.Status)
                {
                    var brandForCreate = new Tbl_Brand()
                    {
                        BrandLogoUrl = uploadRes.Url
                    };
                    var brand = _mapper.Map(brandForCreateDto, brandForCreate);

                    await _db.BrandRepository.InsertAsync(brand);

                    if (await _db.SaveAsync())
                    {
                        return Ok();
                    }
                    else
                        return BadRequest("خطا در ثبت اطلاعات");
                }
                else
                {
                    return BadRequest(uploadRes.Message);
                }

            }
            else
            {
                return BadRequest("مدارک ارسالی قبلیه شما در حال بررسی میباشد");
            }


        }

        [Authorize(Policy = "RequireAdminRole")]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        [HttpDelete(ApiV1Routes.AdminBrands.DeleteBrand)]
        public async Task<IActionResult> DeleteBrand(string userId, string id)
        {
            var brandExist = await _db.BrandRepository.GetAsync(p => p.ID == id);
            if (brandExist != null)
            {
                var productOfBrands = await _db.ProductRepository.GetAsync(p => p.Tbl_Brands.ID == brandExist.ID);
                _db.BrandRepository.Delete(brandExist);

                if (await _db.SaveAsync())
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("خطا در حذف اطلاعات");
                }

            }
            else
            {
                return BadRequest("چنین برندی وجود ندارد");
            }


        }

        [Authorize(Policy = "RequireAdminRole")]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        [HttpGet(ApiV1Routes.AdminBrands.GetBrand)]
        public async Task<IActionResult> GetBrand(string id, string userId)
        {
            var brand = await _db.BrandRepository.GetByIdAsync(id);
            if (brand != null)
            {
                var brandForreturn = _mapper.Map<BrandForReturnDto>(brand);

                return Ok(brandForreturn);
            }
            else
            {
                return BadRequest("چنبن برندی وجود ندارد");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        [HttpPut(ApiV1Routes.AdminBrands.UpdateBrand)]
        public async Task<IActionResult> UpdateBrand(string id, string userId, [FromForm] BrandForUpdateDto brandForUpdateDto)
        {
            var brandFromRepo = await _db.BrandRepository.GetByIdAsync(id);
            if (brandFromRepo != null)
            {
                var uploadRes = await _uploadService.UploadPic(
                    brandForUpdateDto.File,
                        userId,
                        _env.WebRootPath,
                        $"{Request.Scheme ?? ""}://{Request.Host.Value ?? ""}{Request.PathBase.Value ?? ""}",
                        "0",
                        "BrandLogo"
                    );
                if (uploadRes.Status)
                {
                    brandFromRepo.BrandLogoUrl = uploadRes.Url;
                    var brand = _mapper.Map(brandForUpdateDto, brandFromRepo);
                    _db.BrandRepository.Update(brand);

                    if (await _db.SaveAsync())
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("خطا در بروزرسانی اطلاعات");
                    }
                }
                else
                {
                    return BadRequest(uploadRes.Message);
                }
            }
            else
            {
                return BadRequest("چنین برندی وجود ندارد");
            }
        }
    }
}