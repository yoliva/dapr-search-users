using System;

namespace Oiga.UserService.Data.Entities
{
    public class User
    {
        public string ID { get; set; } = Guid.NewGuid().ToString("N");
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime CreatedAtUtc { get; set; }
    }
}
