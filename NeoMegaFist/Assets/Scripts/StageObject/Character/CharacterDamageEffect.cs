using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace StageObject
{
    public class CharacterDamageEffect : MonoBehaviour
    {
        [SerializeField] private Material damageMat;
        [SerializeField] private float effectDuration = 0.05f;

        public void Initalize(CharacterBase target)
        {
            target.OnDamage += async dmg =>
            {
                Material[] mats = new Material[target.SpriteRenderers.Length];
                for(int i = 0; i < target.SpriteRenderers.Length;i++)
                {
                    mats[i] = target.SpriteRenderers[i].material;
                    target.SpriteRenderers[i].material = damageMat;
                }
                await UniTask.Delay(TimeSpan.FromSeconds(effectDuration));
                for (int i = 0; i < target.SpriteRenderers.Length; i++)
                {
                    target.SpriteRenderers[i].material = mats[i];
                }
            };
        }
    }
}