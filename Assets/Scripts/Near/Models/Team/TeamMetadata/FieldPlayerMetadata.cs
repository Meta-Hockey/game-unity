using Near.Models.Extras;

namespace Near.Models.Team.Metadata
{
    public class FieldPlayerMetadata
    {
        public string title { get; set; }
        public string description { get; set; }
        public string media { get; set; }
        public string media_hash { get; set; }
        public string issued_at { get; set; }
        public string expires_at { get; set; }
        public string starts_at { get; set; }
        public string updated_at { get; set; }
        public FieldPlayerExtra extra { get; set; }
    }
}