using Microsoft.AspNetCore.Identity;

namespace LetterBox.Application.Accounts.DataModels
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; private set; }
        public bool isActive { get; private set; }
    }
    
}
