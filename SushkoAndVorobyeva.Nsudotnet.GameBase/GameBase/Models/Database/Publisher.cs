using System.Collections.Generic;

namespace GameBase.Models.Database
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
