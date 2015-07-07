using Microsoft.AspNet.Identity;

namespace GoSmokeBackend.Dao.AuthManagers
{
    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

     
    }
}
