using UnityEngine;

namespace Effect
{
	public class EffectView : MonoBehaviour
	{
		[SerializeField] private float duration;

		private float nowTime;

		private void Update()
		{
			nowTime += Time.unscaledDeltaTime;
			if(nowTime >= duration)
			{
				Destroy(gameObject);
			}
		}
	}
}