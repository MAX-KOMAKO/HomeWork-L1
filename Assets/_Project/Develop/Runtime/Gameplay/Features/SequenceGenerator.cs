using System.Text;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class SequenceGenerator
    {
        public string Generate(GameModeConfig config)
        {
            if (config.SequenceLength <= 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(config.SequenceLength), "Длина последовательности должна быть больше нуля.");
            }

            if (string.IsNullOrEmpty(config.Characters))
            {
                throw new System.ArgumentException("Набор символов не может быть пустым.", nameof(config.Characters));
            }

            StringBuilder sequenceBuilder = new StringBuilder();

            for (int i = 0; i < config.SequenceLength; i++)
            {
                int randomIndex = Random.Range(0, config.Characters.Length);
                sequenceBuilder.Append(config.Characters[randomIndex]);
            }

            return sequenceBuilder.ToString();
        }
    }
}