using UnityEngine;
using Zenject;

namespace Effect
{
	public class EffectPlayerInstaller : MonoInstaller
	{
		[SerializeField] private EffectDB effectDB;
		[SerializeField] private EffectPlayer effectPlayer;

		public override void InstallBindings()
		{
			Container.BindInstance<IEffectDB>(effectDB);
			Container.BindInstance<IEffectPlayer>(effectPlayer);
		}
	}
}