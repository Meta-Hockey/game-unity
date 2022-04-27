using System.Collections.Generic;

namespace Near.Models
{
    public class Sale
    {
        public string owner_id { get; set; }
        public ulong approval_id { get; set; }
        public string nft_contract_id { get; set; }
        public string token_id { get; set; }
        public ulong created_at { get; set; }
        public bool is_auction { get; set; }
        public string token_type  { get; set; }
        public Dictionary<string, string> sale_conditions  { get; set; }
        public Dictionary<string, List<Bid>> bids  { get; set; }
    }
}