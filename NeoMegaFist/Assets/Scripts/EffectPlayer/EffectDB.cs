using UnityEngine;

namespace Effect
{
	[CreateAssetMenu(fileName = "EffectDB", menuName = "Effect/EffectDB")]
	public class EffectDB : ScriptableObject, IEffectDB
	{
		[SerializeField] private EffectView[] effectPrefabs;

		public EffectView GetPrefab(string name)
		{
			foreach(EffectView prefab in effectPrefabs)
			{
				if(prefab.name.Equals(name))
				{
					return prefab;
				}
			}
			return null;
		}
	}
}