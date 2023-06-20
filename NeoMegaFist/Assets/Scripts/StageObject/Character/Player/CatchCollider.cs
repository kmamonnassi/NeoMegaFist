using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageObject
{
    public class CatchCollider : MonoBehaviour
    {
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
            
            if (stageObject != null && stageObject.Size.IsCatchable() && catchAndThrow.IsCatchableObject)
            {
                CharacterBase character = stageObject.GetComponent<CharacterBase>();
                if(character != null)
                {
                    if(character.IsStun)
                    {
                        OnCatch?.Invoke(stageObject);
                    }
                }
                else
                {
                    OnCatch?.Invoke(stageObject);
                }
            }
        }
    }
}