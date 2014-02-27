using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public interface IHookHandler
    {
        Message Handle(IncomingMessage message);
    }
}