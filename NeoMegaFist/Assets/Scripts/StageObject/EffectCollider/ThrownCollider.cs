using DG.Tweening;
using System;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace StageObject
{
    public class ThrownCollider : EffectCollider, ITimeScalable
    {
        [SerializeField] private Collider2D attackCollider;
        [SerializeField] private Collider2D mainCollider;
        [Inject] private IPostEffectCamera postEffectCamera;
        [Inject] private ITimeScaler timeScaler;

        public int Priority => 2;
        public float Scale => 0f;
        public ThrownState State { get; private set; }
        public Collider2D AttackCollider => attackCollider;
        public Collider2D MainCollider => mainCollider;

        private const float THROWN_CAMERA_SHAKE_POWER = 0.01f;
        private const float OVERHAND_THROWN_CAMERA_SHAKE_POWER = 5;

        public void SetState(ThrownState state)
        {
            State = state;
        }

        public void Initalize(Rigidbody2D rb)
        {
            OnHitTarget += obj =>
            {
                if (State == ThrownState.Throw)
                {
                    Vector2 diff = transform.position - obj.transform.position;
                    Vector2 dir = diff.normalized;
                    obj.GetComponent<StageObjectCatchAndThrow>().Thrown(dir, rb.velocity.magnitude / 2);
                    postEffectCamera.Shake(new Vector2(THROWN_CAMERA_SHAKE_POWER, THROWN_CAMERA_SHAKE_POWER) * rb.velocity.magnitude, 0.1f, 0.1f);
                }
                else
                if(State == ThrownState.OverhandThrow)
                {
                    postEffectCamera.Shake(new Vector2(OVERHAND_THROWN_CAMERA_SHAKE_POWER, OVERHAND_THROWN_CAMERA_SHAKE_POWER), 0.1f, 0.01f, true);

                }
                timeScaler.Add(this);
                DOVirtual.DelayedCall(0.1f, () => 
                {
                    timeScaler.Remove(this);
                    Time.timeScale = 1;
                });
            };

            OnHitWall += (obj) =>
            {
                postEffectCamera.Shake(new Vector2(OVERHAND_THROWN_CAMERA_SHAKE_POWER, OVERHAND_THROWN_CAMERA_SHAKE_POWER), 0.1f, 0.01f, true);
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
