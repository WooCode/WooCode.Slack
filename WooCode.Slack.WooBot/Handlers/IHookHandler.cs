using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public interface IHookHandler
    {
        void Handle(IncomingMessage message);
    }
}