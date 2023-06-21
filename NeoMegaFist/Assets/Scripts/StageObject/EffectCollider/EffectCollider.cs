using StageObject.Buff;
using System;
using UnityEngine;

namespace StageObject
{
    public class EffectCollider : MonoBehaviour
    {
        [SerializeField] private BuffData[] buffs;
        [SerializeField] private StageObjectType[] hitTargets;
        [SerializeField] private int damage;
        [SerializeField] private int stunDamage;
        [SerializeField] private int knockBackPower;
        [SerializeField] private float coolTime = 0.5f;

        public BuffData[] Buffs { get => buffs;  }
        public StageObjectType[] HitTargets { get => hitTargets; }
        public int Damage { get => damage; }
        public int StunDamage { get => stunDamage; }
        public int KnockBackPower { get => knockBackPower; }
        public float CoolTime { get => coolTime; }

        public event Action OnHitWall;
        public event Action<StageObjectBase> OnHitTarget;

        private void OnCollisionEnter2D(Collision2D col)
        {
            Check(col.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Check(col.gameObject);
        }

        private void Check(GameObject obj)
        {
            StageObjectBase stageObject = obj.GetComponent<StageObjectBase>();
            if (stageObject != null)
            {
                if (Array.Exists(HitTargets, x => stageObject.Type == x))
                {
                    OnHitTarget?.Invoke(stageObject);
                    stageObject.GetComponent<CharacterBase>()?.HitEffectColliderDamage(this);
                }
            }
            else
            if(obj.GetComponent<Wall>() != null)
            {
                OnHitWall?.Invoke();
            }
        }
    }
}