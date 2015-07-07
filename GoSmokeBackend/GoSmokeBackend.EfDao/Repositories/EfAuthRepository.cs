using System;
using System.Linq;
using System.Threading.Tasks;
using GoSmokeBackend.Dao.Repositories;
using GoSmokeBackend.Dto.AuthUsers;
using GoSmokeBackend.EfDao.Base;

namespace GoSmokeBackend.EfDao.Repositories
{
    public class EfAuthRepository : GenericRepository<User>, IAuthRepository
    {
        private readonly GoSmokeConnectionString _context;


        public EfAuthRepository(GoSmokeConnectionString context)
            : base(context)
        {
            _context = context;
        }

        public Task RegisterUser(ApplicationUser appUser)
        {
            var user = Db.Set<User>().Create();
            user.Phone = appUser.UserName;
            user.Password = appUser.PasswordHash;
            user.DateRegister = DateTime.Now;
            user.SecurityStamp = appUser.SecurityStamp;
            user.Confirm = appUser.ConfirmPhone;
            user.Profile = Db.Set<Profile>().Create();
            user.Profile.Id = user.Id;

            base.Insert(user);
            base.Save();
            appUser.Id = user.Id;
            return Task.FromResult(0);
        }

        public Task UpdateUser(ApplicationUser appUser)
        {
            var user = Db.Set<User>().FirstOrDefault(x => x.Id == appUser.Id);
            user.Phone = appUser.UserName;
            user.Password = appUser.PasswordHash;
            user.SecurityStamp = appUser.SecurityStamp;
            base.Update(user);
            base.Save();

            return Task.FromResult(0);
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            var user = Db.Set<User>().FirstOrDefault(x => x.Phone == userName);

            if (user == null)
            {
                return null;
            }

            return new ApplicationUser() { Id = user.Id, PasswordHash = user.Password, UserName = user.Phone};
        }




        public async Task<ApplicationUser> FindUser(long id)
        {
            var user = Db.Set<User>().FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return new ApplicationUser() { Id = user.Id, PasswordHash = user.Password, UserName = user.Phone };
        }
    }
}
