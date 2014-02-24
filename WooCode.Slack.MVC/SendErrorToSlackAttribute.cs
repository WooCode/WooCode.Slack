using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WooCode.Slack.WebHooks;

namespace WooCode.Slack.MVC
{
    /// <summary>
    /// Decorate your MVC controller with this attribute to send exceptions to your default slack channel (configured in web.config)
    /// </summary>
    public class SendErrorToSlackAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var message = new Message();
            message.AttachExceptionContext(filterContext);
            WebHook.Send(message);

            base.OnException(filterContext);
        }
    }
}
