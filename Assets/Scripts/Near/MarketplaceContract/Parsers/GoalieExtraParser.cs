using System.Linq;
using Near.Models;
using Near.Models.Extras;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Near.MarketplaceContract.Parsers
{
    public class GoalieExtraParser : IExtraParser
    {
        public Extra ParseExtra(JObject data)
        {
            uint[] stats = JsonConvert.DeserializeObject<uint[]>(data["stats"].ToString());

            return new GoalieExtra()
            {
                Number = uint.Parse(data["number"].ToString()),
                Position = data["position"].ToString(),
                Role = data["role"].ToString(),
                Type = data["type"].ToString(),
                Stats = new GoalieStats()
                {
                    // TODO: check stats matching 
                    GloveAndBlocker = stats[0],
                    Morale = stats[1],
                    Pads = stats[2],
                    Stand = stats[3],
                    Stretch = stats[4]
                }
            };
        }
    }
}