using StageObject;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class MonsterHouse : MonoBehaviour
    {
        [SerializeField] private CharacterBase[] enemies;

        public IReadOnlyList<CharacterBase> Enemies => enemies;
        public IReadOnlyList<bool> IsKilled => isKilled;

        private bool[] isKilled;

        public event Action OnKilledMonster;
        public event Action OnClearMonsterHouse;

        private void Start()
        {
            isKilled = new bool[enemies.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].OnKill += () => CheckClear(i);
            }
        }

        private void CheckClear(int i)
        {
            isKilled[i] = true;
            OnKilledMonster?.Invoke();
            for(int j = 0; j < isKilled.Length;j++)
            {
                if(!isKilled[j])
                {
                    return;
                }
            }
            OnClearMonsterHouse?.Invoke();
        }
    }
}