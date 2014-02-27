using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public class HelloHandler : IHookHandler
    {
        public Message Handle(IncomingMessage message)
        {
            return new Message
            {
                Text = string.Format("Hello @{0}, how are you?", message.UserName),
                Channel = message.ChannelName
            };
        }
    }
}