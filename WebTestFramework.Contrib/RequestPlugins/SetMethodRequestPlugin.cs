using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace Microsoft.VisualStudio.QualityTools.WebTestFramework.Contrib.RequestPlugins
{
    [DisplayName("Http method setter")]
    [Description("Set Http method of a request")]
    public class SetMethodRequestPlugin : WebTestRequestPlugin
    {
        public string Method { get; set; }

        public override void PreRequest(object sender, PreRequestEventArgs e)
        {
            base.PreRequest(sender, e);
            e.Request.Method = Method;
        }
    }
}
