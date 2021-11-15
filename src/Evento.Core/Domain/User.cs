using System;

namespace Evento.Core.Domain
{
    public class User : Entity
    {
        public string Role { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected User()
        {

        }

        public User(Guid id, string role, string name, string email, string password)
        {
            Id = id;
            Role = role;
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
