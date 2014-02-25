WooCode.Slack
=============

Library + Bot for [slack](https://slack.com/) in .Net

![WooBot](http://i.imgur.com/yXlH3Md.png)

Basic support for WebHooks and SlashCommands (WooBot + Nancy project).

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
*More information about WooCode.Slack.WooBot will come later, just check the [source](https://github.com/WooCode/WooCode.Slack/tree/develop/WooCode.Slack.WooBot) ;)*

If you use the Nancy host you have to direct SlashCommands AND/OR Outgoing webhooks to your **hosturl(app.config)/hook**

### Bot commands
``` bash
# Start a timer for the user that makes the request
/woobot timer start
# Stop the timer for the user and message the channel how the timer was active**
/woobot timer stop 
# Say hello
/woobot hello
# Echoes the <text> in the channel
/woobot echo <text> 
```

### Add custom commands
Check the source until we have time to describe it ;)
**TBD**
