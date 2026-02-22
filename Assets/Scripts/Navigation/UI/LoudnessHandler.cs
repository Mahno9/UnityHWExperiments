using System;
using System.Collections.Generic;

using Navigation.Sounds;

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
        [SerializeField] private AudioMixer                 _mixer;
        [SerializeField] private List<SourceNameSliderPair> _sourceSliders;

        private readonly List<LoudnessController> _controllers = new ();

        private void Awake()
        {
            foreach (SourceNameSliderPair sourceNameSlider in _sourceSliders)
            {
                LoudnessController controller = new (_mixer, sourceNameSlider.SourceName);
                _controllers.Add(controller);

                sourceNameSlider.Slider.onValueChanged.AddListener(x => controller.SetLoudness(x));
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