using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class CharacterControllerMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private CharacterController _characterController;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _characterController = entity.CharacterController;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 direction = _moveDirection.Value.normalized;
            _characterController.Move(direction * _moveSpeed.Value * deltaTime);
        }
    }
}