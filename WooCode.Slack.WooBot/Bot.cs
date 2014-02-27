using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using WooCode.Slack.WebHooks;
using WooCode.Slack.WooBot.Handlers;

namespace WooCode.Slack.WooBot
{
    public class Bot : NancyModule
    {
        public Bot()
        {
            Post["/"] = HandleSlashCommand;
        }

        private object HandleSlashCommand(dynamic args)
        {
            IncomingMessage message = IncomingMessage.From(Request.Form);

            var values = message.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string handlerName = values[message.Command == null ? 1 : 0].ToLower();
            message.Text = string.Join(" ", values.Skip(message.Command == null ? 2 : 1));
            Message response = null; 

            IHookHandler handler;
            if (TinyIoCContainer.Current.TryResolve(handlerName, out handler))
            {
                response = handler.Handle(message);
            }
            else
            {
                response = new Message
                {
                    Text = string.Format("@{0} I dont understand  \"{1}\"", message.UserName, handlerName + " " + message.Text),
                    Channel = message.ChannelName
                }; 
            }
                
            if(response != null)
            {
                // If it's a slashcommand we want PM the user.
                if (message.IsSlashCommand())
                    response.Channel = "@" + message.UserName;

                response.Send();
            }
            

            return 200;
        }
    }
}
