namespace fikatime_api.Models
{
    public class FikaModel
    {
        public string? Id { get; set; }
        public string? PartitionKey { get; set; }
        public int Month { get; set; }
        public int DayOfMonth { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? WikiUrl { get; set; }
    }
}
