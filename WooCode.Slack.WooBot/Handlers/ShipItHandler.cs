using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public class ShipItHandler : IHookHandler
    {
        public Message Handle(IncomingMessage message)
        {
            return new Message
            {
                Text = string.Format("Ship IT!"),
                Channel = message.ChannelName
            };
        }
    }
}