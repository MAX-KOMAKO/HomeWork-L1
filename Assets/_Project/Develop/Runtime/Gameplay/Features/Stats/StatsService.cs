using System;
using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Utilities.Save;

namespace _Project.Develop.Runtime.Gameplay.Features.Stats
{
    public class StatsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        public event Action<int, int> StatsChanged;

        private int _wins;
        private int _losses;

        public int Wins => _wins;
        public int Losses => _losses;

        public void AddWin()
        {
            _wins++;
            StatsChanged?.Invoke(_wins, _losses);
        }

        public void AddLoss()
        {
            _losses++;
            StatsChanged?.Invoke(_wins, _losses);
        }

        public void ResetStats()
        {
            _wins = 0;
            _losses = 0;
            StatsChanged?.Invoke(_wins, _losses);
        }

        public void ReadFrom(PlayerData data)
        {
            _wins = data.Wins;
            _losses = data.Losses;
            StatsChanged?.Invoke(_wins, _losses);
        }

        public void WriteTo(PlayerData data)
        {
            data.Wins = _wins;
            data.Losses = _losses;
        }
    }
}