using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WooCode.Slack.WebHooks;
using Xunit;

namespace WooCode.Slack.Test
{
    /// <summary>
    /// Be sure to change the App.config before running this test that will send a message
    /// </summary>
    public class Tests
    {
        [Fact]
        public void PostToSlack()
        {
            try
            {
                string test = null;
                var len = test.Length;
            }
            catch (Exception e)
            {
                var message = new Message("I think something is wrong.", "#exceptions");
                message.AttachException(new NotSupportedException("I FAILED YOU MASTER",e));
                WebHook.Send(message);
            }
        }
    }
}
