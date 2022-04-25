namespace Near.Models
{
    public abstract class Extra
    {
        public string Type { get; set; }

        public abstract Extra GetExtra();
    }
}