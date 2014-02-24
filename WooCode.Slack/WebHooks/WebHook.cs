using System.Net;
using Newtonsoft.Json;

namespace WooCode.Slack.WebHooks
{
    public static class WebHook
    {
        static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

        static WebHook()
        {
            _jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            _jsonSerializerSettings.ContractResolver = new LowercaseContractResolver();
        }

        public static void Send(Message message)
        {          
            var data = JsonConvert.SerializeObject(message, _jsonSerializerSettings);
            
            using(var client = new WebClient())
                client.UploadString(message.HookUrl, data);
        }
    }
}
