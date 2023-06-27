using System;
using UnityEngine;

namespace StageObject
{
    public class Wall : MonoBehaviour, IHitEffectCollider
    {
        public event Action<EffectCollider> OnHitEffectColliderEventTrigger;

		public void OnHitEffectCollider(EffectCollider col)
        {
            OnHitEffectColliderEventTrigger?.Invoke(col);
        }
    }
}

