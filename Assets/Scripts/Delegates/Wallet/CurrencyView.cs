using System;

using Navigation.Utils;

using TMPro;

using UnityEngine;

namespace Delegates.Wallet
{
    public class CurrencyView : MonoBehaviour, IDisposable
    {
        private const int DefaultOldValue = 0;

        [SerializeField] private CurrencyType    _type;
        [SerializeField] private TextMeshProUGUI _view;

        private WalletService _service;

        public void Initialize(WalletService service) => Subscribe(service);

        public void Dispose() => Unsubscribe();

        private void Subscribe(WalletService service)
        {
            if (_service is not null)
                Unsubscribe();

            _service = service;
            IReactiveVariableReadonly<int> currencyVar = _service.GetCurrencyReactiveVar(_type);
            currencyVar.Changed += Updater;
            Updater(DefaultOldValue, currencyVar.Value);
        }

        private void Unsubscribe()
        {
            _service.GetCurrencyReactiveVar(_type).Changed += Updater;
        }

        private void Updater(int _, int newValue)
        {
            _view.SetText($"{newValue}");
        }
    }
}