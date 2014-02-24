using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WooCode.Slack.WebHooks;

namespace WooCode.Slack.MVC
{
    public class SendErrorToSlackAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var message = new Message("I think something is wrong.", "#exceptions");
            message.AttachExceptionContext(filterContext);
            WebHook.Send(message);

            base.OnException(filterContext);
        }
    }
}
