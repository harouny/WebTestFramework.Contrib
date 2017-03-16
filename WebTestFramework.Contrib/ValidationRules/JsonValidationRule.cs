using System;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Newtonsoft.Json.Linq;

namespace Microsoft.VisualStudio.QualityTools.WebTestFramework.Contrib.ValidationRules
{

    [DisplayName("Json value validation rule")]
    [Description("Validates that a single json value exists in resposne body")]
    public class JsonValidationRule : ValidationRule
    {
        [Description("A path to the target property in json body. For more info: http://goessner.net/articles/JsonPath/")]
        public string JsonPath { get; set; }
        [Description("A string that matches json value or json value.ToString() if value is not a string. This will override ValueRegex property.")]
        public string ExactValue { get; set; }
        [Description("A regular expression to use to validate value string or value.ToString() if value is not a string.")]
        public string ValueRegex { get; set; }
        public override void Validate(object sender, ValidationEventArgs e)
        {
            var isValid = false;
            if (e.Response.StatusCode == HttpStatusCode.OK
                || e.Response.StatusCode == HttpStatusCode.Created)
            {
                try
                {
                    var responseJObject = JToken.Parse(e.Response.BodyString);
                    var jToken = responseJObject.SelectToken(JsonPath);
                    var actualValue = jToken.Value<string>();
                    if (!string.IsNullOrEmpty(ExactValue))
                    {
                        isValid = string.Equals(ExactValue, actualValue, StringComparison.InvariantCultureIgnoreCase);
                    }
                    else if(!string.IsNullOrEmpty(ValueRegex))
                    {
                        isValid = Regex.IsMatch(actualValue, ValueRegex);
                    }
                    else
                    {
                        throw new Exception($"either {nameof(ValueRegex)} or {nameof(ExactValue)} are required");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            e.IsValid = isValid;
        }
    }
}
