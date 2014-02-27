using System;
using System.Linq;
using Nancy;
using Nancy.TinyIoc;
using WooCode.Slack.WooBot.Handlers;

namespace WooCode.Slack.NancyHost
{
    public class BotBootstraper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var type = typeof(IHookHandler);
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(x => type.IsAssignableFrom(x) && x.IsClass).ToList()
                .ForEach(t =>
                {
                    var name = t.Name.ToLower();
                    if (name.EndsWith("handler"))
                        name = name.Substring(0, name.Length - 7);

                    TinyIoCContainer.Current.Register(type, t, name);
                });
        }
    }
}
