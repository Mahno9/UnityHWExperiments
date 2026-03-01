using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Navigation.Timer
{
    public class HorizontalDiscreteStatusView
    {
        private readonly GameObject            _statusIconPrefab;
        private readonly HorizontalLayoutGroup _layout;

        private          int              _maxValue;
        private          int              _currentValue;
        private readonly List<GameObject> _instances = new();

        public HorizontalDiscreteStatusView(HorizontalLayoutGroup layout, GameObject statusIconPrefab)
        {
            _statusIconPrefab = statusIconPrefab;
            _layout = layout;
        }

        public void InitMaxValue(int value)
        {
            if (_maxValue == value)
                return;

            Clear();
            _maxValue = value;

            for (int i = 0; i < _maxValue; i++)
            {
                GameObject newInstance = Object.Instantiate(_statusIconPrefab, _layout.transform);
                _instances.Add(newInstance);
            }
        }

        public void UpdateStatus(int value)
        {
            int clampedValue = Mathf.Clamp(value, 0, _maxValue);

            for (int i = 0; i < _instances.Count; i++)
                _instances[i].SetActive(i < clampedValue);
        }

        public void UpdateStatus(float maxTime, float currentTime)
        {
            int seconds = (int)maxTime;
            if (_instances.Count != seconds)
                InitMaxValue(seconds);

            float clampedValue = Mathf.Clamp(currentTime / maxTime, 0, 1);
            UpdateStatus((int)(Mathf.Ceil(_maxValue * clampedValue)));
        }

        private void Clear()
        {
            foreach (GameObject instance in _instances)
                Object.Destroy(instance);

            _instances.Clear();
        }

        public void SetActive(bool isActive)
        {
            _layout.gameObject.SetActive(isActive);
        }
    }
}