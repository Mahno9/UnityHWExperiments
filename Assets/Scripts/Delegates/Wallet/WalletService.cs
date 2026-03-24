using System;
using System.Collections.Generic;

using Navigation.Utils;

using UnityEngine;

namespace Delegates.Wallet
{
    public struct CurrencyTypeValue
    {
        public CurrencyType Type;
        public int          Value;
    };

    public class WalletService
    {
        public event Action<CurrencyType, int, int> OnCurrencyChanged;

        private readonly Dictionary<CurrencyType, ReactiveVariable<int>> _wallet = new();

        public WalletService()
        {
            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
            {
                ReactiveVariable<int> newReactive = new ();
                _wallet.Add(currencyType, newReactive);

                // Dispose in destructor
                newReactive.Changed += (oldValue, newValue) =>
                    OnCurrencyChanged?.Invoke(currencyType, oldValue, newValue);
            }
        }

        public void Earn(CurrencyType type, int amount)
        {
            if (amount < 0)
            {
                Debug.Log($"Invalid amount for {type}: {amount}");
                return;
            }

            _wallet[type].Value += amount;
        }

        public void Spend(CurrencyType type, int amount)
        {
            if (amount < 0)
            {
                Debug.Log($"Invalid amount for {type}: {amount}");
                return;
            }

            _wallet[type].Value = Mathf.Max(_wallet[type].Value - amount, 0);
        }

        public int GetAmount(CurrencyType type) => _wallet.GetValueOrDefault(type).Value;
    }
}