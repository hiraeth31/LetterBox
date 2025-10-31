namespace LetterBox.Application.Accounts.DataModels
{
    public class AdminAccount
    {
        public const string ADMIN = nameof(ADMIN);
        
        //ef core
        private AdminAccount()
        {
        }

        public AdminAccount(string fullname, User user)
        {
            Id = Guid.NewGuid();
            FullName = fullname;
            User = user;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string FullName { get; set; }
    }
}
