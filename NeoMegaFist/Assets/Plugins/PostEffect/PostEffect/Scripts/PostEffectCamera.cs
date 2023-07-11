using Cysharp.Threading.Tasks;
using DG.Tweening;
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
		[SerializeField] private Transform shakeParent;

		private void Awake()
        {
			renderTexture.Release();
			renderTexture.width = Screen.width;
            renderTexture.height = Screen.height;
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
			ShakeCoroutine(power, time, interval, decay);
		}

		private async void ShakeCoroutine(Vector2 power, float time, float interval, bool decay)
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
					shakeParent.position += (Vector3)offset;
					shakeParent.DOMove(offset, interval).onComplete += () =>
					{
						shakeParent.position -= (Vector3)offset;
						offset = Vector2.zero;
					};
				}
				await UniTask.DelayFrame(1);
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

		Vector2 IPostEffectCamera.ScreenToWorldPoint(Vector3 worldPoint)
		{
			return postEffectCamera.ScreenToWorldPoint(worldPoint);
		}

		Vector2 IPostEffectCamera.WorldToScreenPoint(Vector3 screenPoint)
		{
			return postEffectCamera.WorldToScreenPoint(screenPoint);
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

        public Ray ScreenPointToRay(Vector3 mousePosition)
        {
			return postEffectCamera.ScreenPointToRay(mousePosition);
        }
    }
}
