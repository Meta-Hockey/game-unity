using System;
using Near.MarketplaceContract.Parsers;
using Near.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Near.MarketplaceContract
{
    public class ExtraConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }
        
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Extra));
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private IExtraParser MapDataToParser(JObject data)
        {
            switch (data["type"].ToString())
            {
                case "Goalie":
                    return new GoalieExtraParser();
                default:
                    throw new Exception("Extra type not found");
            }
        }
        
        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var extra = JObject.Load(reader);
            IExtraParser parser = MapDataToParser(extra);
            return parser.ParseExtra(extra);
        }
    }
}