using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UPatterns;

namespace PP3.Data
{
    /// <summary>
    /// Simple JSON save/load system for PlayerProfileState.
    /// </summary>
    public static class SaveSystem
    {
        private static string SavePath => Path.Combine(Application.persistentDataPath, "player_profile.json");

        public static void Save()
        {
            var profile = UStateRepo.GetState<PlayerProfileState>();
            if (profile == null)
            {
                Debug.LogWarning("[SaveSystem] No PlayerProfileState to save.");
                return;
            }

            string json = JsonConvert.SerializeObject(profile, Formatting.Indented);
            File.WriteAllText(SavePath, json);
            Debug.Log($"[SaveSystem] Saved profile to: {SavePath}");
        }

        public static void Load()
        {
            if (!File.Exists(SavePath))
            {
                Debug.Log("[SaveSystem] No save file found — starting new profile.");
                return;
            }

            string json = File.ReadAllText(SavePath);
            var profile = UState.FromJson<PlayerProfileState>(json);
            UStateRepo.SetState(profile);
            Debug.Log("[SaveSystem] Loaded PlayerProfileState.");
        }

        public static void DeleteSave()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                Debug.Log("[SaveSystem] Save file deleted.");
            }
        }
    }
}
