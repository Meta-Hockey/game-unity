namespace Near.Models
{
    public class Metadata
    {
        public string title { get; set; }
        public string description { get; set; }
        public string media { get; set; }
        public string media_hash { get; set; }
        public string issued_at { get; set; }
        public string expires_at { get; set; }
        public string starts_at { get; set; }
        public string updated_at { get; set; }
        public Extra extra { get; set; }
    }
}