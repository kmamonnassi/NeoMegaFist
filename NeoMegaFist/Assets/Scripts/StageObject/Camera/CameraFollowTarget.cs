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

        [Inject] private IPostEffectCamera cam;

        private BoxCollider2D cameraConfiner;
        private Vector2 pos;

        private void FixedUpdate()
        {
            Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
            pos = Vector2.Lerp(cam.GetPosition(), targetPos, Time.deltaTime * moveSpeed);
			if (cameraConfiner == null) return;

			Vector2 cameraRightTop = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - cam.GetPosition();
			Vector2 cameraLeftBottom = cam.ScreenToWorldPoint(Vector3.zero) - cam.GetPosition();
			Vector2 min = cameraConfiner.offset + (Vector2)cameraConfiner.transform.position - (cameraConfiner.size / 2) - cameraLeftBottom;
			Vector2 max = cameraConfiner.offset + (Vector2)cameraConfiner.transform.position + (cameraConfiner.size / 2) - cameraRightTop;
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
			Vector2 truePos = Vector2.Lerp(pos, new Vector2(x, y), Time.deltaTime * 4);
			cam.SetPosition(truePos);
		}

		private void LateUpdate()
		{
		}

		public void SetConfiner(BoxCollider2D confiner)
		{
			cameraConfiner = confiner;
		}
	}
}