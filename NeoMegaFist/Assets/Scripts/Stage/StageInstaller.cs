using UnityEngine;
using Zenject;

namespace Stage
{
	public class StageInstaller : MonoInstaller
	{
		[SerializeField] private CameraFollowTarget followTarget;

		public override void InstallBindings()
		{
			Container.BindInstance<ICameraFollowTarget>(followTarget);
		}
	}
}