using UnityEngine;

namespace Stage
{
	public interface ICameraFollowTarget 
	{
		void SetConfiner(BoxCollider2D confiner);
		void SetTarget(Transform target);
		void SetTargetPlayer();
	}
}