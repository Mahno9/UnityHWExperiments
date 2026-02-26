using System;

using UnityEngine;

namespace Navigation.Wallet
{
    public class WalletServiceInitializer : MonoBehaviour
    {
        [SerializeField] private WalletView    _view;
        private                  WalletService _service;

        private void Awake()
        {
            _service = new WalletService();

            _service.OnCurrencyChanged += _view.OnCurrencyAmountChanged;

            // Refresh on init
            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
                _view.OnCurrencyAmountChanged(currencyType, _service.GetAmount(currencyType));

            // Enable view after refresh
            _view.SetViewActive(true);
        }

        private void OnDestroy()
        {
            _service.OnCurrencyChanged -= _view.OnCurrencyAmountChanged;
        }
    }
}