using Oiga.UserService.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Oiga.UserService.v1.Models
{
    public class UserProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }

        internal static UserProfileDto FromUser(User usr)
        {
            return new UserProfileDto
            {
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                FullName = usr.FullName,
                Username = usr.Username
            };
        }
    }
}
