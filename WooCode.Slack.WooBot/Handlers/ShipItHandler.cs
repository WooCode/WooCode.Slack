using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public class ShipItHandler : IHookHandler
    {
        public void Handle(IncomingMessage message)
        {
            new Message
            {
                Text = string.Format("Ship IT!"),
                Channel = message.ChannelName
            }.Send();
        }
    }
}