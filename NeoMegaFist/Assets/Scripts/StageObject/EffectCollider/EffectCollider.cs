using StageObject.Buff;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace StageObject
{
    public class EffectCollider : MonoBehaviour
    {
        [SerializeField] private HitColliderDamage hitColliderDamage = new HitColliderDamage(null, new [] { StageObjectType.Enemy, StageObjectType.StageObject }, 50, 50, 50, 0.5f);
        [SerializeField] private bool onHitWallToIgnore = false;
        [SerializeField] private float onHitWallToIgnoreRaycastLength = 10;
        [SerializeField] private float onHitWallToIgnoreRaycastOffset = 0;

        public HitColliderDamage HitColliderDamage => hitColliderDamage;

        public event Action<GameObject> OnHitWall;
        /// <summary>ìGÇÃçUåÇÇéÛÇØÇΩè⁄ç◊Ç»à íuÇéÊìæ(îÒêÑèß)</summary>
        public event Action<StageObjectBase, Vector2> OnHitTargetByPosition;
        public event Action<StageObjectBase> OnHitTarget;

        public List<GameObject> ignores = new List<GameObject>();
        private List<GameObject> hitList = new List<GameObject>();

        protected virtual void Start()
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

            IHitEffectCollider hitEffectCollider = obj.GetComponent<IHitEffectCollider>();
            if (hitEffectCollider != null)
            {
                hitEffectCollider.OnHitEffectCollider(this);
                if (hitEffectCollider is Wall)
                {
                    Wall wall = hitEffectCollider as Wall;
                    OnHitWall?.Invoke(obj);
                }
            }

            if (stageObject != null)
            {
                CharacterBase character = stageObject.GetComponent<CharacterBase>();
                if (character != null)
                {
                    if (character.InvisibleTime > 0) return;
				}

                Vector2 dir = ((Vector2)(obj.transform.position - transform.position)).normalized;
                float rayLength = onHitWallToIgnoreRaycastLength - Vector2.Distance(transform.position, (Vector2)transform.position + dir * onHitWallToIgnoreRaycastOffset);
                Vector2 rayOrigin = (Vector2)transform.position + dir * onHitWallToIgnoreRaycastOffset;

                if (onHitWallToIgnore)
                {
                    RaycastHit2D hitWall = Physics2D.Raycast(rayOrigin, dir, rayLength, LayerMask.GetMask("Wall"));
                    if (hitWall) 
                    {
                        if (Vector2.Distance(obj.transform.position, rayOrigin) > Vector2.Distance(hitWall.point, rayOrigin)) return;
					}
                }

                if (Array.Exists(hitColliderDamage.HitTargets, x => stageObject.Type == x))
                {
                    RaycastHit2D hitTarget = Physics2D.Raycast(rayOrigin, dir, rayLength, LayerMask.GetMask("StageObject"));
                    if(hitTarget)
                    {
                        OnHitTargetByPosition?.Invoke(stageObject, hitTarget.point);
                    }
                    OnHitTarget?.Invoke(stageObject);
                    character?.Damage(hitColliderDamage);
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