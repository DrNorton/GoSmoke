using System.Threading.Tasks;
using GoSmokeMobile.Api.Models;
using GoSmokeMobile.Api.Requests.Base;

namespace GoSmokeMobile.Api.Executer
{
    public interface IApiExecuter
    {
        Task<ApiResponse<T>> Execute<T>(IRequest request);
        Task<T> ExecuteWithoutApiResponse<T>(IRequest request);
    }
}