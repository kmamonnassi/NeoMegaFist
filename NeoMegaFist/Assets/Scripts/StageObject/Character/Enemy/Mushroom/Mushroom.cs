using UnityEngine;
using Zenject;

namespace StageObject
{
    public class Mushroom : CharacterBase
    {
        [SerializeField] private float baseSpeed = 100;
        [SerializeField] private EffectCollider attackCollider;

        public override StageObjectID ID => StageObjectID.Mushroom;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Small;

        [Inject] private Player player;

        protected override void OnAwake_Virtual()
        {
            base.OnAwake_Virtual();
            OnStun += () => attackCollider.enabled = false;
            OnEndStun += () => attackCollider.enabled = true;
        }

        protected override void OnFixedUpdate_Virtual()
        {
            base.OnFixedUpdate_Virtual();
            
            if (IsStun) return;

            Vector2 diff = player.transform.position - transform.position;
            Vector2 dir = diff.normalized;
            rb.velocity = dir * baseSpeed * Speed;
        }
    }
}