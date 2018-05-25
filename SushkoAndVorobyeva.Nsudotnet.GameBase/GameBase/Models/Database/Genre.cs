using System.Collections.Generic;

namespace GameBase.Models.Database
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games{ get; set; }
    }
}
