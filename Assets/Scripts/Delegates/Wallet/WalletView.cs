using System;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;

namespace Delegates.Wallet
{
    [Serializable]
    public struct CurrencyViewPair
    {
        public CurrencyType    Type;
        public TextMeshProUGUI View;
    }

    public class WalletView : MonoBehaviour
    {
        [SerializeField] private List<CurrencyViewPair> _currencyDisplays;
        [SerializeField] private GameObject             _walletRootWidget;

        public void OnCurrencyAmountChanged(CurrencyType type, int newAmount)
        {
            foreach (CurrencyViewPair currencyViewPair in _currencyDisplays.Where(currencyViewPair => currencyViewPair.Type == type))
                currencyViewPair.View.SetText($"{newAmount}");
        }

        public void SetViewActive(bool isActive)
        {
            _walletRootWidget.SetActive(isActive);
        }
    }
}