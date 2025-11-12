using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace UPatterns.Networking
{
    public record UHTTPConfig(string BaseURL = "", KeyValuePair<string, string>[] DefaultHeaders = default, bool UseBearerPrefixAuthHeader = false, bool Logger = true);
    public static class UHTTP
    {
        public const string PATCH_VERB = "PATCH";

        public static UHTTPConfig Config { get; set; }

        private static Action onTokenExpired;
        public static void OnTokenExpired(Action action) =>
            onTokenExpired = action;

        private static Action<bool> LoadingAction;
        public static void SetLoading(Action<bool> action) =>
            LoadingAction = action;

        public static string Token { private set; get; } = null;
        public static void SetToken(string token) =>
            Token = token;

        private static Action<UnityWebRequest> OnError = req => Debug.LogError("ERROR: =>>> " + $"{req.url}\n" + req.error + "\n" + req.downloadHandler.text);
        public static void SetOnError(Action<UnityWebRequest> action) =>
            OnError = action;

        public static UnityWebRequest SetupRequest(this UnityWebRequest req, string body = null, KeyValuePair<string, string>[] headers = default)
        {
            req.downloadHandler = new DownloadHandlerBuffer();
            req.AddBody(body);
            req.AddHeaders(Config.DefaultHeaders);
            req.AddHeaders(headers);

#if UNITY_EDITOR
            if (Config.Logger) Debug.Log(GenerateCurlCommand(req, headers, body));
#endif

            return req;
        }

        public static UnityWebRequest CreateRequest(string appendUrl, string method, string body = null, KeyValuePair<string, string>[] headers = default, bool dontAppendURL = false) =>
            new UnityWebRequest((!dontAppendURL ? Config.BaseURL : "") + appendUrl, method).SetupRequest(body, headers);
        public static UnityWebRequest CreateRequest(string appendUrl, WWWForm form = null, KeyValuePair<string, string>[] headers = default, bool dontAppendURL = false) =>
            UnityWebRequest.Post((!dontAppendURL ? Config.BaseURL : "") + appendUrl, form).SetupRequest("", headers);
        public static UnityWebRequest CreateGetTextureRequest(string appendUrl, string body = null, KeyValuePair<string, string>[] headers = default, bool dontAppendURL = false) =>
            UnityWebRequestTexture.GetTexture((!dontAppendURL ? Config.BaseURL : "") + appendUrl).SetupRequest(body, headers);

        public static void AddToken(this UnityWebRequest request) =>
            request.SetRequestHeader("Authorization", Config.UseBearerPrefixAuthHeader ? $"Bearer {Token}" : Token);
        public static void AddHeaders(this UnityWebRequest request, KeyValuePair<string, string>[] headers)
        {
            if (headers == null || headers.Length == 0) return;
            headers.ForEach(header => request.SetRequestHeader(header.Key, header.Value));
        }
        public static void AddBody(this UnityWebRequest request, string body)
        {
            if (!string.IsNullOrEmpty(body))
                request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
        }

        public static void Send(this UnityWebRequest request, Action onComplete = null, bool addTokenIfExist = true, bool haveLoading = false)
        {
            if (haveLoading)
                LoadingAction?.Invoke(true);

            if (addTokenIfExist && !string.IsNullOrEmpty(Token))
                request.AddToken();

            request.SendWebRequest().completed += Response;

            void Response(AsyncOperation ao)
            {
                if (haveLoading) LoadingAction?.Invoke(false);

                if (request.responseCode == 401 && addTokenIfExist && Token != null && onTokenExpired != null)
                {
                    Token = null;
                    onTokenExpired();
                    return;
                }

                onComplete?.Invoke();
            }
        }

        public static T GetData<T>(this UnityWebRequest request) where T : class
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                if (Config.Logger) Debug.Log($"[UHTTP:Error]\n[REQUEST:{request.url}]\n Error:{request.error}");
#endif
                OnError?.Invoke(request);
                return null;
            }

#if UNITY_EDITOR
            if (Config.Logger) Debug.Log($"[UHTTP:Response]\n[REQUEST:{request.url}]\n Data:{request.downloadHandler.text}");
#endif

            string json = request.downloadHandler.text;

            try
            {
                return JsonConvert.DeserializeObject<T>(json); // Newton soft
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                if (Config.Logger) Debug.LogError($"[UHTTP:DeserializationError]\n[REQUEST:{request.url}]\n Exception:{ex}");
#endif
                OnError?.Invoke(request);
                return null;
            }
        }

        private static string GenerateCurlCommand(UnityWebRequest request, KeyValuePair<string, string>[] headers, string body)
        {
            System.Text.StringBuilder curl = new("curl -X " + request.method);
            if (headers != null && headers.Length > 0)
                headers.ForEach(header => curl.Append($" -H \"{header.Key}: {header.Value}\""));

            if (!string.IsNullOrEmpty(body))
                curl.Append($" -d '{body}'");

            curl.Append($" \"{request.url}\"");

            return curl.ToString();
        }
    }
}