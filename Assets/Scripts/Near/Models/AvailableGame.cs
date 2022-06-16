using System;

namespace Near.Models
{
    public class AvailableGame
    {
        public int GameId { get; set; } 
        public Tuple<string, string> PlayerIds { get; set; }
    }
}