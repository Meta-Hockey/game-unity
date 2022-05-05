using Near.Models;
using Near.Models.Extras;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Near.MarketplaceContract.Parsers
{
    public class FieldPlayerExtraParser : IExtraParser
    {
        public Extra ParseExtra(JObject data)
        {
            uint[] stats = JsonConvert.DeserializeObject<uint[]>(data["stats"].ToString());

            return new FieldPlayerExtra()
            {
                Number = uint.Parse(data["number"].ToString()),
                Position = data["position"] != null ? data["position"].ToString() : data["player_position"].ToString(),
                Role = data["role"] != null ? data["role"].ToString() : data["player_role"].ToString(),
                Type = data["type"] != null ? data["type"].ToString() : data["player_type"].ToString(),
                Stats = new FieldPlayerStats()
                {
                    // TODO: check stats matching 
                    Skating = stats[0],
                    Morale = stats[1],
                    Shooting = stats[2],
                    Strength = stats[3],
                    IQ = stats[4]
                }
            };
        }
    }
}