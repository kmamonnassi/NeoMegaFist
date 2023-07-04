using StageObject.Buff;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StageObject
{
    public class EffectCollider : MonoBehaviour
    {
        [SerializeField] private HitColliderDamage hitColliderDamage = new HitColliderDamage(null, new [] { StageObjectType.Enemy, StageObjectType.StageObject }, 50, 50, 50, 0.5f);

        public HitColliderDamage HitColliderDamage => hitColliderDamage;

        public event Action<GameObject> OnHitWall;
        public event Action<StageObjectBase> OnHitTarget;

        public List<GameObject> ignores = new List<GameObject>();
        private List<GameObject> hitList = new List<GameObject>();

        private void Start()
        {
            hitColliderDamage.Object = gameObject;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(this.enabled)
            {
                hitList.Add(col.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (this.enabled)
            {
                hitList.Add(col.gameObject);
            }
        }

        private void LateUpdate()
        {
            if (hitList.Count == 0) return;
            hitList = hitList.OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).ToList();
            foreach (GameObject hitObject in hitList)
            {
                Check(hitObject);
            }
            hitList.Clear();
        }

        private void Check(GameObject obj)
        {
            if (ignores.Contains(obj)) return;

            StageObjectBase stageObject = obj.GetComponent<StageObjectBase>();
            if (stageObject != null)
            {
                if (Array.Exists(hitColliderDamage.HitTargets, x => stageObject.Type == x))
                {
                    OnHitTarget?.Invoke(stageObject);
                    stageObject.GetComponent<CharacterBase>()?.Damage(hitColliderDamage);
                }
            }
            else
            {
                IHitEffectCollider hitEffectCollider = obj.GetComponent<IHitEffectCollider>();
                if (hitEffectCollider != null)
                {
                    hitEffectCollider.OnHitEffectCollider(this);
                    if(hitEffectCollider is Wall)
                    {
                        Wall wall = hitEffectCollider as Wall;
                        OnHitWall?.Invoke(obj);
                    }
                }
            }
        }

        public void IgnoreCollision(GameObject col, bool ignore = true)
        {
            if(ignore)
            {
                ignores.Add(col);
            }
            else
            {
                ignores.Remove(col);
            }
        }
    }

    [System.Serializable]
    public class HitColliderDamage
    {
        [SerializeField] private BuffData[] buffs;
        [SerializeField] private StageObjectType[] hitTargets;
        [SerializeField] private int damage;
        [SerializeField] private int stunDamage;
        [SerializeField] private int knockBackPower;
        [SerializeField] private float coolTime = 0.5f;
        [SerializeField] private float hitStopTime = 0.1f;

        public BuffData[] Buffs { get => buffs; set => buffs = value; }
        public StageObjectType[] HitTargets { get => hitTargets; set => hitTargets = value; }
        public int Damage { get => damage; set => damage = value; }
        public int StunDamage { get => stunDamage; set => stunDamage = value; }
        public int KnockBackPower { get => knockBackPower; set => knockBackPower = value; }
        public float CoolTime { get => coolTime; set => coolTime = value; }
        public float HitStopTime { get => hitStopTime; set => hitStopTime = value; }

        public GameObject Object { get; set; }

        public HitColliderDamage(BuffData[] buffs, StageObjectType[] hitTargets, int damage, int stunDamage, int knockBackPower, float coolTime)
        {
            this.Buffs = buffs;
            this.HitTargets = hitTargets;
            this.Damage = damage;
            this.StunDamage = stunDamage;
            this.KnockBackPower = knockBackPower;
            this.CoolTime = coolTime;
        }
    }
}