using System.Collections.Generic;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Models;
using GoSmokeMobile.Api.Models.Dtos;

namespace GoSmokeMobile.Api.Facade
{
    public interface IApiFacade
    {
        Task<IsRegisteredDto> TryAuth(string phone);
        Task<ProfileDto> AuthAndGetProfile(string phone,string password);
    }
}