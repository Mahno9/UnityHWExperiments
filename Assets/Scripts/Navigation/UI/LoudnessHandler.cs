using System;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Navigation.UI
{
    [Serializable]
    struct SourceNameSliderPair
    {
        public string SourceName;
        public Slider Slider;
    }

    public class LoudnessHandler : MonoBehaviour
    {
        private const float MinDb = -80;

        [SerializeField] private AudioMixer                 _mixer;
        [SerializeField] private List<SourceNameSliderPair> _sourceSliders;

        private void Awake()
        {
            foreach (SourceNameSliderPair sourceNameSlider in _sourceSliders)
            {
                sourceNameSlider.Slider.onValueChanged.AddListener(x =>
                {
                    float db = Math.Max((float)(20*Math.Log10(x)), MinDb);
                    _mixer.SetFloat(sourceNameSlider.SourceName, db);
                });
            }
        }


        private void OnDestroy()
        {
            foreach (SourceNameSliderPair sourceNameSlider in _sourceSliders)
            {
                sourceNameSlider.Slider.onValueChanged.RemoveAllListeners();
            }
        }
    }
}