using System;
using UnityEngine;

namespace StageObject
{
    public abstract class CharacterBase : StageObjectBase
    {
        [SerializeField] private int maxHP = 0;
        private int hp = 0;

        public int MaxHP { get => maxHP; }
        public int HP { get => hp; }

        public event Action<int> OnSetHP;
        public event Action<int> OnSetMaxHP;
        public event Action<int> OnDamage;
        public event Action<int> OnHeal;
        public event Action OnDead;

        public void Damage(int pt)
        {
            hp -= pt;
            OnDamage?.Invoke(pt);
            if(hp <= 0)
            {
                Dead();
            }
        }

        public void Heal(int pt)
        {
            hp += pt;
            OnHeal?.Invoke(pt);
            if(maxHP < hp)
            {
                hp = maxHP;
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

        public void Dead()
        {
            OnDead?.Invoke();
            Kill();
        }
    }
}