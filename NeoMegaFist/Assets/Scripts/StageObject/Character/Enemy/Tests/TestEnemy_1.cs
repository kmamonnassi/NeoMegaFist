using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace StageObject
{
    public class TestEnemy_1 : CharacterBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackingTime;
        [SerializeField] private float baseSpeed;

        [Inject] private Player player;

        public override StageObjectID ID => StageObjectID.Mushroom;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Small;

        private bool isAttacking = false;

        protected override void OnFixedUpdate_Virtual()
        {
            base.OnFixedUpdate_Virtual();

            if (IsStun) return;

            if(!isAttacking)
            {
                if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
                {
                    Move();
                }
                else
                {
                    Attack();
                }
            }
        }

        public async void Attack()
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
            await UniTask.Delay(TimeSpan.FromSeconds(attackingTime));
            isAttacking = false;
            transform.DORotate(new Vector3(0, 0, GetAngle(transform.position, player.transform.position)), 1);
        }

        private void Move()
        {
            Vector2 diff = player.transform.position - transform.position;
            Vector2 dir = diff.normalized;
            rb.velocity = dir * baseSpeed * Speed;
            transform.eulerAngles = new Vector3(0, 0, GetAngle(transform.position, player.transform.position));
        }

        private float GetAngle(Vector2 start, Vector2 target)
        {
            Vector2 dt = target - start;
            float rad = Mathf.Atan2(dt.y, dt.x);
            float degree = rad * Mathf.Rad2Deg;

            return degree + 90;
        }
    }
}