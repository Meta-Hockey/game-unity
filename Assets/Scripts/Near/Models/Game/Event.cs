using Near.Models.Game.Team;

namespace Near.Models.Game
{
    public class Event
    {
        public Team.Team MyTeam { get; set; }
        
        public Team.Team OpponentTeam { get; set; }
        
        public FieldPlayer PlayerWithPuck { get; set; }
        
        public string Action { get; set; }
        
        public int ZoneNumber { get; set; }
        
        private int Time { get; set; }
    }
}