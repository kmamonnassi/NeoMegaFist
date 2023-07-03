using StageObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace Stage
{
    public class CameraFollowTarget : MonoBehaviour, ICameraFollowTarget
	{
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed = 10;

		[Inject] private Player player;
        [Inject] private IPostEffectCamera cam;

        private BoxCollider2D cameraConfiner;

        private void Start()
        {
            if(target == null)
            {
				target = player.transform;
            }
        }

        private void FixedUpdate()
        {
			Vector2 pos = new Vector2(target.transform.position.x, target.transform.position.y);

			Vector2 cameraRightTop = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - cam.GetPosition();
			Vector2 cameraLeftBottom = cam.ScreenToWorldPoint(Vector3.zero) - cam.GetPosition();

			Vector2 min = new Vector2(float.MinValue, float.MinValue);
			Vector2 max = new Vector2(float.MaxValue, float.MaxValue);

			if (cameraConfiner != null)
			{
				min = cameraConfiner.offset + (Vector2)cameraConfiner.transform.position - (cameraConfiner.size / 2) - cameraLeftBottom;
				max = cameraConfiner.offset + (Vector2)cameraConfiner.transform.position + (cameraConfiner.size / 2) - cameraRightTop;
			}

			float x = pos.x;
			if (max.x > min.x)
			{
				x = Mathf.Clamp(pos.x, min.x, max.x);
			}
			float y = pos.y;
			if (max.y > min.y)
			{
				y = Mathf.Clamp(pos.y, min.y, max.y);
			}
			Vector2 truePos = Vector2.Lerp(cam.GetPosition(), new Vector2(x, y), Time.deltaTime * moveSpeed);
			cam.SetPosition(truePos);
		} 

		public void SetConfiner(BoxCollider2D confiner)
		{
			cameraConfiner = confiner;
		}

		public void SetTarget(Transform target)
		{
            if (target == null)
            {
                SetTargetPlayer();
            }
            else
			{
				this.target = target;
			}
		}

		public void SetTargetPlayer()
        {
			this.target = player.transform;
		}
	}
}