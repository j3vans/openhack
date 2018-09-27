using System;

namespace OpenHackTeam16
{
    public class ProductRating
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string UserNotes { get; set; }
        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
