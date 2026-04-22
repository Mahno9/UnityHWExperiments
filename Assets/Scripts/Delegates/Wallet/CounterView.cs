using System;

using Navigation.Utils;

using TMPro;

using UnityEngine;

namespace Delegates.Wallet
{
    public class CounterView : MonoBehaviour, IDisposable
    {
        private const int DefaultOldValue = 0;

        [SerializeField] private TextMeshProUGUI _view;

        private IReactiveVariableReadonly<int> _reactiveVariable;

        public void Initialize(IReactiveVariableReadonly<int> reactiveVariable) => Subscribe(reactiveVariable);

        public void Dispose() => Unsubscribe();

        private void Subscribe(IReactiveVariableReadonly<int> reactiveVariable)
        {
            if (_reactiveVariable is not null)
                Unsubscribe();

            _reactiveVariable = reactiveVariable;
            _reactiveVariable.Changed += Updater;
            Updater(DefaultOldValue, _reactiveVariable.Value);
        }

        private void Unsubscribe()
        {
            _reactiveVariable.Changed += Updater;
        }

        private void Updater(int _, int newValue)
        {
            _view.SetText($"{newValue}");
        }
    }
}