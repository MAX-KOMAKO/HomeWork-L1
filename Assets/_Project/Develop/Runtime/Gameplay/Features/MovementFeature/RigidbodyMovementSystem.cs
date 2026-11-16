using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _rigidbody = entity.Rigidbody;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 velocity = _moveDirection.Value.normalized * _moveSpeed.Value;
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        }
    }
}