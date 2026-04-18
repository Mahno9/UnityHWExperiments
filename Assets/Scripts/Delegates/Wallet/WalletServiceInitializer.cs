using System;

using UnityEngine;

namespace Delegates.Wallet
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
            _view.UnsubscribeFromService();

            _testTriggerInputs.OnCurrencyEarn -= _service.Earn;
            _testTriggerInputs.OnCurrencySpend -= _service.Spend;
        }

        private void BindServiceUpdatesToView() => _view.SubscribeToService(_service);

        private void BindTestTriggersToService()
        {
            _testTriggerInputs.OnCurrencyEarn += _service.Earn;
            _testTriggerInputs.OnCurrencySpend += _service.Spend;
        }

        private void PrepareView() => _view.SetViewActive(true);
    }
}