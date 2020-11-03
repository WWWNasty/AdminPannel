using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.UserManage;
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
            List<GetAllUsersDto> model = await _manageUserRepository.GetAllUsers();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "UsersRead")]
        public async Task<ActionResult> GetAllUsersForUser()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<GetAllUsersDto> model = await _manageUserRepository.GetAllUsersForUser(userId);
            return View("GetAllUsers", model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<IActionResult> UpdateUser(int userId)
        {
            UpdateUserViewModel model = await _manageUserRepository.GetUser(userId);
            //return View(_mapper.Map<UpdateUserViewModel>(user));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var isLastAdmin = await _manageUserRepository.IsAdminLastActive();
                if (isLastAdmin && model.Role == "SuperAdministrator" && model.IsUsed == false)
                {
                    model = await _manageUserRepository.GetUser(model.Id);
                    model.IsAdminLastActive = true;
                    return View(model);
                }

                await _manageUserRepository.UpdateUser(model);
                _logger.LogInformation("Пользователь с Id:{0} успешно отредактирован", model.Id);
                return RedirectToAction("GetAllUsers", "ManageUser");
            }

            model = await _manageUserRepository.GetUser(model.Id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "UsersEdit")]
        public async Task<IActionResult> UpdateUserForUser(int userId)
        {
            var model = await _manageUserRepository.GetUser(userId);
            return View("UpdateUser", model);
        }

        [HttpPost]
        [Authorize(Roles = "UsersEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateUserForUser(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isLastAdmin = await _manageUserRepository.IsAdminLastActive();
                if (isLastAdmin && model.Role == "SuperAdministrator" && model.IsUsed == false)
                {
                    model = await _manageUserRepository.GetUser(model.Id);
                    model.IsAdminLastActive = true;
                    return RedirectToAction("GetAllUsersForUser", model);
                }

                await _manageUserRepository.UpdateUser(model);
                _logger.LogInformation("Пользователь с Id:{0} успешно отредактирован", model.Id);

                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

                List<GetAllUsersDto> allUsers = await _manageUserRepository.GetAllUsersForUser(userId);
                return RedirectToAction("GetAllUsersForUser", allUsers);
            }

            model = await _manageUserRepository.GetUser(model.Id);
            return View("UpdateUser", model);
        }
    }
}