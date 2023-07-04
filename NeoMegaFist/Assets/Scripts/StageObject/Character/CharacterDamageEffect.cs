using DG.Tweening;
using System;
using UnityEngine;

namespace StageObject
{
    public class CharacterDamageEffect : MonoBehaviour
    {
        [SerializeField] private Material damageMat;
        [SerializeField] private float defaultEffectDuration = 0.1f;

        private Material[] mats;
        private Tween dmgTween;

        public void Initalize(CharacterBase target)
        {
            mats = new Material[target.SpriteRenderers.Length];
            for (int i = 0; i < target.SpriteRenderers.Length; i++)
            {
                mats[i] = target.SpriteRenderers[i].material;
            }

            target.OnDamageByCollider += dmg =>
            {
                for (int i = 0; i < target.SpriteRenderers.Length; i++)
                {
                    target.SpriteRenderers[i].material = damageMat;
                }
                dmgTween?.Kill();
                dmgTween = DOVirtual.DelayedCall(dmg.HitStopTime, () =>
                {
                    for (int i = 0; i < target.SpriteRenderers.Length; i++)
                    {
                        target.SpriteRenderers[i].material = mats[i];
                    }
                });

                Rigidbody2D obj_rb = target.GetComponent<Rigidbody2D>();
                obj_rb.simulated = false;
                DOVirtual.DelayedCall(dmg.HitStopTime, () =>
                {
                    obj_rb.simulated = true;
                });
            };

            target.OnDamage += dmg =>
            {
                for(int i = 0; i < target.SpriteRenderers.Length;i++)
                {
                    target.SpriteRenderers[i].material = damageMat;
                }
                dmgTween?.Kill();
                dmgTween =  DOVirtual.DelayedCall(defaultEffectDuration, () =>
                {
                    for (int i = 0; i < target.SpriteRenderers.Length; i++)
                    {
                        target.SpriteRenderers[i].material = mats[i];
                    }
                });
            };
        }
    }
}