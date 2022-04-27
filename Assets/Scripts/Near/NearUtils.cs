using NearClientUnity.Utilities;

namespace Near
{
    public static class NearUtils
    {
        public static readonly ulong GasMakeAvailable = 300_000_000_000_000;
        public static readonly ulong GasMove = 50_000_000_000_000;
        
        public static readonly int MinterRoyaltyCap = 2000;
        public static readonly ulong GasMint = 200000000000000;
        
        private static readonly UInt128 NearNominationExp = UInt128.Parse("1000000000000000000000000");

        public static UInt128 ParseNearAmount(string amount)
        {
            return UInt128.Parse(amount) * NearNominationExp;
        }
        
        public static UInt128 FormatNearAmount(UInt128 amount)
        {
            return amount / NearNominationExp;
        }
    }
}