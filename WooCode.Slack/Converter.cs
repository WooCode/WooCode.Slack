using System.IO.Ports;
using Newtonsoft.Json;

namespace WooCode.Slack
{
    public static class Converter
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new LowercaseContractResolver()
        };

        public static JsonSerializer GetSerializer()
        {
            var serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.ContractResolver = new LowercaseContractResolver();
            return serializer;
        }

        public static string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSerializerSettings);
        }

    }
}