using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UPatterns
{
    public static class StringHelper
    {
        public static string ConvertToHashImgName(this string input, string ext = ".png") =>
            string.IsNullOrEmpty(input) ? "" : HashMD5(input) + (Path.GetExtension(input) ?? ext);

        public static string HashMD5(this string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                hashBytes.ForEach(b => sb.Append(b.ToString("x2")));
                return sb.ToString();
            }
        }

        public static string AddParam(this string str, string key, string value) =>
            str += string.IsNullOrEmpty(value) ? "" : $"{(string.IsNullOrEmpty(str) ? "?" : "&")}{key}={value}";

        public static string AddParams(Dictionary<string, string> Params)
        {
            string str = "";
            foreach (var param in Params)
                str = str.AddParam(param.Key, param.Value);
            return str;
        }

        public static string ThousandSeparatorFormatter(this int number) =>
            number.ToString("N0");

        public static string ConvertISOToReadable(this string isoDate)
        {
            DateTime dateTime = DateTime.Parse(isoDate).ToLocalTime();
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ConvertISOToReadable(this DateTime isoDate)
        {
            DateTime dateTime = isoDate.ToLocalTime();
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string Turncate(this string value, int maxLength) =>
            value.Length < maxLength ? value : value.Substring(0, maxLength - 3) + "...";
    }
}