using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace Microsoft.VisualStudio.QualityTools.WebTestFramework.Contrib.Utils
{
    public class TokenReplacer
    {
        public static string Replace(string source, WebTestContext context)
        {
            var processed = new StringBuilder(source);
            var tokens = Regex.Matches(source, @"{{(\w+)}}");
            if (tokens.Count > 0)
            {
                foreach (Match token in tokens)
                {
                    processed = processed.Replace(token.Value,
                        context[token.Groups[1].Value].ToString());
                }
            }
            return processed.ToString();
        }
    }
}
