using Microsoft.AspNetCore.Identity;

namespace LetterBox.Application.Accounts.DataModels
{
    public class User : IdentityUser<Guid>
    {
        private User()
        {
            
        }

        private List<Role> _roles = [];

        public IReadOnlyList<Role> Roles => _roles;

        public DateTime CreatedAt { get; private set; }
        public bool isActive { get; private set; }

        public static User CreateAdmin(string userName, string email, Role role)
        {
            return new User
            {
                UserName = userName,
                Email = email,
                CreatedAt = DateTime.UtcNow,
                isActive = true,
                _roles = [role]
            };
        }
    }

}
