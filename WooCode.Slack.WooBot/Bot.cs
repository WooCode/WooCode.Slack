using System;
using System.Linq;
using Nancy;
using Nancy.TinyIoc;
using WooCode.Slack.WebHooks;
using WooCode.Slack.WooBot.Handlers;

namespace WooCode.Slack.WooBot
{
    public class Bot : NancyModule
    {
        /// <summary>
        /// You have to call this when you start your server to load all handlers.
        /// </summary>
        public static void LoadHandlers()
        {
            var type = typeof(IHookHandler);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(x => type.IsAssignableFrom(x) && x.IsClass).ToList();

            types.ForEach(t =>
            {
                var target = (IHookHandler)Activator.CreateInstance(t);
                var name = target.GetType().Name.ToLower();

                if (name.EndsWith("handler"))
                    name = name.Substring(0, name.Length - 7);

                TinyIoCContainer.Current.Register(type, target, name);
            });
        }

        public Bot()
        {
            Post["/hook"] = HandleSlashCommand;
        }

        private object HandleSlashCommand(dynamic args)
        {
            IncomingMessage message = IncomingMessage.From(Request.Form);

            var values = message.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            
            var handlerName = values[message.Command == null ? 1 : 0].ToLower();

            message.Text = string.Join(" ", values.Skip(message.Command == null ? 2 : 1));
            var handler = TinyIoCContainer.Current.Resolve<IHookHandler>(handlerName);

            if(handler != null)
                handler.Handle(message);

            return 200;
        }
    }
}
