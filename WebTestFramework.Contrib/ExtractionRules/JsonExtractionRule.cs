using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Newtonsoft.Json.Linq;

namespace Microsoft.VisualStudio.QualityTools.WebTestFramework.Contrib.ExtractionRules
{
    [DisplayName("Json value extraction rule")]
    [Description("Extract a single Json value from response body")]
    public class JsonExtractionRule : ExtractionRule
    {
        public string JsonPath { get; set; }
        public override void Extract(object sender, ExtractionEventArgs e)
        {
            var success = false;
            try
            {
                var responseJObject = JToken.Parse(e.Response.BodyString);
                var jToken = responseJObject.SelectToken(JsonPath);
                e.WebTest.Context.Add(ContextParameterName, jToken.Value<string>());
                success = true;
            }
            catch (Exception exception)
            {
                e.Message = $"Error extracting Json path: ({JsonPath}) from response body. {exception.Message}";
            }
            e.Success = success;
        }
    }
}
