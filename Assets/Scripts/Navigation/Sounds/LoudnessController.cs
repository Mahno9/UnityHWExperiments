using System;

using UnityEngine.Audio;

namespace Navigation.Sounds
{
    public class LoudnessController
    {
        private const float MinDb = -80;

        private readonly string     _sourceName;
        private readonly AudioMixer _mixer;

        public LoudnessController(AudioMixer mixer, string sourceName)
        {
            _sourceName = sourceName;
            _mixer = mixer;
        }

        public void SetLoudness(float value)
        {
            float db = Math.Max((float)(20 * Math.Log10(value)), MinDb);
            _mixer.SetFloat(_sourceName, db);
        }
    }
}