namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection MoveDirectionC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection>();

		public _Project.Develop.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> MoveDirection => MoveDirectionC.Value;

		public Entity AddMoveDirection()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection() { Value = new _Project.Develop.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public Entity AddMoveDirection(_Project.Develop.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed MoveSpeedC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed>();

		public _Project.Develop.Utilities.Reactive.ReactiveVariable<System.Single> MoveSpeed => MoveSpeedC.Value;

		public Entity AddMoveSpeed()
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed() { Value = new _Project.Develop.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public Entity AddMoveSpeed(_Project.Develop.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RigidbodyComponent RigidbodyC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.RigidbodyComponent>();

		public UnityEngine.Rigidbody Rigidbody => RigidbodyC.Value;

		public Entity AddRigidbody(UnityEngine.Rigidbody value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.RigidbodyComponent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.TransformComponent TransformC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.TransformComponent>();

		public UnityEngine.Transform Transform => TransformC.Value;

		public Entity AddTransform(UnityEngine.Transform value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.TransformComponent() {Value = value}); 
		}

		public _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CharacterControllerComponent CharacterControllerC => GetComponent<_Project.Develop.Runtime.Gameplay.Features.MovementFeature.CharacterControllerComponent>();

		public UnityEngine.CharacterController CharacterController => CharacterControllerC.Value;

		public Entity AddCharacterController(UnityEngine.CharacterController value)
		{
			return AddComponent(new _Project.Develop.Runtime.Gameplay.Features.MovementFeature.CharacterControllerComponent() {Value = value}); 
		}
	}
}