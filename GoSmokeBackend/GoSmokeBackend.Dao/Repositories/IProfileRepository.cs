using System.Threading.Tasks;
using Hsking.Api.Dto.Dtos;

namespace GoSmokeBackend.Dao.Repositories
{
    public interface IProfileRepository
    {
        Task<ProfileDto> GetProfile(long userId);
        Task UpdateProfile(ProfileDto profile);
    }
}
