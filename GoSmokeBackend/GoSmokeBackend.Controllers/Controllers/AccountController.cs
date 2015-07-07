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

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("tryauth")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> TryAuth(PhoneModel phoneModel)
        {


            var findedUser=await _userManager.FindByNameAsync(phoneModel.Phone);
      
            if (findedUser == null)
            {
                await Register(phoneModel);
                return SuccessApiResult(new {Registered = false});
            }
            else
            {
                return SuccessApiResult(new { Registered = true });
            }
        }


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("getprofile")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetProfile(AuthModel authModel)
        {
            if (!String.IsNullOrEmpty(authModel.SocialToken))
            {
                //Значит авторизация через контактик
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
               var response=await webClient.DownloadStringTaskAsync(new Uri(String.Format("{0}{1}", @"https://api.vk.com/method/users.get?access_token=", authModel.SocialToken)));
                var vkResponse = JsonConvert.DeserializeObject<VkUserGet>(response);
         
                //берём id из социалочки
                var findedUser=(_userManager as CustomUserManager).FindByIdAsync(vkResponse.response.FirstOrDefault().uid);
                return EmptyApiResult();
            }
            else
            {
                //Входит стандартно через логин пасс
                var oauthData = await GetToken(authModel.Login, authModel.Password);
                if (oauthData.IsError)
                {
                    return ErrorApiResult(401, oauthData.Error);
                }
                //oauth

                var findedUser = await _userManager.FindByNameAsync(authModel.Login);
                var profile = await _profileRepository.GetProfile(findedUser.Id);
                return SuccessApiResult(new ProfileResponse()
                {
                    Token = new Token(){ExpiredIn = oauthData.ExpiresIn,TokenType = oauthData.TokenType,Value = oauthData.AccessToken},
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Birthday = profile.Birthday
                });

            }
         
            

            
            
        }

        private async Task<TokenResponse> GetToken(string login,string password)
        {
            var host = base.ActionContext.Request.RequestUri.ToString().Replace("/account/getprofile", "") + "/token";
            var client = new OAuth2Client(new Uri(host));
            var oauthData = await client.RequestResourceOwnerPasswordAsync(login, password);
            return oauthData;
        }


        [System.Web.Http.Authorize]
        [System.Web.Http.Route("GetOrders")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetOrders()
        {
            return EmptyApiResult();
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
