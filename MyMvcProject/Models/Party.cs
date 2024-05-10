using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyMvcProject.Models
{
    public class Party
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public bool Joined { get; set; }

        public ICollection<ApplicationUser> Guests { get; set; } = [];
    }
}
