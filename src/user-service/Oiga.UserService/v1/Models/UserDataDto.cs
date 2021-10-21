using Oiga.UserService.Data.Entities;

namespace Oiga.UserService.v1.Models
{
    public class UserDataDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }

        public static UserDataDto FromUser(User user)
        {
            return new UserDataDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                UserName = user.Username
            };
        }
    }
}
