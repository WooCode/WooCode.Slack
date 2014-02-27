using System;
using System.Configuration;
using Nancy.Hosting.Self;

namespace WooCode.Slack.NancyHost
{
    /// <summary>
    /// Simple SlackBot with Nancy
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri(ConfigurationManager.AppSettings["Host"]);
            var config = new HostConfiguration { RewriteLocalhost = false };

            using (var host = new Nancy.Hosting.Self.NancyHost(new BotBootstraper(), config, uri))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Enter [exit] to close the host.");
                while (Console.ReadLine() != "exit")
                {

                }
                host.Stop();
            }
        }
    }
}
