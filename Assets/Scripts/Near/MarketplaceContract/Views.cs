using System.Dynamic;
using System.Threading.Tasks;
using NearClientUnity;
using Newtonsoft.Json.Linq;

namespace Near.MarketplaceContract
{
    public static class Views
    {
        public static async Task<dynamic> LoadCards(string fromIndex = "0", int limit = 50)
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetNftContract();
            dynamic args = new ExpandoObject();
            args.from_index = fromIndex;
            args.limit = 50;

            dynamic cards = await gameContract.View("nft_tokens", args);
            dynamic res = JObject.Parse(cards);
            return cards.result;
        }
    }
}