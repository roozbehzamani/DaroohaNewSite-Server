using AutoMapper;
using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Dtos.Site.App.Menu;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Presentation.Routes.V1;
using DaroohaNewSite.Repo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaroohaNewSite.Presentation.Controllers.Site.V1.App
{
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "v1_Site_App")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IUnitOfWork<DaroohaDbContext> _db;
        private readonly IMapper _mapper;
        public MenuController(IUnitOfWork<DaroohaDbContext> dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        [HttpGet(ApiV1Routes.MenuSite.AllMenuList)]
        public async Task<IActionResult> AllMenuList()
        {
            var getAllMenu = await _db.MenuRepository.GetAllAsync();
            var allMenu = _mapper.Map<IEnumerable<Tbl_Menu>, List<AllMenuForReturnDto>>(getAllMenu);
            return Ok(allMenu);
        }
    }
}
