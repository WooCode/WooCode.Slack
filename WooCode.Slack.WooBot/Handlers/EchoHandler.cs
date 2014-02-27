using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public class EchoHandler : IHookHandler
    {
        public Message Handle(IncomingMessage message)
        {
            return new Message
            {
                Text = string.Format("@{0} {1}", message.UserName, message.Text),
                Channel = message.ChannelName
            };
        }
    }
}
