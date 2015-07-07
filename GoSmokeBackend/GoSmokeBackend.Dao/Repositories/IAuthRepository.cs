using System;
using System.Threading.Tasks;
using GoSmokeBackend.Dto.AuthUsers;

namespace GoSmokeBackend.Dao.Repositories
{
    public interface IAuthRepository : IDisposable
    {
        Task RegisterUser(ApplicationUser createUserDto);

        Task<ApplicationUser> FindUser(string userName, string password);
        Task<ApplicationUser> FindUser(long id);

        Task UpdateUser(ApplicationUser appUser);
        Task<ApplicationUser> FindByVkId(long vkId);
    }
}
