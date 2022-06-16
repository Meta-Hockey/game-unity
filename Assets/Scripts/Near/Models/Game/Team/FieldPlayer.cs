using Near.Models.Extras;

namespace Near.Models.Game.Team
{
    public class FieldPlayer : Player
    {
        public string NativePosition { get; set; }
        
        public string Position { get; set; } 
        
        public float PositionCoefficient { get; set; }
        
        public FieldPlayerStats Stats { get; set; }
    }
}