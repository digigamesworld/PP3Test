using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UPatterns;

public class TextureCacherRuntimeTest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private string url;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sr.sprite = TextureCacher.LoadSprite(url.ConvertToHashImgName(), TextureCacheMode.PersistentCache);

            if (sr.sprite == null)
                TextureDownloader();
        }
    }

    private void TextureDownloader()
    {
        print("Downloading Texture");
        var req = UnityWebRequestTexture.GetTexture(url);
        req.downloadHandler = new DownloadHandlerTexture();
        req.certificateHandler = new CustomCertificateHandler();

        req.SendWebRequest().completed += Callback;

        void Callback(AsyncOperation operation)
        {
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(req.error);
                return;
            }

            var tex = DownloadHandlerTexture.GetContent(req);
            TextureCacher.Save(url.ConvertToHashImgName(), tex, TextureCacheMode.PersistentCache);
        }
    }

    public class CustomCertificateHandler : CertificateHandler
    {
            protected override bool ValidateCertificate(byte[] certificateData) => true;
    }
}
