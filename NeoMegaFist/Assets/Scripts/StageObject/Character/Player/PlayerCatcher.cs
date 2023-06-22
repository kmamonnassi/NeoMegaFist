using DG.Tweening;
using StageObject;
using UnityEngine;
using Utility;

namespace StageObject
{
    public class PlayerCatcher : MonoBehaviour, IPlayerRotate, IUpdate
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerRotater rotater;
        [SerializeField] private CatchCollider catchCollider;
        [SerializeField] private Player player;
        [SerializeField] private float catchInterval = 0.35f;//�͂ݓ���̃N�[���^�C��
        [SerializeField] private float baseThrowPower = 50;//�������

        public int Priority => 2;
        public float Rotation { get; private set; }
        public bool IsActive { get; private set; }

        private bool isCatching = false;//�͂ݓ�����s���Ă��邩
        private StageObjectCatchAndThrow catchTarget;//�͂�ł���I�u�W�F�N�g

        private void Awake()
        {
            rotater.Add(this);
            //�͂ޔ͈͂̃R���C�_�[�ɐڐG������͂ރ��\�b�h�𔭉�
            catchCollider.OnCatch += Catched;
        }

        /// <summary>�͂ݓ���</summary>
        public void Catching()
        {
            animator.SetTrigger("Catch");
            IsActive = true;
            isCatching = true;
            Rotation = GetAngle(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) + 90;
            DOVirtual.DelayedCall(catchInterval, () =>
            {
                isCatching = false;
                IsActive = false;
            });
        }

        /// <summary>�I�u�W�F�N�g��͂�</summary>
        private void Catched(StageObjectBase stageObject)
        {
            if (catchTarget != null) return;
            catchTarget = stageObject.GetComponent<StageObjectCatchAndThrow>();
            catchTarget.Catched();

            //�͂�ł�����̂����ꂽ���A�͂�ł�����̂������ϐ���null�ɂ���
            void OnReleased()
            {
                catchTarget.OnReleased -= OnReleased;
                catchTarget = null;
            }
            catchTarget.OnReleased += OnReleased;
        }

        /// <summary>�͂񂾃I�u�W�F�N�g�𓊂���</summary>
        public void Throw(Vector2 dir)
        {
            catchTarget.Thrown(dir, baseThrowPower);
        }

        private float GetAngle(Vector2 start, Vector2 target)
        {
            Vector2 dt = target - start;
            float rad = Mathf.Atan2(dt.y, dt.x);
            float degree = rad * Mathf.Rad2Deg;

            return degree;
        }

        public void ManagedUpdate()
        {
            //�͂�ł�����̂�����΁A��������g�ɒǏ]������
            if(catchTarget != null)
            {
                if(catchTarget.IsCatched)
                {
                    catchTarget.transform.position = transform.position;
                }
            }

            //�X�^�����Ă���Ɖ����ł��Ȃ�
            if (player.IsStun) return;

            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && catchTarget != null)
            {
                if (catchTarget.IsCatched)
                {
                    Vector2 dir = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
                    Throw(dir);
                }
            }
            else
            //�}�E�X�������ꂽ�Ƃ��A�͂�ł�����̂��Ȃ���Β͂ݓ���A����΂���𓊂���
            if (Input.GetMouseButtonDown(1))
            {
                if (!isCatching)
                {
                    Catching();
                }
            }
            
        }
    }
}