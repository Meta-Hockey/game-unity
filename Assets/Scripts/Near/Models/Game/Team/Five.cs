using System.Collections.Generic;

namespace Near.Models.Game.Team
{
    public class Five
    {
        public Dictionary<string, FieldPlayer> field_players { get; set; } 
        public string number { get; set; }
        public string ice_time_priority { get; set; }
        public string time_field { get; set; }
    }
}