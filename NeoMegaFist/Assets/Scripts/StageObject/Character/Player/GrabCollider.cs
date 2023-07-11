using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageObject
{
    public class GrabCollider : MonoBehaviour
    {
        [SerializeField] float rayOffset;
        public event Action<StageObjectBase> OnCatch;

        private void OnCollisionEnter2D(Collision2D col)
        {
            CheckHit(col.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            CheckHit(col.gameObject);
        }

        private void CheckHit(GameObject obj)
        {
            StageObjectBase stageObject = obj.GetComponent<StageObjectBase>();
            StageObjectCatchAndThrow catchAndThrow = obj.GetComponent<StageObjectCatchAndThrow>();
            
            //衝突したコライダがステージオブジェクトであり、サイズが掴めるサイズで掴むことのできるオブジェクト限定で、
            if (stageObject != null && stageObject.Size.IsCatchable() && catchAndThrow.IsCatchableObject && (catchAndThrow.State == ThrownState.Throw || catchAndThrow.State == ThrownState.Freedom))
            {
                Vector2 dir = ((Vector2)(stageObject.transform.position - transform.position)).normalized;
                Vector2 origin = (Vector3)(dir * rayOffset) + transform.position;
                float dist = Vector2.Distance(transform.position, transform.position);
                int mask = LayerMask.GetMask("Wall", "LowWall");
                Debug.DrawRay(origin, dir * dist, Color.green, 1);
                var hit = Physics2D.Raycast(origin, dir, dist, mask);

                if (!hit)
                {
                    //このステージオブジェクトがキャラクターを継承していて、スタンしていれば掴むことができる
                    CharacterBase character = stageObject.GetComponent<CharacterBase>();
                    if (character != null)
                    {
                        if (character.IsStun)
                        {
                            OnCatch?.Invoke(stageObject);
                        }
                    }
                    else
                    {
                        //キャラクターを継承していなければそのまま掴める
                        OnCatch?.Invoke(stageObject);
                    }
                }
            }
        }
    }
}