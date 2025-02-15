namespace MovieMate.Models
{
    public class PlanToWatch
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public Guid PlanId { get; set; }
        public Media Media { get; set; } = null!;
    }
}
