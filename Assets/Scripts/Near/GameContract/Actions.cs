using System;
using System.Dynamic;
using NearClientUnity;

namespace Near.GameContract
{
    public static class Actions
    {
        public static async void MakeAvailable(string bid)
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();
            args.config = new Object();

            await gameContract.Change("make_available", args,
                NearPersistentManager.
                    Instance.
                    GasMakeAvailable,
                NearPersistentManager.Instance.ParseNearAmount(bid));
        }

        public static async void MakeUnavailable()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();

            await gameContract.Change("make_unavailable", args);
        }
    }
}