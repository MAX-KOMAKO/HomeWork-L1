using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.Wallet;

namespace _Project.Develop.Utilities.DataManagment
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public int Wins;
        public int Losses;
        public Dictionary<CurrencyTypes, int> WalletData = new();
    }
}