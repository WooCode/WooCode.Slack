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
        [JsonIgnore]
        public string HookUrl { get; set; }

        public string Text { get; set; }
        public string Channel { get; set; }
        public string UserName { get; set; }
        public string Icon_Url { get; set; }
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
            Icon_Url = ConfigurationManager.AppSettings["Slack.Icon"];
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

        public void AttachException<T>(T exception) where T : Exception
        {
            var attachment = new Attachment();
            attachment.Color = AttachmentColors.Danger;
            attachment.Fields.Add(new AttachmentField() { Title = "Type", Value = exception.GetType().FullName, Short = true });
            attachment.Fields.Add(new AttachmentField() { Title = "Message", Value = exception.Message, Short = true });

            if(exception.StackTrace != null)
                attachment.Fields.Add(new AttachmentField() { Title = "StackTrace", Value = exception.StackTrace, Short = false });
            
            if (exception.InnerException != null)
            {
                
                attachment.Fields.Add(new AttachmentField() { Title = "InnerException Type", Value = exception.InnerException.GetType().FullName, Short = true });
                attachment.Fields.Add(new AttachmentField() { Title = "InnerException Message", Value = exception.InnerException.Message, Short = true });
                if (exception.InnerException.StackTrace != null)
                    attachment.Fields.Add(new AttachmentField() { Title = "InnerException StackTrace", Value = exception.InnerException.StackTrace, Short = false });
            }

            Attachments.Add(attachment);
        }
    }
}