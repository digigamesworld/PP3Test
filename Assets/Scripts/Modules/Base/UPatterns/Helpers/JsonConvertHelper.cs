using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace UPatterns
{
    public static class JsonConvertHelper
    {
        public static JsonSerializerSettings JsonSettingsNullRemover => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
    }
}