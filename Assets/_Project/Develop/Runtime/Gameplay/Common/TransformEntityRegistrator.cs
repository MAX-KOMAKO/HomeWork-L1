using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Common
{
    public class TransformEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            entity.AddTransform(GetComponent<Transform>());
        }
    }
}