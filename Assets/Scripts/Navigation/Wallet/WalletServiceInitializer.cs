using System;

using UnityEngine;

namespace Navigation.Wallet
{
    public class WalletServiceInitializer : MonoBehaviour
    {
        [SerializeField] private WalletView       _view;
        [SerializeField] private WalletTestInputs _testTriggerInputs;
        private                  WalletService    _service;

        private void Awake()
        {
            _service = new WalletService();

            BindServiceUpdatesToView();
            PrepareView();
            BindTestTriggersToService();
        }

        private void OnDestroy()
        {
            _service.OnCurrencyChanged -= _view.OnCurrencyAmountChanged;
        }

        private void BindServiceUpdatesToView()
        {
            _service.OnCurrencyChanged += _view.OnCurrencyAmountChanged;
        }

        private void BindTestTriggersToService()
        {
            _testTriggerInputs.OnCurrencyEarn += _service.Earn;
            _testTriggerInputs.OnCurrencySpend += _service.Spend;
        }

        private void PrepareView()
        {
            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
                _view.OnCurrencyAmountChanged(currencyType, _service.GetAmount(currencyType));
            _view.SetViewActive(true);
        }
    }
}