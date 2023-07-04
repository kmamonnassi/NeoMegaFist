using StageObject;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class GateOpenByEnemy : MonoBehaviour
    {
        [SerializeField] private List<CharacterBase> enemies;
        [SerializeField] private Gate gate;

        private void Start()
        {
            //敵が死んだとき、全ての敵が死んだかチェックを走らせて全員死んでたら扉が開く
            for(int i = 0; i < enemies.Count;i++)
            {
                CharacterBase target = enemies[i];
                target.OnDead += () =>
                {
                    enemies.Remove(target);
                    if(enemies.Count == 0)
                    {
                        gate.Open();
                    }
                };
            }
        }
    }
}
