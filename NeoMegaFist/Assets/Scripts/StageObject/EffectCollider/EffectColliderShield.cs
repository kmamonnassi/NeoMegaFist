using UnityEngine;

namespace StageObject
{
    public class EffectColliderShield : Wall
    {
        [SerializeField] private CharacterBase protectTarget;
        [SerializeField] private bool isBrokenByThrownObject;

        private void Awake()
        {
            OnHitEffectColliderEventTrigger += Protect;
        }

        private void Protect(EffectCollider col)
        {
            float dist_protectTarget = Vector2.Distance(protectTarget.transform.position, col.transform.position);
            float dist_shield = Vector2.Distance(transform.position, col.transform.position);
            if(dist_shield < dist_protectTarget)
                protectTarget.Invisible(1);

            if (col is ThrownCollider && isBrokenByThrownObject)
            {
                Destroy(gameObject);
            }
        }
    }
}