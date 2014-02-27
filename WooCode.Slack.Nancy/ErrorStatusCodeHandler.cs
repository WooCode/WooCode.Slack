using System.Linq;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using WooCode.Slack.WebHooks;

namespace WooCode.Slack.NancyHost
{
    public sealed class ErrorStatusCodeHandler : IStatusCodeHandler
    {
        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound
                || statusCode == HttpStatusCode.InternalServerError
                || statusCode == HttpStatusCode.Forbidden
                || statusCode == HttpStatusCode.Unauthorized;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var statusText = "";
            switch (statusCode)
            {
                case HttpStatusCode.Forbidden:
                    statusText = "Sorry, you do not have permission to perform that action. Please contact your Octopus administrator.";
                    break;
                case HttpStatusCode.NotFound:
                    statusText = "Sorry, the resource you requested was not found.";
                    break;
                case HttpStatusCode.InternalServerError:
                    statusText = "An unexpected error occurred.";
                    break;
                default:
                    statusText = "The resource you requested was not found.";
                    break;
            }

            context.Response = new TextResponse("WooBot : " + statusText).WithStatusCode(statusCode);;
        }

        static bool ShouldRenderFriendlyErrorPage(NancyContext context)
        {
            var enumerable = context.Request.Headers.Accept;

            var ranges = enumerable.OrderByDescending(o => o.Item2).Select(o => MediaRange.FromString(o.Item1)).ToList();
            foreach (var item in ranges)
            {
                if (item.Matches("application/json"))
                    return false;
                if (item.Matches("text/json"))
                    return false;
                if (item.Matches("text/html"))
                    return true;
            }

            return true;
        }
    }
}
