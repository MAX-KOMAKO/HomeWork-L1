using System;
using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Utilities.Save;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Wallet
{
    public class WalletService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        public event Action<int> GoldChanged;

        private readonly WalletConfig _config;
        private int _currentGold;

        public int CurrentGold => _currentGold;

        public WalletService(WalletConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _currentGold = config.StartGold;
        }

        public bool CanSpend(int amount)
        {
            return _currentGold >= amount;
        }

        public void AddGold(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма не может быть отрицательной.");
            }

            _currentGold += amount;
            GoldChanged?.Invoke(_currentGold);
        }

        public void SpendGold(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма не может быть отрицательной.");
            }

            if (CanSpend(amount) == false)
            {
                Debug.LogError($"Попытка списать {amount} золота, но на балансе только {_currentGold}.");
                return;
            }

            _currentGold -= amount;
            GoldChanged?.Invoke(_currentGold);
        }

        public void RewardForWin() => AddGold(_config.RewardForWin);

        public void PenalizeForLoss()
        {
            if (CanSpend(_config.PenaltyForLoss) == false)
            {
                Debug.LogWarning($"Недостаточно золота для штрафа. Штраф: {_config.PenaltyForLoss}, Баланс: {_currentGold}. Баланс обнулён.");
                _currentGold = 0;
                GoldChanged?.Invoke(_currentGold);
                return;
            }

            SpendGold(_config.PenaltyForLoss);
        }

        public void ResetToStart()
        {
            _currentGold = _config.StartGold;
            GoldChanged?.Invoke(_currentGold);
        }

        public void ReadFrom(PlayerData data)
        {
            _currentGold = data.Gold;
            GoldChanged?.Invoke(_currentGold);
        }

        public void WriteTo(PlayerData data)
        {
            data.Gold = _currentGold;
        }
    }
}