using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces;
using Admin.Panel.Core.Interfaces.Repositories;
using Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services;
using Admin.Panel.Core.Interfaces.Services.UserManageServiceInterfaces;
using Admin.Panel.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IManageUserService _manageUserService;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IManageUserService manageUserService,
            ILogger<AccountController> logger)

        {
            _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _manageUserService = manageUserService;
            _logger = logger;
        }

        [TempData] public string ErrorMessage { get; set; }

        [TempData] public string StatusMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var isUsed = await _manageUserService.IsUsed(model.Email, CancellationToken.None);

                if (isUsed)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                        isPersistent: false,
                        lockoutOnFailure: false);

                    //if (result.IsLockedOut)
                    //{
                    //    return RedirectToAction(nameof(Lockout));
                    //}

                    if (result.Succeeded)
                    {
                        var userId = _userRepository.GetIdByName(model.Email);
                        var userRole = _userRepository.IsUserInRoleAsync(userId);
                        _logger.LogInformation("Пользователь {0} с Id {1} был успешно авторизован.", userId,
                            model.Email);
                        if (userRole == "SuperAdministrator")
                        {
                            return RedirectToAction("GetAll", "Questionary");
                        }

                        return RedirectToAction("GetAllForUser", "Questionary");
                    }

                    _logger.LogInformation("Пользователь {0} не был авторизован.", model.Email);
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                _logger.LogInformation("Попытка авторизации неактивного пользователя {0}.", model.Email);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
                //return RedirectToAction(nameof(Lockout));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException(
                        $"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException(
                        $"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            // if (!ModelState.IsValid)
            // {
            //     return View(model);
            // }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            var email = user.Email;
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> Register()
        {
            RegisterDto model = await _manageUserService.GetCompaniesAndRoles();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email.Trim(),
                    Nickname = model.Nickname.Trim(),
                    IsConfirmed = true,
                    ConfirmationToken = model.ConfirmationToken,
                    CreatedDate = DateTime.UtcNow,
                    ApplicationCompaniesId = model.SelectedCompaniesId,
                    Email = model.Email.Trim(),
                    EmailConfirmed = true,
                    RoleId = model.RoleId
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("GetAllUsers", "ManageUser");
                }

                AddErrors(result);
            }

            model = await _manageUserService.GetCompaniesAndRoles();
            // If we got this far, something failed, redisplay form
            return View(model);
            //return RedirectToAction("Register", "Account", new { c = model.ApplicationCompanies });
        }

        [HttpGet]
        [Authorize(Roles = "UsersEdit")]
        public async Task<ActionResult> RegisterForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            RegisterDto model = await _manageUserService.GetCompaniesAndRolesForUser(userId);
            return View("Register", model);
        }

        [HttpPost]
        [Authorize(Roles = "UsersEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterForUser(RegisterDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email.Trim(),
                    Nickname = model.Nickname.Trim(),
                    IsConfirmed = true,
                    ConfirmationToken = model.ConfirmationToken,
                    CreatedDate = DateTime.UtcNow,
                    ApplicationCompaniesId = model.SelectedCompaniesId,
                    Email = model.Email.Trim(),
                    EmailConfirmed = true,
                    RoleId = model.RoleId
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} был успешно создан.", model.Email);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("GetAllUsersForUser", "ManageUser");
                }

                AddErrors(result);
            }

            model = await _manageUserService.GetCompaniesAndRoles();
            // If we got this far, something failed, redisplay form
            return View("Register", model);
            //return RedirectToAction("Register", "Account", new { c = model.ApplicationCompanies });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation(
                $"Пользователь {_userManager.GetUserName(User)} с ID: {_userManager.GetUserId(User)} вышел.");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    _logger.LogInformation("Пароль не может быть изменен, пользователя {0} нет в системе.",
                        model.Email);
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                    $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                _logger.LogInformation("Ссылка на восстановление паролдя была отпрпавлена на почту {0}.", model.Email);
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }

            var model = new ResetPasswordViewModel {Code = code};
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogInformation("Пароль не может быть изменен, пользователя {0} нет в системе.", model.Email);
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Пароль был успешно изменен пользователю {0}.", model.Email);
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            _logger.LogInformation("Не удалось изменить пароль пользователю {0}.", model.Email);
            AddErrors(result);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError(
                    $"Пароль не может быть изменен, пользователя с Id: {_userManager.GetUserId(User)} невозможно загрузить.");
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel {StatusMessage = StatusMessage};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError(
                    $"Пароль не может быть изменен, пользователя с Id: {_userManager.GetUserId(User)} невозможно загрузить.");
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult =
                await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                _logger.LogWarning(
                    $"Пароль не может быть изменен, пользователя с Id: {_userManager.GetUserId(User)} нет в системе.");
                AddErrors(changePasswordResult);
                return View(model);
            }

            _logger.LogInformation($"Пароль был успешно измнен пользователю с Id: {_userManager.GetUserId(User)}.");
            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel {StatusMessage = StatusMessage};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = _userRepository.IsUserInRoleAsync(Convert.ToInt32(userId));

            if (userRole == "SuperAdmin")
            {
                return RedirectToAction("GetAll", "Questionary");
            }

            return RedirectToAction("GetAllForUser", "Questionary");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}