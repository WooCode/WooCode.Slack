using System;
using System.Configuration;
using Nancy.Hosting.Self;
using WooCode.Slack.WooBot;
namespace WooCode.Slack.Nancy
{
    /// <summary>
    /// Simple SlackBot with Nancy
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Bot.LoadHandlers();

            var uri = new Uri(ConfigurationManager.AppSettings["Host"]);
            var config = new HostConfiguration {RewriteLocalhost = false};

            using (var host = new NancyHost(config, uri))
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
