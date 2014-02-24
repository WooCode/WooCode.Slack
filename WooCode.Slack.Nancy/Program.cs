using System;
using Nancy.Hosting.Self;

namespace WooCode.Slack.Nancy
{
    /// <summary>
    /// Simple SlackBot with Nancy
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var host = new NancyHost(new Uri("http://projects.woocode.com:1337"));
            host.Start();
            while (Console.ReadLine() != "exit")
            {
                
            }
            host.Stop();
        }
    }
}
