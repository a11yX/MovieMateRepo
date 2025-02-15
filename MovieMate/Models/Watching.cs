namespace MovieMate.Models
{
    public class Watching
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public Guid WatchingId { get; set; }
        public Media Media { get; set; } = null!;
    }
}
