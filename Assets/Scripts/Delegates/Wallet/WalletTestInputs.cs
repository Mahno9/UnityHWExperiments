using System;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Delegates.Wallet
{
    public class WalletTestInputs : MonoBehaviour
    {
        public event Action<CurrencyType, int> OnCurrencyEarn;
        public event Action<CurrencyType, int> OnCurrencySpend;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                OnCurrencyEarn?.Invoke(CurrencyType.Money, 1);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                OnCurrencyEarn?.Invoke(CurrencyType.Honey, 1);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                OnCurrencyEarn?.Invoke(CurrencyType.Bones, 1);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                int spendAmount = Random.Range(1, 4);

                CurrencyType currencyType =
                    (CurrencyType)Enum.GetValues(typeof(CurrencyType))
                        .GetValue(Random.Range(0, Enum.GetValues(typeof(CurrencyType)).Length));

                OnCurrencySpend?.Invoke(currencyType, spendAmount);
            }
        }
    }
}