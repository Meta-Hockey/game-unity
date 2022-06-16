using Near.Models.Extras;

namespace Near.Models.Game.Team
{
    public class FieldPlayer : Player
    {
        public string native_position { get; set; }
        
        public string position { get; set; } 
        
        public float position_coefficient { get; set; }
        
        public FieldPlayerStats stats { get; set; }
    }
}