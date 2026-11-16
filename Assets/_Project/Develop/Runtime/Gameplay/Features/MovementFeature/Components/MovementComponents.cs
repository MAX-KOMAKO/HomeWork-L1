using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class MoveDirection : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class MoveSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class RigidbodyComponent : IEntityComponent
    {
        public Rigidbody Value;
    }

    public class TransformComponent : IEntityComponent
    {
        public Transform Value;
    }

    public class CharacterControllerComponent : IEntityComponent
    {
        public CharacterController Value;
    }
}