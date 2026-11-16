using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class TransformRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private Transform _transform;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _transform = entity.Transform;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;

            if (direction.sqrMagnitude > 0.01f)
            {
                _transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}