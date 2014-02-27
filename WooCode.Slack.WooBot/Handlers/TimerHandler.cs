using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public class TimerHandler : IHookHandler
    {
        ConcurrentDictionary<string,Stopwatch> _timers = new ConcurrentDictionary<string, Stopwatch>();

        public Message Handle(IncomingMessage message)
        {
            var verb = message.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)[0];

            var msg = new Message(null, message.ChannelName);

            switch (verb.ToLower())
            {
                case "start":
                    _timers.AddOrUpdate(message.UserId, (uid) =>
                    {
                        var newWatch = new Stopwatch();
                        newWatch.Start();
                        msg.Text = string.Format("Started new timer for @{0}.", message.UserName);
                        return newWatch;
                    }, (uid, watch) =>
                    {
                        watch.Stop();
                        var newWatch = new Stopwatch();
                        newWatch.Start();
                        msg.Text = string.Format("Stopped at {0} and started new timer for @{1}.",watch.Elapsed.ToString("g"),message.UserName);
                        return newWatch;
                    });
                    break;
                case "stop":
                    Stopwatch currentWatch = null;
                    if (_timers.TryGetValue(message.UserId, out currentWatch))
                    {
                        currentWatch.Stop();
                        msg.Text = string.Format("Timer stopped at {0} for @{1}.", currentWatch.Elapsed.ToString("g"), message.UserName);
                    }
                    break;
                default:
                    msg = null;
                    break;
            }

            return msg;
        }
    }
}
