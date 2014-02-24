using System.Web.Mvc;
using WooCode.Slack.WebHooks;

namespace WooCode.Slack.MVC
{
    public static class MessageMvcExtensions
    {
        public static void AttachExceptionContext(this Message @this, ExceptionContext filterContext)
        {          
            var attachment = new Attachment
            {
                Color = AttachmentColor.Warning
            };

            attachment.Fields.Add(new AttachmentField() { Title = "Controller", Value = filterContext.Controller.GetType().Name, Short = true });
            attachment.Fields.Add(new AttachmentField() { Title = "Path", Value = filterContext.HttpContext.Request.Path, Short = true });
            @this.Attachments.Add(attachment);
            @this.AttachException(filterContext.Exception);
        }
    }
}