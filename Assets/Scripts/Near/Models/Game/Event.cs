using Near.Models.Game.Team;

namespace Near.Models.Game
{
    public class Event
    {
        public Team.Team my_team { get; set; }
        
        public Team.Team opponent_team { get; set; }
        
        public FieldPlayer player_with_puck { get; set; }
        
        public string action { get; set; }
        
        public int zone_number { get; set; }
        
        private int time { get; set; }
    }
}