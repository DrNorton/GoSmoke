using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using GoSmokeBackend.Controllers.ApiResults;
using GoSmokeBackend.Controllers.Models;
using GoSmokeBackend.Dao.Repositories;
using GoSmokeBackend.Dto.AuthUsers;

namespace GoSmokeBackend.Controllers.Controllers
{
      [System.Web.Http.RoutePrefix("api/Profile")]
    public class ProfileController:CustomApiController
    {
          private readonly IProfileRepository _profileRepository;

          public ProfileController(IProfileRepository profileRepository)
          {
              _profileRepository = profileRepository;
          }

          [System.Web.Http.AllowAnonymous]
          [System.Web.Http.Route("Get")]
          [System.Web.Http.HttpPost]
          public async Task<IHttpActionResult> Get()
          {
              if (!ModelState.IsValid)
              {
                  var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                  return ErrorApiResult(1, errorsMessages);
              }
              return null;
          }

    }
}
