using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace WooCode.Slack.WebHooks
{
    public class Message
    {
        static readonly Dictionary<string, string> EscapeDictionary = new Dictionary<string, string>
        {
            {"&", "&amp;"},
            {"<", "&lt;"},
            {">", "&gt;"}
        };
        [JsonIgnore]
        public string HookUrl { get; set; }
        public string Text { get; set; }
        [JsonProperty(PropertyName = "icon_url")]
        public string IconUrl { get; set; }
        public string UserName { get; set; }
        private string _channel;

        public string Channel
        {
            get { return _channel; }
            set { _channel = value.StartsWith("#") || value.StartsWith("@") ? value : "#" + value; }
        }

        [JsonProperty(PropertyName = "unfurl_links")]
        public bool UnfurlLinks { get; set; }
        
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Create new instance and read all properties from configuration file.
        /// </summary>
        public Message()
        {
            HookUrl = ConfigurationManager.AppSettings["Slack.HookUrl"];
            Text = ConfigurationManager.AppSettings["Slack.Text"];
            UserName = ConfigurationManager.AppSettings["Slack.UserName"];
            IconUrl = ConfigurationManager.AppSettings["Slack.Icon"];
            Channel = ConfigurationManager.AppSettings["Slack.Channel"];
            Attachments = new List<Attachment>();
        }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="text">Message text</param>
        /// <param name="channel">The channel you want the message to be sent to</param>
        /// <param name="userName">What username should the message have</param>
        public Message(string text, string channel = null, string userName = null) : this()
        {
            Text = text ?? Text;
            Channel = channel ?? Channel;
            UserName = userName ?? UserName;
        }

        public void Send()
        {
            var data = Converter.ToJson(this);
            data = EscapeDictionary.Aggregate(data, (current, escapeChar) => current.Replace(escapeChar.Key, escapeChar.Value));

            using (var client = new WebClient())
                client.UploadString(new Uri(HookUrl), "POST", data);
        }
    }
}