using System;
using System.Collections.Generic;
using System.Linq;

using Navigation.Utils;

using TMPro;

using UnityEngine;

namespace Delegates.Wallet
{
    [Serializable]
    public class CurrencyViewData
    {
        public CurrencyType     Type;
        public TextMeshProUGUI  View;
        public Action<int, int> Updater;
    }

    public class WalletView : MonoBehaviour
    {
        [SerializeField] private List<CurrencyViewData> _currencyDisplays;
        [SerializeField] private GameObject             _walletRootWidget;

        private WalletService _service;

        private const int DefaultOldValue = 0;

        public void SubscribeToService(WalletService service)
        {
            if (_service is not null)
                UnsubscribeFromService();

            _service = service;
            foreach (CurrencyViewData currencyViewData in _currencyDisplays)
            {
                currencyViewData.Updater ??= (_, newValue) => { currencyViewData.View.SetText($"{newValue}"); };

                IReactiveVariableReadonly<int> currencyVar = _service.GetCurrencyReactiveVar(currencyViewData.Type);
                currencyVar.Changed += currencyViewData.Updater;

                currencyViewData.Updater(DefaultOldValue, currencyVar.Value);
            }
        }

        public void UnsubscribeFromService()
        {
            foreach (CurrencyViewData currencyViewData in _currencyDisplays)
                _service.GetCurrencyReactiveVar(currencyViewData.Type).Changed -= currencyViewData.Updater;
            
            _service = null;
        }

        public void SetViewActive(bool isActive)
        {
            _walletRootWidget.SetActive(isActive);
        }
    }
}