using System;
using System.Dynamic;
using System.Threading.Tasks;
using NearClientUnity;

namespace Near.GameContract
{
    public static class Actions
    {
        public static async Task MakeAvailable(string bid)
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.config = new Object();
            await gameContract.Change("make_available", args,
                NearUtils.GasMakeAvailable,
                NearUtils.ParseNearAmount(bid));
        }

        public static async Task MakeUnavailable()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();

            await gameContract.Change("make_unavailable", args);
        }
    }
}