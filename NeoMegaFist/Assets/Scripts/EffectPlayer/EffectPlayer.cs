using UnityEngine;
using Zenject;

namespace Effect
{
    public class EffectPlayer : MonoBehaviour, IEffectPlayer
    {
        [Inject] private IEffectDB effectDB;

		public EffectView PlayEffect(string name)
		{
			return PlayEffect(name, Vector3.zero, Quaternion.identity, null);
		}

		public EffectView PlayEffect(string name, Vector3 position)
		{
			return PlayEffect(name, position, Quaternion.identity, null);
		}

		public EffectView PlayEffect(string name, Vector3 position, Quaternion rotation)
		{
			return PlayEffect(name, position, rotation, null);
		}

		public EffectView PlayEffect(string name, Vector3 position, Quaternion rotation, Transform parent)
		{
			EffectView prefab = effectDB.GetPrefab(name);
			return Instantiate(prefab, position, rotation, parent);
		}
	}
}