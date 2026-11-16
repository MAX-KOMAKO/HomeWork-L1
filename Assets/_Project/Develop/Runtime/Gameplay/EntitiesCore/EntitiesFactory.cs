using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private const string RigidbodyPrefabPath = "Entities/TestEntityRigidbody";
        private const string CharacterControllerPrefabPath = "Entities/TestEntityCC";
        private const float DefaultMoveSpeed = 5f;

        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateTestEntity(Vector3 position)
        {
            Entity entity = new Entity();
            _monoEntitiesFactory.Create(entity, position, RigidbodyPrefabPath);

            entity
                .AddMoveDirection(new ReactiveVariable<Vector3>(Vector3.zero))
                .AddMoveSpeed(new ReactiveVariable<float>(DefaultMoveSpeed))
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new TransformRotationSystem());

            _entitiesLifeContext.Add(entity);
            return entity;
        }

        public Entity CreateCharacterControllerEntity(Vector3 position)
        {
            Entity entity = new Entity();
            _monoEntitiesFactory.Create(entity, position, CharacterControllerPrefabPath);

            entity
                .AddMoveDirection(new ReactiveVariable<Vector3>(Vector3.zero))
                .AddMoveSpeed(new ReactiveVariable<float>(DefaultMoveSpeed))
                .AddSystem(new CharacterControllerMovementSystem())
                .AddSystem(new TransformRotationSystem());

            _entitiesLifeContext.Add(entity);
            return entity;
        }
    }
}