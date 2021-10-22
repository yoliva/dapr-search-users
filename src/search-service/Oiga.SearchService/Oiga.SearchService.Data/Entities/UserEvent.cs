using System;

namespace Oiga.SearchService.Data.Entities
{
    public class UserEvent
    {
        public string ID { get; set; } = Guid.NewGuid().ToString("N");
        public string EvtTopic { get; set; }
        public string Data { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
