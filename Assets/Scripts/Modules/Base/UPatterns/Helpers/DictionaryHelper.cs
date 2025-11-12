using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UPatterns
{
    public static class DictionaryHelper
    {
        public static Dictionary<string, object> ToDictionary<T>(this T record)
        {
            var dict = new Dictionary<string, object>();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
                dict[prop.Name] = prop.GetValue(record);

            return dict;
        }

        public static Dictionary<string, string> ToDictionaryString<T>(this T record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            var dict = new Dictionary<string, string>();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
                dict[prop.Name] = prop.GetValue(record)?.ToString() ?? string.Empty;

            return dict;
        }

        public static Dictionary<string, string> AddOrOverwrite(
        this Dictionary<string, string> first,
        Dictionary<string, string> second)
        {
            foreach (var kvp in second)
                first[kvp.Key] = kvp.Value;

            return first;
        }

        public static string ToStringCustom(this Dictionary<string, string> dict, string separator = ", ") =>
            string.Join(separator, dict.Select(kvp => $"{kvp.Key}={kvp.Value}"));
    }
}