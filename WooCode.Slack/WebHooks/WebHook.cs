using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace WooCode.Slack.WebHooks
{
    public static class WebHook
    {
        static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings();
        static readonly Dictionary<string, string> EscapeDictionary = new Dictionary<string, string>
        {
            {"&", "&amp;"},
            {"<", "&lt;"},
            {">", "&gt;"}
        };


        static WebHook()
        {
            JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            JsonSerializerSettings.ContractResolver = new LowercaseContractResolver();
        }

        public static void Send(Message message)
        {          
            var data = JsonConvert.SerializeObject(message, JsonSerializerSettings);
            data = EscapeDictionary.Aggregate(data, (current, escapeChar) => current.Replace(escapeChar.Key, escapeChar.Value));

            using(var client = new WebClient())
                client.UploadStringAsync(new Uri(message.HookUrl), data);
        }
    }
}
