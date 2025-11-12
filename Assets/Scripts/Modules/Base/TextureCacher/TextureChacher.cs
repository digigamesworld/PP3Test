using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UPatterns;

public enum TextureCacheMode
{
    None,
    Cache,
    PersistentCache
}

public static class TextureCacher
{
#if UNITY_EDITOR
    private const bool DEBUG = false;
#endif

    private static Dictionary<string, Texture2D> cache = new();
    private static Dictionary<string, Sprite> cacheSpr = new();

    private static string GetPathFile(string fileName) => Application.persistentDataPath + "/" + fileName;

    public static Sprite LoadSprite(string key, TextureCacheMode cacheMode = TextureCacheMode.Cache)
    {
        if (cacheSpr.ContainsKey(key))
            return cacheSpr[key];

        Texture2D tex = Load(key);

        if (tex == null)
            return null;

        Sprite spr = tex.CreateSprite();
        SaveSprite(key, spr);
        return spr;
    }
    public static Texture2D Load(string key, TextureCacheMode cacheMode = TextureCacheMode.Cache)
    {
        if (cache.ContainsKey(key))
            return cache[key];

        if (cacheMode != TextureCacheMode.PersistentCache)
            return null; 

        Texture2D tex = LoadFromFile(key);

        if (tex == null)
            return null;

        Save(key, tex);
        return tex;
    }
    public static Texture2D LoadFromFile(string fileName)
    {
        Texture2D tex = null;

        try
        {
            if (File.Exists(GetPathFile(fileName)))
            {
                byte[] bytes = File.ReadAllBytes(fileName);
                tex = new Texture2D(2, 2);
                if (tex.LoadImage(bytes))
                { }
#if UNITY_EDITOR
                // else
                // if(DEBUG) Debug.Log("Failed to load image from file.");
#endif
            }
#if UNITY_EDITOR
            // else
            //     if(DEBUG) Debug.Log($"File not found: {fileName}");
#endif
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            //if(DEBUG) Debug.Log($"Failed to load texture: {e.Message}");
#endif
        }

        return tex;
    }

    public static void Save(string key, Texture2D tex, TextureCacheMode cacheMode)
    {
        Save(key, tex);

        if (cacheMode == TextureCacheMode.PersistentCache)
            SaveOnFile(key, tex);
    }
    public static void Save(string key, Texture2D texture)
    {
        if (texture == null || string.IsNullOrEmpty(key)) return;

        if (cache.ContainsKey(key))
            cache[key] = texture;
        else
            cache.Add(key, texture);
    }
    public static void SaveOnFile(string fileName, Texture2D texture)
    {
        try
        {
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(GetPathFile(fileName), bytes);
#if UNITY_EDITOR
            if(DEBUG) Debug.Log($"Texture saved at: {GetPathFile(fileName)}");
#endif

        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            if(DEBUG) Debug.LogError($"Failed to save texture: {e.Message}");
#endif
        }
    }

    public static void SaveSprite(string key, Sprite spr)
    {
        if (spr == null || string.IsNullOrEmpty(key)) return;

        if (cacheSpr.ContainsKey(key))
            cacheSpr[key] = spr;
        else
            cacheSpr.Add(key, spr);
    }
}