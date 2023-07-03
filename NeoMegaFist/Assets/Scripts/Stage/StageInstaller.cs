using StageObject;
using UnityEngine;
using Zenject;

namespace Stage
{
	public class StageInstaller : MonoInstaller
	{
		[SerializeField] private CameraFollowTarget followTarget;
		[SerializeField] private CharacterInitalizer characterInitalier;

		public override void InstallBindings()
		{
			Container.BindInstance<ICameraFollowTarget>(followTarget);
			Container.BindInstance<ICharacterInitalizer>(characterInitalier);
		}
	}
}