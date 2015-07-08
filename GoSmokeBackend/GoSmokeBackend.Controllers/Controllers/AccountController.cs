using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GoSmokeBackend.Controllers.ApiResults;
using GoSmokeBackend.Controllers.Models;
using GoSmokeBackend.Dao.AuthManagers;
using GoSmokeBackend.Dao.Repositories;
using GoSmokeBackend.Dto.AuthUsers;
using GoSmokeBackend.Dto.Dtos;
using GoSmokeBackend.EfDao;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Thinktecture.IdentityModel.Client;
using VkNet;
using VkNet.Enums.Filters;
using System.Text;

namespace GoSmokeBackend.Controllers.Controllers
{
    [System.Web.Http.RoutePrefix("api/Account")]
   
    public class AccountController : CustomApiController
    {
        private readonly CustomUserManager _userManager;
        private readonly IProfileRepository _profileRepository;

        public AccountController(CustomUserManager userManager,IProfileRepository profileRepository)
        {
            _userManager = userManager;
            _profileRepository = profileRepository;
        }

        // POST api/Account/Register
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("Register")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Register(PhoneModel phoneModel)
        {
            if (!ModelState.IsValid)
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }


            var user = new ApplicationUser()
            {
                UserName = phoneModel.Phone,
            };
            var password = await _userManager.GeneratePassword();
            
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                try
                {
                    await _userManager.SendSmsAsync(user.Id, password);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                return EmptyApiResult();
            }
            else
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                if (!errorsMessages.Any()) return ErrorApiResult(12, "User exist");
                return ErrorApiResult(1, errorsMessages);
            }

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }
            var name = User.Identity.GetUserName();
            if (name == null)
            {
                return ErrorApiResult(100, "User not exists");
            }
            var userId = long.Parse(User.Identity.GetUserId());
            var result = await _userManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return EmptyApiResult();
            }
            else
            {
                return ErrorApiResult(2, result.Errors);
            }

        }


        [System.Web.Http.Route("RecoverPassword")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> RecoverPassword(RecoverPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }
         
            var user =await _userManager.FindByNameAsync(model.Phone);
            await _userManager.RemovePasswordAsync(user.Id);
            var newPassword = await _userManager.GeneratePassword();
            await _userManager.AddPasswordAsync(user.Id, newPassword);
            await _userManager.SendSmsAsync(user.Id, newPassword);
            return EmptyApiResult();
        }

    }
}
