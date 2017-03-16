using System.ComponentModel;
using Microsoft.VisualStudio.QualityTools.WebTestFramework.Contrib.Utils;
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
            e.Request.Body = new StringHttpBody
            {
                BodyString = TokenReplacer.Replace(BodyString, e.WebTest.Context),
                ContentType = ContentType
            };
        }
    }
}
