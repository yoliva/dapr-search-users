using System;

namespace Oiga.SearchService.Data.Entities
{
    public class UserData
    {
        public string ID { get; set; } = Guid.NewGuid().ToString("N");
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
