﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Interfaces;
using Admin.Panel.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Admin.Panel.Web.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IManageUserRepository _manageUserRepository;
        private readonly IMapper _mapper;

        public ManageUserController(IManageUserRepository manageUserRepository, IUserRepository userRepository, IMapper mapper)
        {
            _manageUserRepository = manageUserRepository;
            _userRepository = userRepository;
            _mapper = mapper;
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
        [Authorize(Roles = "SuperAdministrator, UsersRead")]
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
                await _manageUserRepository.UpdateUser(model);
                return View(model);
            }
            return View(model);
        }
    }
}
