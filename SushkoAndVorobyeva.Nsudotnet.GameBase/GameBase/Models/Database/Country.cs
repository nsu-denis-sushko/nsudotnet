using System.Collections.Generic;

namespace GameBase.Models.Database
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Developer> Developers { get; set; }
    }
}
