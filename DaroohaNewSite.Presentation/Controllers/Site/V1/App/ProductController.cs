﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DaroohaNewSite.Common.Helpers.Helpers;
using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Dtos.Common.Pagination;
using DaroohaNewSite.Data.Dtos.Site.App.Product;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Presentation.Routes.V1;
using DaroohaNewSite.Repo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaroohaNewSite.Presentation.Controllers.Site.V1.App
{
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "v1_Site_App")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork<DaroohaDbContext> _db;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork<DaroohaDbContext> dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        [HttpGet(ApiV1Routes.Product.GetProductList)]
        public async Task<IActionResult> GetProductList(string id, [FromQuery] PaginationDto paginationDto)
        {
            var getFourProduct = (await _db.ProductRepository.GetManyPagedListAsync(paginationDto, paginationDto.Filter.ToProductExpression(id), paginationDto.SortHe.ToProductOrderBy(paginationDto.SortDir), ""));
            var allProduct = _mapper.Map<IEnumerable<Tbl_Product>, List<ProductForReturnDto>>(getFourProduct);

            Response.AddPagination(getFourProduct.CurrentPage, getFourProduct.PageSize,
                    getFourProduct.TotalCount, getFourProduct.TotalPage);

            foreach (var item in allProduct)
            {
                item.ImageUrl = (await _db.ProductImageRepository.GetManyAsync(p => p.ProductId == item.ID, null, "")).FirstOrDefault().ImageUrl;
            }
            return Ok(allProduct);
        }

        [HttpGet(ApiV1Routes.Product.GetSingleProduct)]
        public async Task<IActionResult> GetSingleProduct(string id)
        {
            var getProduct = (await _db.ProductRepository.GetManyAsync(p => p.ID.Equals(id), null, "")).SingleOrDefault();
            var product = _mapper.Map<Tbl_Product, ProductForReturnDto>(getProduct);
            product.ImageUrl = (await _db.ProductImageRepository.GetManyAsync(p => p.ProductId == product.ID, null, "")).FirstOrDefault().ImageUrl;
            return Ok(product);
        }

        [HttpGet(ApiV1Routes.Product.GetProductImages)]
        public async Task<IActionResult> GetProductImages(string id)
        {
            var getImages = (await _db.ProductImageRepository.GetManyAsync(p => p.ProductId.Equals(id), null, ""));
            var images = _mapper.Map<IEnumerable<Tbl_ProductImage>, List<ProductImagesForReturnDto>>(getImages);
            return Ok(images);
        }
    }
}