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
            
            //�Փ˂����R���C�_���X�e�[�W�I�u�W�F�N�g�ł���A�T�C�Y���͂߂�T�C�Y�Œ͂ނ��Ƃ̂ł���I�u�W�F�N�g����ŁA
            if (stageObject != null && stageObject.Size.IsCatchable() && catchAndThrow.IsCatchableObject)
            {
                //���̃X�e�[�W�I�u�W�F�N�g���L�����N�^�[���p�����Ă��āA�X�^�����Ă���Β͂ނ��Ƃ��ł���
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
                    //�L�����N�^�[���p�����Ă��Ȃ���΂��̂܂ܒ͂߂�
                    OnCatch?.Invoke(stageObject);
                }
            }
        }
    }
}