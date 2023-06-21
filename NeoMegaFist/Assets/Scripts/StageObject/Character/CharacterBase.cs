using StageObject.Buff;
using System;
using UnityEngine;
using Zenject;

namespace StageObject
{
    public abstract class CharacterBase : StageObjectBase
    {
        [SerializeField] private int maxHP = 30;
        private int hp = 0;
        [SerializeField] private int maxStamina = 10;
        private int stamina = 0;
        [SerializeField] private float stunDuration = 1;

        public Vector2 CharacterStatusUIOffset = new Vector2(0, -48);

        [Inject] private ICharacterStatusUIManager uiManager;

        public int MaxHP { get => maxHP; }
        public int HP { get => hp; }
        public int MaxStamina { get => maxStamina; }
        public int Stamina { get => stamina; }
        public bool IsStun { get; private set; }

        public event Action<int> OnSetHP;
        public event Action<int> OnSetMaxHP;
        public event Action<int> OnDamage;
        public event Action<int> OnHeal;
        public event Action<int> OnSetStamina;
        public event Action<int> OnSetMaxStamina;
        public event Action<int> OnStunDamage;
        public event Action OnStun;
        public event Action OnEndStun;
        public event Action OnDead;

        private float nowStunDuration;
        private int thrownHitDamage = 50;
        private float nowDamageCoolTime = 0;

        protected override void OnAwake_Virtual()
        {
            hp = maxHP;
            stamina = maxStamina;
            uiManager.Create(this);
            StageObjectCatchAndThrow catchAndThrow = GetComponent<StageObjectCatchAndThrow>();
            if(catchAndThrow.ThrownCollider != null)
                catchAndThrow.ThrownCollider.OnHit += (obj) => Damage(thrownHitDamage);
        }

        public void HitEffectColliderDamage(EffectCollider col)
        {
            if (nowDamageCoolTime <= 0)
            {
                if (col.Damage > 0) Damage(col.Damage);
                if (col.StunDamage > 0) StunDamage(col.StunDamage);

                for (int i = 0; i < col.Buffs.Length; i++)
                {
                    GetComponent<IStageObjectBuffManager>().Add(col.Buffs[i]);
                }
                KnockBack(-((Vector2)(transform.position - col.transform.position)).normalized, col.KnockBackPower);
                nowDamageCoolTime = col.CoolTime;
            }
        }

        public void Damage(int pt)
        {
            SetHP(HP - pt);
            OnDamage?.Invoke(pt);
            if (hp <= 0)
            {
                Dead();
            }
        }

        public void Heal(int pt)
        {
            SetHP(pt + hp);
            OnHeal?.Invoke(pt);
            if(maxHP < hp)
            {
                SetHP(maxHP);
            }
        }

        public void SetHP(int hp)
        {
            this.hp = hp;
            OnSetHP?.Invoke(hp);
        }

        public void SetMaxHP(int maxHP)
        {
            this.maxHP = maxHP;
            OnSetMaxHP?.Invoke(maxHP);
        }

        public void StunDamage(int pt)
        {
            if (stamina <= 0) return;
            SetStamina(stamina - pt);
            OnStunDamage?.Invoke(pt);
            if (stamina <= 0)
            {
                Stun();
            }
        }

        public void Stun()
        {
            nowStunDuration = stunDuration;
            IsStun = true;
            OnStun?.Invoke();
        }

        public void EndStun()
        {
            SetStamina(maxStamina);
            IsStun = false;
            OnEndStun?.Invoke();
        }

        public void SetStamina(int stamina)
        {
            this.stamina = stamina;
            OnSetStamina(stamina);
        }

        public void SetMaxStamina(int maxStamina)
        {
            this.maxStamina = maxStamina;
            OnSetMaxStamina?.Invoke(maxStamina);
        }

        protected override void OnUpdate_Virtual()
        {
            base.OnUpdate_Virtual();

            if (IsStun)
            {
                nowStunDuration -= Time.deltaTime;
                if (nowStunDuration <= 0)
                {
                    EndStun();
                }
            }
            if(nowDamageCoolTime > 0)
            {
                nowDamageCoolTime -= Time.deltaTime;
            }
        }

        public void Dead()
        {
            OnDead?.Invoke();
            Kill();
        }
    }
}