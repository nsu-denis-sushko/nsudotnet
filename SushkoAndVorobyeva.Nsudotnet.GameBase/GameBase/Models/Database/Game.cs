using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBase.Models.Database
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Serial { get; set; }

        [ForeignKey("Developer")]
        public int DevelopersId { get; set; }
        public Developer Developer { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public int Multiplayer { get; set; }
        public int Age { get; set; }

  

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        public virtual ICollection<Genre> Genres{ get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Platform> Platforms{ get; set; }
    }
}
