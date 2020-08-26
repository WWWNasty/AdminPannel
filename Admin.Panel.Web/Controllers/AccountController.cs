using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class AccountController : Controller
    {

        //private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        //private readonly IEmailSender _emailSender;

        public AccountController(
            //UserManager<User> userManager,
            IUserRepository userRepository,
            SignInManager<User> signInManager
            //IEmailSender emailSender

        )

        {
           // _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
            //_emailSender = emailSender;

        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            try
            {
                RegisterDto сompanies = _userRepository.GetAllCompanies();
                return View(сompanies);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email.TrimEnd(), Nickname = model.Nickname.TrimEnd(), IsConfirmed = true,/* ConfirmationToken = confirmationToken,*/ CreatedDate = DateTime.UtcNow, ApplicationCompanyId = model.CompanyId };

                var result = await _userRepository.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<ActionResult> Register(RegisterDto model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //Generate a new confirmation token.  Here we are just storing a Guid as a string, but feel free to use whatever you want (if you use another type, make sure to update the user object
        //            //and the user table accordingly).
        //            var confirmationToken = Guid.NewGuid().ToString();

        //            //Create the User object.  If you have customized this beyond this example, make sure you update this to contain your new fields.  
        //            //The confirmation token in our example is ultimately for show.  Make sure to modify the RegisterViewModel and the Register view if you have customized the object.
        //            var user = new User { UserName = model.Email.TrimEnd(), Nickname = model.Nickname.TrimEnd(), IsConfirmed = true, ConfirmationToken = confirmationToken, CreatedDate = DateTime.UtcNow, ApplicationCompanyId = model.CompanyId };

        //            //Create the user
        //            var result = await _userManager.CreateAsync(user, model.Password);

        //            //If it's successful we log the user in and redirect to the home page
        //            if (result.Succeeded)
        //            {
        //                //send e-mail confirmation here and instead of logging the user in and taking them to the home page, redirect them to some page indicating a confirmation email has been sent to them
        //                //await SignInAsync(user, false);
        //                return RedirectToAction("", "");
        //            }
        //            AddErrors(result);
        //        }

        //        // If we got this far, something failed, redisplay form
        //        return View(model);
        //    }

        //    [AllowAnonymous]
        //    public async Task<ActionResult> ConfirmationLink(string id)
        //    {
        //        //decode the confirmation token
        //        var token = EmailConfirmationHelper.DecodeConfirmationToken(id);

        //        //find the user based on the email address
        //        var user = await _userManager.FindByNameAsync(token.Email);

        //        if (user != null)
        //        {
        //            //check if the user has already confirmed their account
        //            if (user.IsConfirmed)
        //            {
        //                ViewBag.MessageTitle = "Already Confirmed";
        //                ViewBag.Message = "Your account is already confirmed!";
        //                return View();
        //            }

        //            //check if the confirmation token is older than a day, if it is send them a new token
        //            if ((DateTime.UtcNow - user.CreatedDate).TotalDays > 1)
        //            {
        //                await ResendConfirmationToken(user);
        //                ViewBag.MessageTitle = "Token Expired";
        //                ViewBag.MessageTitle = "The confirmation token has expired.  A new token has been generated and emailed to you.";
        //                return View();
        //            }

        //            //set the account to confirmed and updated the user
        //            user.IsConfirmed = true;
        //            await _userManager.UpdateAsync(user);

        //            //pop the view to let the user know the confirmation was successful
        //            ViewBag.MessageTitle = "Confirmation Successful";
        //            ViewBag.Message = "Your account has been successfully activated!  Click <a href='/Account/Login'>here</a> to login.";
        //            return View();
        //        }

        //        //if we got this far then the token is completely invalid
        //        ViewBag.MessageTitle = "Invalid Confirmation Token";
        //        ViewBag.Message = "The confirmation token is invalid.  If you feel you have received this message in error, please contact [your email]";
        //        return RedirectToAction("", "");  //error;
        //    }

        //    private async Task SignInAsync(User user, bool isPersistent)
        //    {
        //        //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //        //var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //        // AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        //    }

        //    private void AddErrors(IdentityResult result)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.ToString());
        //        }
        //    }

        //    private async Task ResendConfirmationToken(User user)
        //    {
        //        //create a new confirmation token
        //        var confirmationToken = Guid.NewGuid().ToString();

        //        //update the users confirmation token and reset the created date
        //        user.ConfirmationToken = confirmationToken;
        //        user.CreatedDate = DateTime.UtcNow;
        //        await _userManager.UpdateAsync(user);

        //        //send the new confirmation link to the user
        //        //await EmailConfirmationHelper.SendRegistrationEmail(confirmationToken, user.UserName);
        //    }

    }
}
