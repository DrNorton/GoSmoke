using System.Collections.Generic;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Models;
using GoSmokeMobile.Api.Response;

namespace GoSmokeMobile.Api.Facade
{
    public interface IApiFacade
    {
        Task<ApiResponse<EmptyResult>> Register(string phone);
    }
}