using UnityEngine;

namespace StageObject
{
    public class OverhandThrownImpact : MonoBehaviour
    {
        [SerializeField] private EffectCollider effectCol;
        [SerializeField] private CircleCollider2D circleCol;

        public void Initalize(ThrownCollider thrownCollider)
        {
            effectCol.HitColliderDamage.Buffs = thrownCollider.HitColliderDamage.Buffs;
            effectCol.HitColliderDamage.HitTargets = thrownCollider.HitColliderDamage.HitTargets;
            effectCol.HitColliderDamage.Damage = thrownCollider.HitColliderDamage.Damage / 2;
            effectCol.HitColliderDamage.StunDamage = thrownCollider.HitColliderDamage.StunDamage / 2;
            effectCol.HitColliderDamage.KnockBackPower = thrownCollider.HitColliderDamage.KnockBackPower;
            effectCol.HitColliderDamage.CoolTime = thrownCollider.HitColliderDamage.CoolTime;
            effectCol.IgnoreCollision(thrownCollider.MainCollider.gameObject);

            if (thrownCollider.AttackCollider is CircleCollider2D)
            {
                circleCol.radius = ((CircleCollider2D)thrownCollider.AttackCollider).radius;
            }
            if (thrownCollider.AttackCollider is BoxCollider2D)
            {
                circleCol.radius = ((BoxCollider2D)thrownCollider.AttackCollider).size.x;
            }
            circleCol.radius *= 3;
        }
    }
}
