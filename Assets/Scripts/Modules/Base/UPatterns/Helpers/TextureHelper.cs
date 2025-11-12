using System.IO;
using UnityEngine;

namespace UPatterns
{
    public static class TextureHelper
    {
        public static Sprite CreateSprite(this Texture2D texture) =>
            texture ? Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero) : null;

        public static Texture2D LoadFromFile(this Texture2D texture, string path)
        {
            texture.LoadImage(File.ReadAllBytes(path));
            return texture;
        }
            
    }
}