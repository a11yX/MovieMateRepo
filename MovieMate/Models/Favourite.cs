namespace MovieMate.Models
{
    public class Favourite
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public Guid FavouriteId { get; set; }
        public Media Media { get; set; } = null!;
    }
}
