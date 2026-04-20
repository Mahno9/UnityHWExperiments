using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Delegates.Wallet
{
    public class WalletView : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject             _walletRootWidget;
        [SerializeField] private List<CurrencyView>     _currencyViews;

        public void Initialize(WalletService service)
        {
            foreach (CurrencyView currencyView in _currencyViews)
                currencyView.Initialize(service);
        }

        public void Dispose()
        {
            foreach (CurrencyView currencyView in _currencyViews)
                currencyView.Dispose();
        }

        public void SetViewActive(bool isActive)
        {
            _walletRootWidget.SetActive(isActive);
        }
    }
}