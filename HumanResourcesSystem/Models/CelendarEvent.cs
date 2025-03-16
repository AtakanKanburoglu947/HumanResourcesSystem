namespace HumanResourcesSystem.Models
{
    public class CelendarEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string UserId { get; set; }
    }
}
