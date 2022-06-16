using System;
using Near.MarketplaceContract.Parsers;
using Near.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Near.GameContract.ContractMethods
{
    public class AvailableGameParser : JsonConverter
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
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var availableGame = JObject.Load(reader);

            return new AvailableGame()
            {
                GameId = (int) availableGame[0],
                PlayerIds = JsonConvert.DeserializeObject<Tuple<string, string>>(availableGame[1]?.ToString() ?? string.Empty)
            };
        }
    }
}