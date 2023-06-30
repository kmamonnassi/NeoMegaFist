using DG.Tweening;
using InputControl;
using StageObject;
using System.Collections;
using UnityEngine;
using Utility;
using Utility.PostEffect;
using Zenject;

namespace StageObject
{
    public class PlayerCatcher : MonoBehaviour, IPlayerRotate, IUpdate
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerRotater rotater;
        [SerializeField] private CatchCollider catchCollider;
        [SerializeField] private Player player;
        [SerializeField] private GameObject overhandThrowMark;//�㓊���œ�����ꏊ�������}�[�N
        [SerializeField] private float catchInterval = 0.35f;//�͂ݓ���̃N�[���^�C��
        [SerializeField] private float baseThrowPower = 50;//�������
        [SerializeField] private float baseOverhandThrowDuration = 0.5f;//�㓊���̍ő�򋗗�
        [SerializeField] private float baseOverhandThrowDistance = 50;//�㓊���̍ő�򋗗�

        [Inject] private IInputer inputer;
        [Inject] private IPostEffectCamera cam;

        public int RotationPriority => 2;
        public float Rotation { get; private set; }
        public bool RotationIsActive { get; private set; }

        private bool isCatching = false;//�͂ݓ�����s���Ă��邩
        private bool overhandThrowPreparation = false;//�㓊���̏�������
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
            RotationIsActive = true;
            isCatching = true;
            Rotation = GetAngle(transform.position, cam.ScreenToWorldPoint(inputer.GetMousePosition())) + 90;
            DOVirtual.DelayedCall(catchInterval, () =>
            {
                isCatching = false;
                RotationIsActive = false;
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

        public void StartOverhandThrow()
        {
            overhandThrowPreparation = true;
            overhandThrowMark.SetActive(true);
        }

        public void OverhandThrow(Vector2 position)
        {
            if(catchTarget != null)
            {
                catchTarget.OverhandThrown(position, baseOverhandThrowDuration);
            }
            overhandThrowPreparation = false;
            overhandThrowMark.SetActive(false);
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
                if(catchTarget.State == ThrownState.Catch)
                {
                    catchTarget.transform.position = transform.position;
                }
            }

            //�X�^�����Ă���Ɖ����ł��Ȃ�
            if (player.IsStun) return;

            if (overhandThrowPreparation)
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(inputer.GetMousePosition());
                Vector2 thrownPos = mousePos;
                var hitWall = Physics2D.Raycast(transform.position, (mousePos - (Vector2)transform.position).normalized, baseOverhandThrowDistance, 1 << LayerMask.NameToLayer("Wall"));

                if (Vector2.Distance(mousePos, transform.position) > baseOverhandThrowDistance)
                {
                    thrownPos = (Vector2)transform.position + (mousePos - (Vector2)transform.position).normalized * baseOverhandThrowDistance;
                }
                if (hitWall)
                {
                    if(Vector2.Distance(mousePos, transform.position) > Vector2.Distance(hitWall.point, transform.position))
                    thrownPos = hitWall.point;
                }

                overhandThrowMark.transform.position = thrownPos;
                if (inputer.GetOverhandThrowEnd())
                {
                    OverhandThrow(thrownPos);
                }
                if(catchTarget == null)
                {
                    overhandThrowPreparation = false;
                    overhandThrowMark.SetActive(false);
                }
            }

            if (catchTarget != null)
            {
                if (catchTarget.State == ThrownState.Catch)
                {
                    if (inputer.GetPlayerThrowStart())
                    {
                        Vector2 dir = (cam.ScreenToWorldPoint(inputer.GetMousePosition()) - (Vector2)transform.position).normalized;
                        Throw(dir);
                    }
                    else
                    if (inputer.GetOverhandThrowStart())
                    {
                        StartOverhandThrow();
                    }
                }
            }
            else
            //�}�E�X�������ꂽ�Ƃ��A�͂�ł�����̂��Ȃ���Β͂ݓ���A����΂���𓊂���
            if (inputer.GetPlayerCatch() && catchTarget == null)
            {
                if (!isCatching)
                {
                    Catching();
                }
            }
        }
    }
}