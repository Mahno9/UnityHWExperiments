using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Serialization;

namespace Delegates.Wallet
{
    [Serializable]
    internal struct CurrencyViewWithType
    {
        [FormerlySerializedAs("CurrencyView")] public CounterView  CounterView;
        public  CurrencyType CurrencyType;
    }

    public class WalletView : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject             _walletRootWidget;
        [SerializeField] private List<CurrencyViewWithType>     _currencyViews;

        public void Initialize(WalletService service)
        {
            foreach (CurrencyViewWithType view in _currencyViews)
                view.CounterView.Initialize(service.GetCurrencyReactiveVar(view.CurrencyType));
        }

        public void Dispose()
        {
            foreach (CurrencyViewWithType view in _currencyViews)
                view.CounterView.Dispose();
        }

        public void SetViewActive(bool isActive)
        {
            _walletRootWidget.SetActive(isActive);
        }
    }
}