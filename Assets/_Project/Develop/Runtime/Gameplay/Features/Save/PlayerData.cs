using System;
using _Project.Develop.Utilities.Save;

namespace _Project.Develop.Runtime.Gameplay.Features.Save
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public int Wins;
        public int Losses;
        public int Gold;
    }
}