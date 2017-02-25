using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Powell.Vehicles.Controllers
{
    using Managers;
    using Mvc.Controllers;
    using Mvc.Models;
    using Mvc.Services;
    using static ManageController.Result;

    public class ManageController : UnauthorizedController
    {
        private ApplicationSignInManager SignInManager { get; }

        private ApplicationUserManager UserManager { get; }

        // TODO: TVD: surely not being able to set the key type was an oversight: i.e. wanting strings instead of, say, Guids
        private IAuthenticationManager AuthManager { get; }

        private IControllerOptions<Result> Options { get; }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IAuthenticationManager authManager, IControllerOptions<Result> options)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            AuthManager = authManager;
            Options = options;
        }

        public enum Result
        {
            Unknown,
            //AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            //RemovePhoneSuccess,
            Error
        }

        internal static IDictionary<Result, string> GetResultMessages()
        {
            return new Dictionary<Result, string>
            {
                {ChangePasswordSuccess, "Your password has been changed."},
                {SetPasswordSuccess, "Your password has been set."},
                {SetTwoFactorSuccess, "Your two-factor authentication provider has been set."},
                //{AddPhoneSuccess, "Your phone number was added."},
                //{RemovePhoneSuccess, "Your phone number was removed."},
                {RemoveLoginSuccess, "The external login was removed."},
                {Error, "An error has occurred."},
            };
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(Result? result)
        {
            ViewBag.StatusMessage = Options.GetMessage(result ?? default(Result));

            var userId = UserId;

            // TODO: TBD: establish auto mappings...
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthManager.TwoFactorBrowserRememberedAsync(userId.ToString("D"))
            };

            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            Result? result;

            var userId = UserId;

            var removal = await UserManager.RemoveLoginAsync(userId, new UserLoginInfo(loginProvider, providerKey));

            if (removal.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                result = RemoveLoginSuccess;
            }
            else
            {
                result = Error;
            }
            return RedirectToAction("ManageLogins", new {Message = result});
        }

        ////
        //// GET: /Manage/AddPhoneNumber
        //public ActionResult AddPhoneNumber()
        //{
        //    return View();
        //}

        ////
        //// POST: /Manage/AddPhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var userId = UserId;

        //    // Generate the token and send it
        //    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, model.Number);
        //    if (UserManager.SmsService != null)
        //    {
        //        var message = new IdentityMessage
        //        {
        //            Destination = model.Number,
        //            Body = "Your security code is: " + code
        //        };
        //        await UserManager.SmsService.SendAsync(message);
        //    }
        //    return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        //}

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            var userId = UserId;

            await UserManager.SetTwoFactorEnabledAsync(userId, true);

            var user = await UserManager.FindByIdAsync(userId);

            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            var userId = UserId;

            await UserManager.SetTwoFactorEnabledAsync(userId, false);

            var user = await UserManager.FindByIdAsync(userId);

            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return RedirectToAction("Index", "Manage");
        }

        //// TODO: TBD: not dealing with "two factor" as far as phones are concerned.
        ////
        //// GET: /Manage/VerifyPhoneNumber
        //public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        //{
        //    var userId = UserId;
        //    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
        //    // Send an SMS through the SMS provider to verify the phone number
        //    return phoneNumber == null
        //        ? View("Error")
        //        : View(new VerifyPhoneNumberViewModel {PhoneNumber = phoneNumber});
        //}

        ////
        //// POST: /Manage/VerifyPhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var userId = UserId;

        //    var change = await UserManager.ChangePhoneNumberAsync(userId, model.PhoneNumber, model.Code);

        //    if (change.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(userId);
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Index", new { Message = AddPhoneSuccess });
        //    }
        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "Failed to verify phone");
        //    return View(model);
        //}

        ////
        //// POST: /Manage/RemovePhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RemovePhoneNumber()
        //{
        //    var userId = UserId;
        //    var result = await UserManager.SetPhoneNumberAsync(userId, null);
        //    if (!result.Succeeded)
        //    {
        //        return RedirectToAction("Index", new { Message = Error });
        //    }
        //    var user = await UserManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    }
        //    return RedirectToAction("Index", new { Message = RemovePhoneSuccess });
        //}

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = UserId;

            var change = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            if (change.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(UserId);

                if (user != null) await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                return RedirectToAction("Index", new {Message = ChangePasswordSuccess});
            }

            AddErrors(change);

            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            // ReSharper disable once InvertIf
            if (ModelState.IsValid)
            {
                var userId = UserId;

                var add = await UserManager.AddPasswordAsync(userId, model.NewPassword);

                if (add.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(UserId);

                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return RedirectToAction("Index", new { Message = Result.SetPasswordSuccess });
                }

                AddErrors(add);
            }

            // If we got this far, something failed, redisplay form.
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(Result? result)
        {
            ViewBag.StatusMessage = Options.GetMessage(result ?? default(Result));

            var userId = UserId;

            var user = await UserManager.FindByIdAsync(userId);

            if (user == null) return View("Error");

            var userLogins = await UserManager.GetLoginsAsync(userId);

            var otherLogins = AuthManager.GetExternalAuthenticationTypes()
                .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();

            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;

            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            var userId = UserId;

            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider,
                Url.Action("LinkLoginCallback", "Manage"), userId);
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());

            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new {Message = Error});
            }

            var userId = UserId;

            var result = await UserManager.AddLoginAsync(userId, loginInfo.Login);

            return result.Succeeded
                ? RedirectToAction("ManageLogins")
                : RedirectToAction("ManageLogins", new {Message = Error});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                UserManager?.Dispose();
            }

            base.Dispose(disposing);
        }

#region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var userId = UserId;
            var user = UserManager.FindById(userId);
            return user?.PasswordHash != null;
        }

#endregion

    }
}
