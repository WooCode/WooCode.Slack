using Nancy;
using Nancy.ModelBinding;
using WooCode.Slack.WebHooks;

namespace WooCode.Slack.Nancy
{
    public class SlackModule : NancyModule
    {
        public SlackModule()
        {
            Post["/slash"] = HandleSlashCommand;
        }

        private object HandleSlashCommand(object arg)
        {
            var model = this.Bind();
            SlashCommand slash = SlashCommand.From(model);
            
            switch (slash.Command.ToLower())
            {
                case "/hello":
                    new Message
                    {
                        Text = string.Format("Hello @{0}, how are you?", slash.UserName),
                        Channel = slash.ChannelName
                    }.Send();
                    break;
                case "/echo":
                    new Message
                    {
                        Text = string.Format("@{0} {1}", slash.UserName, slash.Text),
                        Channel = slash.ChannelName
                    }.Send();
                    break;
            }

            return 200;
        }
    }
}
