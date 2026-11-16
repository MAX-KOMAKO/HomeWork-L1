using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private static readonly Vector3 RigidbodySpawnPosition = new Vector3(-2, 1, 0);
        private static readonly Vector3 CharacterControllerSpawnPosition = new Vector3(2, 1, 0);

        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private EntitiesLifeContext _entitiesLifeContext;

        private Entity _entityRigidbody;
        private Entity _entityCharacterController;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }

        public void Run()
        {
            _entityRigidbody = _entitiesFactory.CreateTestEntity(RigidbodySpawnPosition);
            _entityCharacterController = _entitiesFactory.CreateCharacterControllerEntity(CharacterControllerSpawnPosition);

            if (_entityRigidbody == null || _entityCharacterController == null)
            {
                Debug.LogError("Не удалось создать тестовые сущности!");
                return;
            }

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            _entitiesLifeContext.Update(Time.deltaTime);

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _entityRigidbody.MoveDirection.Value = input;
            _entityCharacterController.MoveDirection.Value = input;
        }
    }
}