using Microsoft.AspNetCore.Identity;

namespace MyMvcProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Party> Parties { get; set; } = [];
    }
}
