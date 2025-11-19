using System;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ProgressbarValueUpdater : MonoBehaviour
{
    [SerializeField] private BaseValueContainer _container;
    [SerializeField] private Image _progressbar;

    private void Update()
    {
        Assert.IsNotNull(_container);

        _progressbar.fillAmount = _container.GetValue() / _container.GetValueMax();
    }
}