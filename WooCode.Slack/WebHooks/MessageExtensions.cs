using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WooCode.Slack.WebHooks
{
    public static class MessageExtensions
    {
        public static void AttachException<T>(this Message @this, T exception) where T : Exception
        {
            var attachment = new Attachment
            {
                Color = AttachmentColor.Danger
            };
            attachment.Fields.Add(new AttachmentField() { Title = "Type", Value = exception.GetType().FullName, Short = true });
            attachment.Fields.Add(new AttachmentField() { Title = "Message", Value = exception.Message, Short = true });

            if (exception.StackTrace != null)
                attachment.Fields.Add(new AttachmentField() { Title = "StackTrace", Value = exception.StackTrace, Short = false });

            if (exception.InnerException != null)
            {
                attachment.Fields.Add(new AttachmentField() { Title = "InnerException Type", Value = exception.InnerException.GetType().FullName, Short = true });
                attachment.Fields.Add(new AttachmentField() { Title = "InnerException Message", Value = exception.InnerException.Message, Short = true });
                if (exception.InnerException.StackTrace != null)
                    attachment.Fields.Add(new AttachmentField() { Title = "InnerException StackTrace", Value = exception.InnerException.StackTrace, Short = false });
            }

            @this.Attachments.Add(attachment);
        }

    }
}
