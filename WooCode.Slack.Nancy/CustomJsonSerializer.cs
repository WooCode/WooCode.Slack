using System.Collections.Generic;
using System.IO;
using Nancy;
using Newtonsoft.Json;

namespace WooCode.Slack.NancyHost
{
    public sealed class CustomJsonSerializer : ISerializer
    {
        public bool CanSerialize(string contentType)
        {
            return true;
        }

        public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
        {
            using (var writer = new StreamWriter(outputStream))
                Converter.GetSerializer().Serialize(writer, model);

        }

        public IEnumerable<string> Extensions { get; private set; }
    }
}