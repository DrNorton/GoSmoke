using Microsoft.AspNet.Identity;

namespace GoSmokeBackend.Dto.AuthUsers
{
    public class ApplicationUser : IUser<long>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool ConfirmPhone { get; set; }
    }
}
