using AutoMapper;
using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Dtos.Site.Panel.User;
using DaroohaNewSite.Repo.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DaroohaNewSite.Common.ErrorsAndMessages;
using DaroohaNewSite.Services.Site.Admin.User.Interface;
using Microsoft.Extensions.Logging;
using DaroohaNewSite.Presentation.Helpers.Filters;
using DaroohaNewSite.Presentation.Routes.V1;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace DaroohaNewSite.Presentation.Controllers.Site.V1.User
{
    [ApiExplorerSettings(GroupName = "v1_Site_Panel")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork<DaroohaDbContext> _db;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUnitOfWork<DaroohaDbContext> dbContext, IMapper mapper, IUserService userservice, ILogger<UserController> logger)
        {
            _db = dbContext;
            _mapper = mapper;
            _userService = userservice;
            _logger = logger;
        }

        [Authorize(Policy = "AccessProfile")]
        [HttpGet(ApiV1Routes.Users.GetUser, Name = "GetUser")]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _db.UserRepository.GetByIdAsync(id);
            var userToReturn = _mapper.Map<UserForDetailedDTO>(user);
            return Ok(userToReturn);
        }

        [Authorize(Policy = "AccessProfile")]
        [HttpPut(ApiV1Routes.Users.UpdateUser)]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        public async Task<IActionResult> UpdateUser(string id, UserForUpdateDTO userForUpdateDTO)
        {
            var userFromRepo = await _db.UserRepository.GetByIdAsync(id);
            _mapper.Map(userForUpdateDTO, userFromRepo);
            _db.UserRepository.Update(userFromRepo);
            if (await _db.SaveAsync())
            {
                return NoContent();
            }
            else
            {
                _logger.LogError($"آپدیت کاربر {id} با خطا مواجه شد");
                return BadRequest(new ReturnErrorMessage()
                {
                    Status = false,
                    Title = "خطا",
                    Message = "آپدیت با خطا مواجه شد"
                });
            }
        }

        [Authorize(Policy = "AccessProfile")]
        [HttpPut(ApiV1Routes.Users.ChangeUserPassword)]
        [ServiceFilter(typeof(UserCheckIdFilter))]
        public async Task<IActionResult> ChangeUserPassword(string id, PasswordForChangeDTO passwordForChangeDto)
        {
            var userFromRepo = await _userService.GetUserForPassChange(id, passwordForChangeDto.OldPassword);
            if (userFromRepo == null)
                return BadRequest(new ReturnErrorMessage()
                {
                    Status = false,
                    Title = "خطا",
                    Message = "پسورد فعلی اشتباه میباشد . لطفا مجددا تلاش نمایید"
                });
            if (await _userService.UpdateUserPassword(userFromRepo, passwordForChangeDto.NewPassword))
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new ReturnErrorMessage()
                {
                    Status = false,
                    Title = "خطا",
                    Message = "ویرایش انجام نشد"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost(ApiV1Routes.Users.GetUsers)]
        public async Task<IActionResult> GetUsers(dtott dto)
        {
            var users = (await _db.UserRepository.GetAllAsync()).ToList();
            switch (dto.Flag)
            {
                case 1:
                    {
                        var usersForReturn = new List<UserForDetailedDTO>();
                        foreach (var item in users)
                        {
                            usersForReturn.Add(_mapper.Map<UserForDetailedDTO>(item));
                        }
                        return Ok(usersForReturn);
                    }
                case 2:
                    {
                        var usersForReturn = _mapper.Map<UserForDetailedDTO>
                            (users.Where(p => p.Id == dto.Id).Single());
                        return Ok(usersForReturn);
                    }
                case 3:
                    {
                        var user = users.First();
                        var rand = new Random();
                        user.Id = rand.Next(500, 20000).ToString();
                        user.UserName = "111111111111";
                        users.Add(user);
                        var usersForReturn = _mapper.Map<UserForDetailedDTO>(user);
                        return Ok(usersForReturn);
                    }
                case 4:
                    {
                        var us = users.Where(p => p.Id == dto.Id).Single();
                        us.FirstName = "33333333333333";

                        var usersForReturn = _mapper.Map<UserForDetailedDTO>(us);
                        return Ok(usersForReturn);
                    }
                case 5:
                    {
                        var us = users.Where(p => p.Id == dto.Id).Single();
                        users.Remove(us);
                        return Ok();
                    }
                default:
                    return Ok("");
            }


        }
    }
}