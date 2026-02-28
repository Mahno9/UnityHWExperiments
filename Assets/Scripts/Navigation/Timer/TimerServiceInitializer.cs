using System;

using UnityEngine;
using UnityEngine.UI;

namespace Navigation.Timer
{
    public class TimerServiceInitializer : MonoBehaviour
    {
        [SerializeField] private float _timerTime = 10f;

        [SerializeField] private Slider _timerSlier;

        [SerializeField] private GameObject            _statusIconPrefab;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;

        private HorizontalDiscreteStatusView _discreteView;

        private TimerService                 _timerService;

        private void Awake()
        {
            _discreteView = new HorizontalDiscreteStatusView(_horizontalLayoutGroup, _statusIconPrefab);
            _discreteView.InitMaxValue((int)_timerTime);

            _timerSlier.minValue = 0f;

            _timerService = new TimerService();
            _timerService.OnTimerTicked += OnTimerTicked;

            _timerService.StartTimer(_timerTime);
        }

        private void OnDestroy()
        {
            _timerService.OnTimerTicked -= OnTimerTicked;
        }

        private void Update()
        {
            _timerService.Update(Time.deltaTime);
        }

        private void OnTimerTicked(float maxTime, float currentTime)
        {
            _timerSlier.value = currentTime;
            _timerSlier.maxValue = maxTime;

            // Debug.Log($"maxTime: {maxTime}; curTime: {currentTime}; value: {(int)currentTime}");
            _discreteView.UpdateStatus(currentTime / maxTime);
        }
    }
}