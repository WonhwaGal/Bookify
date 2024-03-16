using Bookify.Models.Abstractions;

namespace Bookify.Models
{
    public class User: Entity
    {
        public static User Create(string firstName, string lastName, string email, string password)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email, password);
            return user;
        }

        private User(Guid id, string firstName, string lastName, string email, string password) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}