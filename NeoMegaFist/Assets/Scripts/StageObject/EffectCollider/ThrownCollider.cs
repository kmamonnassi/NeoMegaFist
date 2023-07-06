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

        public int Priority => 2;
        public float Scale => 0f;
        public ThrownState State { get; private set; }
        public Collider2D AttackCollider => attackCollider;
        public Collider2D MainCollider => mainCollider;

        private const float THROWN_CAMERA_SHAKE_POWER = 0.005f;
        private const float OVERHAND_THROWN_CAMERA_SHAKE_POWER = 10;

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
                    obj.GetComponent<StageObjectCatchAndThrow>().Thrown(dir, rb.velocity.magnitude / 4);
                    postEffectCamera.Shake(new Vector2(THROWN_CAMERA_SHAKE_POWER, THROWN_CAMERA_SHAKE_POWER) * rb.velocity.magnitude, 0.1f, 0.1f);
                }
                else
                if(State == ThrownState.OverhandThrow)
                {
                    postEffectCamera.Shake(new Vector2(OVERHAND_THROWN_CAMERA_SHAKE_POWER, OVERHAND_THROWN_CAMERA_SHAKE_POWER), 0.1f, 0.01f, true);

                }

                //ヒットストップ
                //timeScaler.Add(this);
                //DOVirtual.DelayedCall(0.15f, () =>
                //{
                //    timeScaler.Remove(this);
                //});

				Rigidbody2D obj_rb = obj.GetComponent<Rigidbody2D>();
				obj_rb.simulated = false;
				rb.simulated = false;
				DOVirtual.DelayedCall(0.05f, () =>
				{
					obj_rb.simulated = true;
					rb.simulated = true;
				});
				AudioReserveManager.AudioReserve("敵", "ストレート投げされたオブジェクトが敵に衝突", transform);
            };

            OnHitWall += (obj) =>
            {
                postEffectCamera.Shake(new Vector2(OVERHAND_THROWN_CAMERA_SHAKE_POWER, OVERHAND_THROWN_CAMERA_SHAKE_POWER), 0.1f, 0.01f, true);

                //ヒットストップ
                //timeScaler.Add(this);
                //DOVirtual.DelayedCall(0.1f, () =>
                //{
                //    timeScaler.Remove(this);
                //});

                //rb.simulated = false;
                //DOVirtual.DelayedCall(0.15f, () =>
                //{
                //    rb.simulated = true;
                //});
                //AudioReserveManager.AudioReserve("プレイヤー", "ストレート投げされたオブジェクトが壁に衝突", transform);
            };
        }
    }
}
