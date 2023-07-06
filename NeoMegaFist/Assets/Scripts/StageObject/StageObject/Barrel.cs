using Effect;
using System.Collections;
using UnityEngine;
using Zenject;

namespace StageObject
{
    public class Barrel : StageObjectBase, IHitEffectCollider
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ThrownCollider thrownCol;
        [SerializeField] private float onBreakWaitKillTime = 1;
        [SerializeField] private Collider2D[] colliders;

        [Inject] private IEffectPlayer effectPlayer;

        public override StageObjectID ID => StageObjectID.Barrel;
        public override StageObjectType Type => StageObjectType.StageObject;
        public override Size DefaultSize => Size.Small;

        private IStageObjectCatchAndThrow catchAndThrow;

        protected override void OnAwake_Virtual()
        {
            base.OnAwake_Virtual();
            catchAndThrow = GetComponent<IStageObjectCatchAndThrow>();
            catchAndThrow.OnCatched += () =>
            {
                animator.Play("Rolling");
            };

            catchAndThrow.OnEndOverhandThrown += () =>
            {
                Break();
            };

            thrownCol.OnHitWall += (wall) =>
            {
                Break();
            };

            thrownCol.OnHitTarget += (target) =>
            {
                Break();
            };

        }

        private void Break()
        {
            catchAndThrow.EndThrown();
            effectPlayer.PlayEffect("BreakBarrel", transform.position);
            Destroy(gameObject);
        }

        public void OnHitEffectCollider(EffectCollider col)
        {
            Break();
        }
    }
}