namespace SKADA.Models.Users.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Role { get; set; }

        public User()
        {

        }

        public User(string name, string surname, string email, string password, UserType role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Role = role;
        }
    }

    public enum UserType
    {
        ADMIN,
        CLIENT
    }
}
