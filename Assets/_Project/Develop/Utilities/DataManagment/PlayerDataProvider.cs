using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.Wallet;

namespace _Project.Develop.Utilities.DataManagment
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly WalletConfig _walletConfig;

        public PlayerDataProvider(SaveLoadService saveLoadService, WalletConfig walletConfig) : base(saveLoadService)
        {
            _walletConfig = walletConfig;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData
            {
                Wins = 0,
                Losses = 0,
                WalletData = new Dictionary<CurrencyTypes, int>
                {
                    { CurrencyTypes.Coins, _walletConfig.StartGold }
                }
            };
        }
    }
}