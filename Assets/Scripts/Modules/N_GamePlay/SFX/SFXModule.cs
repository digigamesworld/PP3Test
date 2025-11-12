using UnityEngine;
using System.Collections.Generic;


namespace PP3.Core
{

    public sealed class SFXModule : IModule
    {
        private InputModule _input;
        private AudioSource _source;
        private Dictionary<string, AudioClip> _clips = new();

        public SFXModule(AudioSource src = null, SFXConfig cfg = null)
        {
            _source = src;
            if (cfg != null)
            {
                foreach (var e in cfg.entries)
                    if (!_clips.ContainsKey(e.id))
                        _clips.Add(e.id, e.clip);
            }
        }

        public void Init(Entity e)
        {
            _input = e.Get<InputModule>();
            if (_input != null)
                _input.OnCommand += OnCmd;
        }

        void OnCmd(ICommand c)
        {
            if (c is HornCmd h && h.Pressed)
                Play("horn");
        }

        public void Tick(float dt) { }

        public void Dispose()
        {
            if (_input != null)
                _input.OnCommand -= OnCmd;
        }

        /// <summary>
        /// Plays a sound effect by ID from the registered config.
        /// </summary>
        public void Play(string id)
        {
            if (_source == null) return;

            if (_clips.TryGetValue(id, out var clip) && clip != null)
                _source.PlayOneShot(clip);
        }
    }

}
