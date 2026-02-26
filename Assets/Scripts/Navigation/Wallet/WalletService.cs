using System;
using System.Collections.Generic;

using Navigation.Controllers;

using UnityEngine;

namespace Navigation.Wallet
{
    public struct CurrencyTypeValue
    {
        public CurrencyType Type;
        public int          Value;
    };

    public class WalletService
    {
        public event Action<CurrencyType, int> OnCurrencyChanged;

        private readonly Dictionary<CurrencyType, int> _wallet = new();

        public void Earn(CurrencyType type, int amount)
        {
            if (amount < 0)
            {
                Debug.Log($"Invalid amount for {type}: {amount}");
                return;
            }

            _wallet[type] += amount;

            OnCurrencyChanged?.Invoke(type, _wallet[type]);
        }

        public void Spend(CurrencyType type, int amount)
        {
            if (amount < 0)
            {
                Debug.Log($"Invalid amount for {type}: {amount}");
                return;
            }

            _wallet[type] = Mathf.Max(_wallet[type] - amount, 0);

            OnCurrencyChanged?.Invoke(type, _wallet[type]);
        }

        public int GetAmount(CurrencyType type) => _wallet.GetValueOrDefault(type);
    }
}