WooCode.Slack
=============

Library for [slacking](https://slack.com/) in .Net

Basic support for WebHooks and SlashCommands (WooBot + Nancy project).

Prepare you config
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


``` csharp
// Create a new message with properties from app.config
var message = new Message("I think something is wrong.");
// Or supply some values
message = new Message("I think something is wrong.", "#exceptions","MyName");
// Add attachments or exceptions (will be added as attachments)
message.AttachException(new NotSupportedException("I FAILED YOU MASTER",e));
// Then you can fire it at the moon
message.Send();
```

*More information about WooCode.Slack.WooBot will come later, just check the [source](https://github.com/WooCode/WooCode.Slack/tree/develop/WooCode.Slack.WooBot) ;)*
