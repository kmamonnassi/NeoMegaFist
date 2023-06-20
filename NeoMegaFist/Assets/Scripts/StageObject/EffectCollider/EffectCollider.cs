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
                if (Array.Exists(hitTargets, x => stageObject.Type == x))
                {
                    if(damage > 0)
                    {
                        stageObject.GetComponent<CharacterBase>()?.Damage(damage);
                    }
                    if(stunDamage > 0)
                    {
                        stageObject.GetComponent<CharacterBase>()?.StunDamage(stunDamage);
                    }
                    for (int i = 0; i < buffs.Length; i++)
                    {
                        stageObject.GetComponent<IStageObjectBuffManager>().Add(buffs[i]);
                    }
                    stageObject.KnockBack(-((Vector2)(transform.position - obj.transform.position)).normalized, knockBackPower);
                }
            }
        }
    }
}