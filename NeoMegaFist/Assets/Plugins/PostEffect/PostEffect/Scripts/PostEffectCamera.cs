using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.PostEffect
{
	[DefaultExecutionOrder(1)]
    public class PostEffectCamera : MonoBehaviour, IPostEffectCamera
    {
		[SerializeField] private Camera postEffectCamera = null;
        [SerializeField] private RenderTexture renderTexture = null;

		private BoxCollider2D cameraConfiner;

        private void Awake()
        {
			renderTexture.Release();
			renderTexture.width = Screen.width;
            renderTexture.height = Screen.height;
		}

        private void LateUpdate()
        {
			if (cameraConfiner == null) return;

			Vector2 cameraRightTop = postEffectCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - postEffectCamera.transform.position;
			Vector2 cameraLeftBottom = postEffectCamera.ScreenToWorldPoint(Vector3.zero) - postEffectCamera.transform.position;
			Vector2 min = cameraConfiner.offset + (Vector2)cameraConfiner.transform.position - (cameraConfiner.size / 2) - cameraLeftBottom;
			Vector2 max = cameraConfiner.offset + (Vector2)cameraConfiner.transform.position + (cameraConfiner.size / 2) - cameraRightTop;
			float x = transform.position.x;
			if(max.x > min.x)
            {
				x = Mathf.Clamp(transform.position.x, min.x, max.x);
			}
			float y = transform.position.y;
			if (max.y > min.y)
			{
				y = Mathf.Clamp(transform.position.y, min.y, max.y);
			}
			Debug.Log(min + "/" + max + "/" + new Vector2(x, y));
			transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), Time.deltaTime * 4);
        }

		public void SetConfiner(BoxCollider2D confiner)
        {
			cameraConfiner = confiner;
		}

        public void SetColor(Color color)
		{
			postEffectCamera.backgroundColor = color;
		}

		Vector2 IPostEffectCamera.GetPosition()
		{
			return transform.position;
		}

		float IPostEffectCamera.GetRotation()
		{
			return transform.eulerAngles.z;
		}

		void IPostEffectCamera.SetPosition(Vector2 pos)
		{
			transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		}

		void IPostEffectCamera.Rotate(float z)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
		}

		void IPostEffectCamera.SetOrthograhicSize(float size)
		{
			postEffectCamera.orthographicSize = size;
		}

		float IPostEffectCamera.GetOrthograhicSize()
		{
			return postEffectCamera.orthographicSize;
		}

		public void Shake(Vector2 power, float time, float interval = 0.01f, bool decay = true)
		{
			StartCoroutine(ShakeCoroutine(power, time, interval, decay));
		}

		private IEnumerator ShakeCoroutine(Vector2 power, float time, float interval, bool decay)
		{
			float nowTime = 0;
			float beforeTime = 0;
			Vector2 offset = Vector2.zero;
			while(nowTime < time)
			{
				nowTime += Time.unscaledDeltaTime;
				if (nowTime - beforeTime > interval)
				{
					beforeTime = nowTime;
					Vector2 truePower = power;
					if (decay) truePower *= nowTime / time;
					offset.x = Random.Range(-truePower.x, truePower.x);
					offset.y = Random.Range(-truePower.y, truePower.y);
					transform.position += (Vector3)offset;
				}
				yield return null;
				transform.position -= (Vector3)offset;
				offset = Vector2.zero;
			}
		}

		Vector2 IPostEffectCamera.ScreenToWorldPoint(Vector3 worldPoint, Camera.MonoOrStereoscopicEye eye)
        {
			return postEffectCamera.ScreenToWorldPoint(worldPoint, eye);
		}

		Vector2 IPostEffectCamera.WorldToScreenPoint(Vector3 screenPoint, Camera.MonoOrStereoscopicEye eye)
		{
			return postEffectCamera.WorldToScreenPoint(screenPoint, eye);
		}

		Vector2 IPostEffectCamera.ViewportToScreenPoint(Vector3 viewportPoint)
		{
			return postEffectCamera.ViewportToScreenPoint(viewportPoint);
		}

		Vector2 IPostEffectCamera.ScreenToViewportPoint(Vector3 screenPoint)
		{
			return postEffectCamera.ScreenToViewportPoint(screenPoint);
		}

		Vector2 IPostEffectCamera.ViewportToWorldPoint(Vector3 viewportPoint)
		{
			return postEffectCamera.ViewportToWorldPoint(viewportPoint);
		}

		Vector2 IPostEffectCamera.WorldToViewportPoint(Vector3 worldPoint)
		{
			return postEffectCamera.WorldToViewportPoint(worldPoint);
		}
    }
}
