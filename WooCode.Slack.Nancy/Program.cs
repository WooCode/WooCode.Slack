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
            var host = new NancyHost(new Uri("http://127.0.0.1:1337"));
            host.Start();
            Console.WriteLine("Started");
            while (Console.ReadLine() != "exit")
            {
                
            }
            host.Stop();
        }
    }
}
