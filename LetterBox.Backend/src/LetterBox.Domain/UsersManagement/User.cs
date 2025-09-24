namespace LetterBox.Domain.UsersManagement
{
    public sealed class User
    {
        private User() // ef core
        {
        }
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public string Role { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public bool isActive { get; private set; }
    }
}
