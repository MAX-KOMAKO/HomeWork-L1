using System;
using System.Collections.Generic;
using _Project.Develop.Utilities.DataManagment;
using _Project.Develop.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Wallet
{
    public class WalletService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies;
        private readonly WalletConfig _config;

        public WalletService(WalletConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _currencies = new Dictionary<CurrencyTypes, ReactiveVariable<int>>
            {
                { CurrencyTypes.Coins, new ReactiveVariable<int>(config.StartGold) }
            };
        }

        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes type) => _currencies[type];

        public bool Enough(CurrencyTypes type, int amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
            return _currencies[type].Value >= amount;
        }

        public void Add(CurrencyTypes type, int amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
            _currencies[type].Value += amount;
        }

        public void Spend(CurrencyTypes type, int amount)
        {
            if (Enough(type, amount) == false) 
                throw new InvalidOperationException($"Not enough {type}");
            
            _currencies[type].Value -= amount;
        }

        public void RewardForWin() => Add(CurrencyTypes.Coins, _config.RewardForWin);

        public void PenalizeForLoss()
        {
            if (Enough(CurrencyTypes.Coins, _config.PenaltyForLoss) == false)
            {
                Spend(CurrencyTypes.Coins, _currencies[CurrencyTypes.Coins].Value);
                return;
            }
            Spend(CurrencyTypes.Coins, _config.PenaltyForLoss);
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (var currency in data.WalletData)
            {
                if (_currencies.ContainsKey(currency.Key))
                    _currencies[currency.Key].Value = currency.Value;
                else
                    _currencies.Add(currency.Key, new ReactiveVariable<int>(currency.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            data.WalletData.Clear();
            foreach (var currency in _currencies)
            {
                data.WalletData[currency.Key] = currency.Value.Value;
            }
        }
    }
}