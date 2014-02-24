using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace WooCode.Slack
{
    public class SlashCommand
    {
        public string Token { get; set; }
        [JsonProperty(PropertyName = "team_id")]
        public string TeamId { get; set; }
        [JsonProperty(PropertyName = "channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty(PropertyName = "channel_name")]
        public string ChannelName { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }
        public string Command { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// Create a new instance of SlashCommand from JSON string
        /// </summary>
        /// <param name="json">String containing JSON data</param>
        /// <returns></returns>
        public static SlashCommand From(string json)
        {
            return JsonConvert.DeserializeObject<SlashCommand>(json);
        }

        /// <summary>
        /// Create a new instance of SlashCommand from JSON stream
        /// </summary>
        /// <param name="jsonStream">Stream containing JSON data</param>
        /// <returns></returns>
        public static SlashCommand From(Stream jsonStream)
        {
            using(var reader = new StreamReader(jsonStream,Encoding.UTF8))
                return From(reader.ReadToEnd());
        }
    }
}
