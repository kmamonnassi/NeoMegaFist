using DG.Tweening;
using System;
using UnityEngine;

namespace StageObject
{
    public class CharacterDamageEffect : MonoBehaviour
    {
        [SerializeField] private Material damageMat;
        [SerializeField] private float effectDuration = 0.05f;

        private Material[] mats;
        private Tween dmgTween;

        public void Initalize(CharacterBase target)
        {
            mats = new Material[target.SpriteRenderers.Length];
            for (int i = 0; i < target.SpriteRenderers.Length; i++)
            {
                mats[i] = target.SpriteRenderers[i].material;
            }

            target.OnDamage += dmg =>
            {
                for(int i = 0; i < target.SpriteRenderers.Length;i++)
                {
                    target.SpriteRenderers[i].material = damageMat;
                }
                dmgTween?.Kill();
                dmgTween =  DOVirtual.DelayedCall(effectDuration, () =>
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