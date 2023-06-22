using DG.Tweening;
using System;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace StageObject
{
    public class ThrownCollider : EffectCollider, ITimeScalable
    {
        [Inject] private IPostEffectCamera postEffectCamera;
        [Inject] private ITimeScaler timeScaler;

        public int Priority => 2;
        public float Scale => 0f;

        private const float CAMERA_SHAKE_POWER = 0.01f;

#if UNITY_EDITOR
        [SerializeField] private Collider2D attackCollider;
        [SerializeField] private Collider2D mainCollider;

        private void Awake()
        {
            if (attackCollider == null)
                Debug.LogError("敵に衝突するコライダーが存在しません。");
            else
            if (!attackCollider.isTrigger)
                Debug.LogError("敵に衝突するコライダーのIsTriggerをTrueにしてください。貫通しなくなります。");

            if (mainCollider == null)
                Debug.LogError("壁に反射するコライダーが存在しません。");
            else
            if (mainCollider.isTrigger)
                Debug.LogError("壁に反射するコライダーのIsTriggerを外してください。反射しなくなります。");
        }
#endif

        public void Initalize(Rigidbody2D rb)
        {
            OnHitTarget += obj =>
            {
                Vector2 diff = transform.position - obj.transform.position;
                Vector2 dir = diff.normalized;
                obj.GetComponent<StageObjectCatchAndThrow>().Thrown(dir, rb.velocity.magnitude / 2);
                postEffectCamera.Shake(new Vector2(CAMERA_SHAKE_POWER, CAMERA_SHAKE_POWER) * rb.velocity.magnitude, 0.1f, 0.1f);
                timeScaler.Add(this);
                DOVirtual.DelayedCall(0.1f, () => 
                {
                    timeScaler.Remove(this);
                    Time.timeScale = 1;
                });
            };

            OnHitWall += () =>
            {
                postEffectCamera.Shake(new Vector2(CAMERA_SHAKE_POWER, CAMERA_SHAKE_POWER) * rb.velocity.magnitude, 0.1f, 0.01f, true);
                timeScaler.Add(this);
                DOVirtual.DelayedCall(0.1f, () =>
                {
                    timeScaler.Remove(this);
                    Time.timeScale = 1; 
                });
            };
        }
    }
}
