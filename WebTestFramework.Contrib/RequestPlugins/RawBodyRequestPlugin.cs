using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace Microsoft.VisualStudio.QualityTools.WebTestFramework.Contrib.RequestPlugins
{
    [DisplayName("Raw body setter")]
    [Description("Set Raw body of a request")]
    public class RawBodyRequestPlugin : WebTestRequestPlugin
    {
        public string BodyString { get; set; }
        public string ContentType { get; set; }

        public override void PreRequest(object sender, PreRequestEventArgs e)
        {
            base.PreRequest(sender, e);
            var processedBody = BodyString;
            var tokens = Regex.Matches(BodyString, @"{{(\w+)}}");
            if (tokens.Count > 0)
            {
                foreach (Match token in tokens)
                {
                    processedBody = processedBody.Replace(token.Value,
                        e.WebTest.Context[token.Groups[1].Value].ToString());
                }
            }
            
            e.Request.Body = new StringHttpBody
            {
                BodyString = processedBody,
                ContentType = ContentType
            };
        }
    }
}
