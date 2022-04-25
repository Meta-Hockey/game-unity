using Near.Models;
using Newtonsoft.Json.Linq;

namespace Near.MarketplaceContract.Parsers
{
    public interface IExtraParser
    {
        public Extra ParseExtra(JObject data);
    }
}