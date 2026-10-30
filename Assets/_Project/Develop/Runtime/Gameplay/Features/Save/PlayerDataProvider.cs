using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Utilities.Save;

namespace _Project.Develop.Runtime.Gameplay.Features.Save
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
                Gold = _walletConfig.StartGold
            };
        }
    }
}