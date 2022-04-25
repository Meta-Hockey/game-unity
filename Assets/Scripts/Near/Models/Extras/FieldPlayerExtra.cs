namespace Near.Models.Extras
{
    public class FieldPlayerExtra : PlayerExtra
    {
        public FieldPlayerStats Stats { get; set; }
        
        public override Extra GetExtra()
        {
            return this;
        }
    }
}