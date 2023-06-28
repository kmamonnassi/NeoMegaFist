using UnityEngine;

namespace Stage
{
	public interface ICameraFollowTarget 
	{
		void SetConfiner(BoxCollider2D confiner);
	}
}