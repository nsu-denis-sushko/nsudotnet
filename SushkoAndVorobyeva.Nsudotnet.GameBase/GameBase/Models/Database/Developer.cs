using System.ComponentModel.DataAnnotations.Schema;

namespace GameBase.Models.Database
{
    public class Developer
    {
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
