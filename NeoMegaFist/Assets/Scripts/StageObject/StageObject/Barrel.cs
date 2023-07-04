using System.Collections;
using UnityEngine;

namespace StageObject
{
    public class Barrel : StageObjectBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ThrownCollider thrownCol;
        [SerializeField] private float onBreakWaitKillTime = 1;

        public override StageObjectID ID => StageObjectID.Barrel;
        public override StageObjectType Type => StageObjectType.StageObject;
        public override Size DefaultSize => Size.Small;

        private IStageObjectCatchAndThrow catchAndThrow;

        protected override void OnAwake_Virtual()
        {
            base.OnAwake_Virtual();

            catchAndThrow.OnCatched += () =>
            {
                animator.Play("Rolling");
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
            animator.Play("Break");
        }

        private IEnumerator WaitKill()
        {
            yield return new WaitForSeconds(onBreakWaitKillTime);
            Destroy(gameObject);
        }
    }
}