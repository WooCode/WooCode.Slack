WooCode.Slack
=============

Library for [slack](https://slack.com/) in .Net
- WebHooks(both incoming and outgoing)
- SlashCommand parsing 
- Bot (WooCode.Slack.WooBot + Nancy project)
- Send ASP exceptions to slack (WooCode.Slack.MVC, experimental)


## Hi and thank you for coming!
This library is developed by WooCode and we will add functions & *fix* fixes now and then. 

That said, we would love to see pullrequest and add your ideas, handlers, fixes and code to this project since we just add things that we need for our own integrations/automations right now.


## Prepare you config
``` xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <!-- host is only needed if you use WooBot with the Nancy host -->
    <add key="Host" value="http://127.0.0.1:1337"/>
    <!-- https://COMPANYNAME.slack.com/services/new and add Incoming WebHooks thats where you'll find your URL -->
    <add key="Slack.HookUrl"  value="https://COMPANYNAME.slack.com/services/hooks/incoming-webhook?token=TOKEN"/>
    <add key="Slack.Icon" value="http://www.woocode.com/head.png"/>
    <add key="Slack.UserName" value="WooBot"/>
    <add key="Slack.Channel" value="#exceptions"/>
    <add key="Slack.Text" value="Something is wrong"/>
  </appSettings>
</configuration>
```

## Send a message
``` csharp
// Create a new message with properties from app.config
var message = new Message("I think something is wrong.");
// Or supply some values
message = new Message("I think something is wrong.", channel:"exceptions", userName:"MyName");
// Add attachments or exceptions (will be added as attachments)
message.AttachException(new NotSupportedException("I FAILED YOU MASTER",e));
// Then you can fire it at the moon
message.Send();
```

## The Bot
![WooBot](http://i.imgur.com/yXlH3Md.png)

*More information about WooCode.Slack.WooBot will come later, just check the [source](https://github.com/WooCode/WooCode.Slack/tree/develop/WooCode.Slack.WooBot) ;)*

If you use the WooCode.Slack.Nancy host you have to direct SlashCommands AND/OR Outgoing webhooks to your **hosturl(app.config)/hook**

- WooCode.Slack.Nancy is the host project, just buid it and start your server (check the app.config).
- WooCode.Slack.WooBot contains the Bot code & handlers that are hosted in the Nancy project.

### Bot commands
``` bash
# Start a timer for the user that makes the request
/woobot timer start
# Stop the timer for the user and message the channel how long the timer was active
/woobot timer stop 
# Say hello
/woobot hello
# Echoes the <text> in the channel
/woobot echo <text> 
```

### Add custom commands/handlers
Check the source until we have time to describe it in detail ;)

Adding custom handlers is really easy. Take the following [EchoHandler](https://github.com/WooCode/WooCode.Slack/blob/develop/WooCode.Slack.WooBot/Handlers/EchoHandler.cs) for example.
``` csharp
using WooCode.Slack.WebHooks;

namespace WooCode.Slack.WooBot.Handlers
{
    public class EchoHandler : IHookHandler
    {
        public void Handle(IncomingMessage message)
        {
            new Message
            {
                Text = string.Format("@{0} {1}", message.UserName, message.Text),
                Channel = message.ChannelName
            }.Send();
        }
    }
}
```
if we write "/woobot echo some random text" in a channel the above handler will be invoked by convention **handlerAlias**Handler since the resolver ignores "Handler" in the end of the classname 

<code>message.Text</code> will contain "some random text" and <code>message.UserName</code> will contain the username of the user that invoked the command.

So to make your own handler you just create a new class in the Handlers folder with a name that ends with Handler (or not) and implement the <code>IHookHandler</code> interface, write your logic in <code>Handle</code> and you are done.
