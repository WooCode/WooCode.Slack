using Newtonsoft.Json;

namespace WooCode.Slack
{
    public static class Converter
    {
        static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new LowercaseContractResolver()
        };

        public static string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSerializerSettings);
        }

    }
}