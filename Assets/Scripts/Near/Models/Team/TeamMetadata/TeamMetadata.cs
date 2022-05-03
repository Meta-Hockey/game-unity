using System.Collections.Generic;

namespace Near.Models.Team.Metadata
{
    public class TeamMetadata
    {
        public Dictionary<string, FiveMetadata> fives;
        public Dictionary<string, GoalieMetadata> goalies;
    }
}