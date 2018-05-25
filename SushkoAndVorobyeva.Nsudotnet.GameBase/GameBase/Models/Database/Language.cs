using System.Collections.Generic;

namespace GameBase.Models.Database
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
     
        public virtual ICollection<Game> Games{ get; set; }
    }
}
