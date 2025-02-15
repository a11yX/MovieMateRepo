using Microsoft.AspNetCore.Identity;

namespace MovieMate.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public byte[]? ProfilePicture { get; set; }

        public ICollection<PlanToWatch> PlanToWatch { get; set; } = null!;
        public ICollection<Watching> Watching { get; set;} = null!;
        public ICollection<Watched> Watched { get; set; } = null!;
        public ICollection<Favourite> Favourites { get; set; } = null!;
    }
}
