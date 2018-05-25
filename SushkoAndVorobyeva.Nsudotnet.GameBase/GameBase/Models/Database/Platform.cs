using System.Collections.Generic;

namespace GameBase.Models.Database
{
    public class Platform
    {
        public int PlatformId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
