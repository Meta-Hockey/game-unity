using System;
using Near.MarketplaceContract.Parsers;
using Near.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Near.MarketplaceContract
{
    public class ExtraConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Extra));
        }

        private IExtraParser MapDataToParser(JObject data)
        {
            string type = data["type"] != null ? data["type"].ToString() : data["player_type"].ToString();
            
            return type switch
            {
                "Goalie" => new GoalieExtraParser(),
                "GoaliePos" => new GoalieExtraParser(),
                "FieldPlayer" => new FieldPlayerExtraParser(),
                _ => throw new Exception("Extra type not found")
            };
        }
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var extra = JObject.Load(reader);
            IExtraParser parser = MapDataToParser(extra);
            return parser.ParseExtra(extra);
        }
    }
}