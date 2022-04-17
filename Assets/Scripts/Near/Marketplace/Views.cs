using System.Dynamic;
using System.Threading.Tasks;
using NearClientUnity;

namespace Near.Marketplace
{
    public static class Views
    {
        public static async Task<dynamic> LoadCards(string fromIndex = "0", int limit = 50)
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetNftContract();
            dynamic args = new ExpandoObject();
            args.from_index = fromIndex;
            args.limit = 50;

            return gameContract.View("nft_tokens", args).result;
        }
    }
}