using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;

namespace WooCode.Slack.WebHooks
{
    public class Message
    {
        private string _channel;

        [JsonIgnore]
        public string HookUrl { get; set; }

        public string Text { get; set; }

        public string Channel
        {
            get { return _channel; }
            set { _channel = value.StartsWith("#") ? value : "#" + value; }
        }

        public string UserName { get; set; }
        [JsonProperty(PropertyName = "icon_url")]
        public string IconUrl { get; set; }
        [JsonProperty(PropertyName = "unfurl_links")]
        public bool UnfurlLinks { get; set; }
        
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Create new instance and read all properties from configuration file.
        /// </summary>
        public Message()
        {
            Text = ConfigurationManager.AppSettings["Slack.Text"];
            Channel = ConfigurationManager.AppSettings["Slack.Channel"];
            UserName = ConfigurationManager.AppSettings["Slack.UserName"];
            HookUrl = ConfigurationManager.AppSettings["Slack.HookUrl"];
            IconUrl = ConfigurationManager.AppSettings["Slack.Icon"];
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
    }
}