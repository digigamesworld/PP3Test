using UPatterns;
using UnityEngine;
using UnityEngine.UI;

namespace UPatterns.Networking.Test
{
    public class TextureDownloaderTestRuntime : MonoBehaviour
    {
        [SerializeField] private string url;
        [SerializeField] private RawImage img;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TextureDownloader.LoadTexture(url, Load, TextureCacheMode.None);
        }

        private void Load(Texture2D tex2D)
        {
            if (!tex2D)
                print("Texture is null");
            else
                img.texture = tex2D;
        }
    }
}