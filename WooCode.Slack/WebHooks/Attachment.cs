using System.Collections.Generic;

namespace WooCode.Slack.WebHooks
{
    public class Attachment
    {
        public string Fallback { get; set; }
        public string Text { get; set; }
        public string PreText { get; set; }
        public string Color { get; set; }
        public List<AttachmentField> Fields { get; set; }

        public Attachment()
        {
            Fields = new List<AttachmentField>();
        }
    }
}