using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace UPatterns
{
    public class TextureDownloader : MonoBehaviour
    {
        private static TextureDownloader instance;
        private static TextureDownloader Instance => instance ??=
            new GameObject(typeof(TextureDownloader).Name).AddComponent<TextureDownloader>();

        private Dictionary<string,List<Action<Texture2D>>> pool = new();

        private void Awake() => DontDestroyOnLoad(gameObject);

        public static void LoadSprite(string url, Action<Sprite> callback, TextureCacheMode cache = TextureCacheMode.None)
        {
            if (string.IsNullOrEmpty(url))
                callback?.Invoke(null);

            Sprite spr = TextureCacher.LoadSprite(url.ConvertToHashImgName());

            if (spr != null)
            {
                callback?.Invoke(spr);
                return;
            }

            LoadTexture(url, Callback, cache);

            void Callback(Texture2D tex)
            {
                if (tex == null)
                {
                    callback?.Invoke(null);
                    return;
                }

                spr = tex.CreateSprite();
                callback?.Invoke(spr);
            }
        }

        public static void LoadTexture(string url, Action<Texture2D> callback, TextureCacheMode cache = TextureCacheMode.None)
        {
            if (cache != TextureCacheMode.None)
            {
                Texture2D tex = TextureCacher.Load(url.ConvertToHashImgName());
                if (tex != null)
                {
                    callback?.Invoke(tex);
                    return;
                }
            }

            Instance.Downloader(url, callback, cache);
        }

        private void Downloader(string url, Action<Texture2D> callback, TextureCacheMode cach)
        {
            if (pool.ContainsKey(url))
            {
                pool[url].Add(callback);
                return;
            }    

            pool.Add(url, new ());
            pool[url].Add(callback);
            
            var req = UnityWebRequestTexture.GetTexture(url);
            req.downloadHandler = new DownloadHandlerTexture();
            req.certificateHandler = new TextureDownloaderCertificateHandler();

            req.SendWebRequest().completed += Callback;

            void Callback(AsyncOperation operation)
            {
                if (req.result != UnityWebRequest.Result.Success)
                {
#if UNITY_EDITOR
                    Debug.Log($"[TextureDownloader:Error]\n[REQUEST:{url}]\n Error:{req.error}");
#endif
                    return;
                }

                var tex = DownloadHandlerTexture.GetContent(req);
                if (cach != TextureCacheMode.None)
                    TextureCacher.Save(url.ConvertToHashImgName(), tex, TextureCacheMode.PersistentCache);

                pool[url].ForEach(x => x?.Invoke(tex));
                pool.Remove(url);
            }
        }
    }

    public class TextureDownloaderCertificateHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData) => true;
    }
}