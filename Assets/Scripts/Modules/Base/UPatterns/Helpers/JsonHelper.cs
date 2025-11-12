using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace UPatterns
{
    public static class JsonHelper
    {
        public static string Create(string key, string value) =>
            $"{{\"{key}\":\"{value}\"}}";

        public static string CreateFromList(Dictionary<string, string> keyValuePairs)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{");

            foreach (var pair in keyValuePairs)
                stringBuilder.Append($"\"{pair.Key}\":\"{pair.Value}\",");

            // Remove the trailing comma
            if (keyValuePairs.Count > 0)
                stringBuilder.Length--;

            return stringBuilder.Append("}").ToString();
        }

        public static string ChangeValue(string json, string key, string curValue, string value)
        {
            JObject obj = JObject.Parse(json);

            if (obj[key]?.ToString() == curValue)
                obj[key] = value;

            return obj.ToString();
        }
    }
}