using System;

namespace OpenHackTeam16
{
    public class ProductRating
    {
        public string id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string UserNotes { get; set; }
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
