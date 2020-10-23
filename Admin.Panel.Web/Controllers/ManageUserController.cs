using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces;
using Admin.Panel.Core.Interfaces.Repositories;
using Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Web.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IManageUserRepository _manageUserRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ManageUserController> _logger;

        public ManageUserController(
            IManageUserRepository manageUserRepository, 
            IUserRepository userRepository, 
            IMapper mapper, 
            ILogger<ManageUserController> logger)
        {
            _manageUserRepository = manageUserRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                List<GetAllUsersDto> model = await _manageUserRepository.GetAllUsers();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "UsersRead")]
        public async Task<ActionResult> GetAllUsersForUser()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                List<GetAllUsersDto> model = await _manageUserRepository.GetAllUsersForUser(userId);
                return View("GetAllUsers", model);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, UsersEdit")]
        public async Task<IActionResult> UpdateUser(int userId)
        {
           var user = await _manageUserRepository.GetUser(userId);

           var model = new UpdateUserViewModel()
           {
               Id = user.Id,
               IsUsed = user.IsUsed,
               UserName = user.UserName,
               Nickname = user.Nickname,
               Email = user.Email,
               //CreatedDate = user.CreatedDate,
               //ApplicationCompanyId = user.ApplicationCompanyId
           };
            //return View(_mapper.Map<UpdateUserViewModel>(user));
           return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, UsersEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Пользователь с Id:{0} успешно отредактирован", model.Id);
                    await _manageUserRepository.UpdateUser(model);
                    return View("GetAllUsers");
                }
                catch (Exception e)
                {
                    _logger.LogError("Не удалось редактировать пользователя с Id: {0} c ошибкой: {1}", model.Id, e);
                }
               
            }
            var user = await _manageUserRepository.GetUser(model.Id);

             model = new UpdateUserViewModel()
            {
                Id = user.Id,
                IsUsed = user.IsUsed,
                UserName = user.UserName,
                Nickname = user.Nickname,
                Email = user.Email,
                //CreatedDate = user.CreatedDate,
                //ApplicationCompanyId = user.ApplicationCompanyId
            };
            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, UsersEdit")]
        public async Task<IActionResult> UpdateUserForUser(int userId)
        {
            var user = await _manageUserRepository.GetUser(userId);

            var model = new UpdateUserViewModel()
            {
                Id = user.Id,
                IsUsed = user.IsUsed,
                UserName = user.UserName,
                Nickname = user.Nickname,
                Email = user.Email,
                //CreatedDate = user.CreatedDate,
                //ApplicationCompanyId = user.ApplicationCompanyId
            };
            //return View(_mapper.Map<UpdateUserViewModel>(user));
            return View("UpdateUser", model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, UsersEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateUserForUser(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                    await _manageUserRepository.UpdateUser(model);
                    _logger.LogInformation("Пользователь с Id:{0} успешно отредактирован", model.Id);
                    var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    List<GetAllUsersDto> model1 = await _manageUserRepository.GetAllUsersForUser(userId);
                    return RedirectToAction("GetAllUsersForUser", model1);
                }
                catch (Exception e)
                {
                    _logger.LogError("Не удалось редактировать пользователя с Id: {0} c ошибкой: {1}", model.Id, e);
                }
                
            }
            var user = await _manageUserRepository.GetUser(model.Id);

            model = new UpdateUserViewModel()
            {
                Id = user.Id,
                IsUsed = user.IsUsed,
                UserName = user.UserName,
                Nickname = user.Nickname,
                Email = user.Email,
                //CreatedDate = user.CreatedDate,
                //ApplicationCompanyId = user.ApplicationCompanyId
            };
            return View("UpdateUser", model);
        }
    }
}
