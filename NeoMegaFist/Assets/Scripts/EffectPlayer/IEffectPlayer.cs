using UnityEngine;

namespace Effect
{
	public interface IEffectPlayer
	{
		EffectView PlayEffect(string name);
		EffectView PlayEffect(string name, Vector3 position);
		EffectView PlayEffect(string name, Vector3 position, Quaternion rotation);
		EffectView PlayEffect(string name, Vector3 position, Quaternion rotation, Transform parent);
	}
}

