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
                Position = data["position"].ToString(),
                Role = data["role"].ToString(),
                Type = data["type"].ToString(),
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