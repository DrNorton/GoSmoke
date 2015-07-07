using System.Threading.Tasks;
using GoSmokeMobile.Api.Requests.Base;

namespace GoSmokeMobile.Api.Executer
{
    public interface IApiExecuter
    {
        Task<T> Execute<T>(IRequest request);
    }
}