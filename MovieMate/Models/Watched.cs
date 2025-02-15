namespace MovieMate.Models
{
    public class Watched
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public Guid WatchedId { get; set; }
        public Media Media { get; set; } = null!;
    }
}
