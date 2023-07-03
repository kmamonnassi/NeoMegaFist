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
        [SerializeField] private LineRenderer throwPreparationLine;
        [SerializeField] private GameObject overhandThrowMark;//�㓊���œ�����ꏊ�������}�[�N
        [SerializeField] private float catchInterval = 0.35f;//�͂ݓ���̃N�[���^�C��
        [SerializeField] private float baseThrowPower = 50;//�������
        [SerializeField] private float baseOverhandThrowDuration = 0.5f;//�㓊���̍ő�򋗗�
        [SerializeField] private float baseOverhandThrowDistance = 50;//�㓊���̍ő�򋗗�
        [SerializeField] private float raycastOffset = 4.1f;
        [SerializeField] private float throwPreparationLineLength = 320f;

        [Inject] private IInputer inputer;
        [Inject] private IPostEffectCamera cam;

        public int RotationPriority => 2;
        public float Rotation { get; private set; }
        public bool RotationIsActive { get; private set; }

        public bool PlayingCatchMotion { get; private set; } = false;//�͂ݓ�����s���Ă��邩
        private bool throwPreparation = false;//�����̏�����
        private bool overhandThrowPreparation = false;//�㓊���̏�����
        public StageObjectCatchAndThrow CatchTarget { get; private set; }//�͂�ł���I�u�W�F�N�g

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
            PlayingCatchMotion = true;
            Rotation = GetAngle(transform.position, cam.ScreenToWorldPoint(inputer.GetMousePosition())) + 90;
            DOVirtual.DelayedCall(catchInterval, () =>
            {
                PlayingCatchMotion = false;
                RotationIsActive = false;
            });
        }

        /// <summary>�I�u�W�F�N�g��͂�</summary>
        private void Catched(StageObjectBase stageObject)
        {
            if (CatchTarget != null) return;
            CatchTarget = stageObject.GetComponent<StageObjectCatchAndThrow>();
            CatchTarget.Catched();

            //�͂�ł�����̂����ꂽ���A�͂�ł�����̂������ϐ���null�ɂ���
            void OnReleased()
            {
                CatchTarget.OnReleased -= OnReleased;
                CatchTarget = null;
            }
            CatchTarget.OnReleased += OnReleased;
        }

        /// <summary>�͂񂾃I�u�W�F�N�g�𓊂���</summary>
        public void Throw(Vector2 dir)
        {
            CatchTarget?.Thrown(dir, baseThrowPower);
        }

        public void StartOverhandThrow()
        {
            overhandThrowPreparation = true;
            overhandThrowMark.SetActive(true);
        }

        private void OverhandThrowPreparation()
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
                if (Vector2.Distance(mousePos, transform.position) > Vector2.Distance(hitWall.point, transform.position))
                    thrownPos = hitWall.point;
            }

            overhandThrowMark.transform.position = thrownPos;
            if (inputer.GetOverhandThrowEnd())
            {
                OverhandThrow(thrownPos);
            }
            if (CatchTarget == null)
            {
                overhandThrowPreparation = false;
                overhandThrowMark.SetActive(false);
            }
        }

        public void OverhandThrow(Vector2 position)
        {
            if (CatchTarget != null)
            {
                CatchTarget.OverhandThrown(position, baseOverhandThrowDuration);
            }
            overhandThrowPreparation = false;
            overhandThrowMark.SetActive(false);
        }

        public void StartThrowPreparation()
        {
            throwPreparation = true;
            throwPreparationLine.gameObject.SetActive(true);
        }

        public void ThrowPreparation()
        {
            Vector2 dir = (cam.ScreenToWorldPoint(inputer.GetMousePosition()) - (Vector2)transform.position).normalized;
            float rayLength = throwPreparationLineLength - Vector2.Distance(transform.position, (Vector2)transform.position + dir * raycastOffset);
            Vector2 rayOrigin = (Vector2)transform.position + dir * raycastOffset;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, dir, rayLength, LayerMask.GetMask("Wall"));
            
            throwPreparationLine.positionCount = 1;
            throwPreparationLine.SetPosition(0, transform.position);

            for (int i = 0; i < 200;i++)
            {
                if (hit)
                {
                    throwPreparationLine.positionCount++;
                    throwPreparationLine.SetPosition(i + 1, hit.point);

                    dir = Vector2.Reflect(dir, hit.normal);
                    rayLength -= Vector2.Distance(rayOrigin, hit.point);
                    rayOrigin = hit.point;
                    hit = Physics2D.Raycast(rayOrigin + dir, dir, rayLength, LayerMask.GetMask("Wall"));
                }
                else
                {
                    throwPreparationLine.positionCount++;
                    throwPreparationLine.SetPosition(i + 1, rayOrigin + dir * rayLength);
                    break;
                }
            }
            if (CatchTarget == null)
            {
                throwPreparation = false;
                throwPreparationLine.gameObject.SetActive(false);
            }
            if (inputer.GetPlayerThrowEnd())
            {
                ThrowToMousePos();
            }
        }

        public void ThrowToMousePos()
        {
            if(CatchTarget != null)
            {
                Vector2 dir = (cam.ScreenToWorldPoint(inputer.GetMousePosition()) - (Vector2)transform.position).normalized;
                Throw(dir);
            }
            throwPreparation = false;
            throwPreparationLine.gameObject.SetActive(false);
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
            if(CatchTarget != null)
            {
                if(CatchTarget.State == ThrownState.Catch)
                {
                    CatchTarget.transform.position = transform.position;
                }
            }

            //�X�^�����Ă���Ɖ����ł��Ȃ�
            if (player.IsStun) return;

            if (overhandThrowPreparation)
            {
                OverhandThrowPreparation();
            }

            if(throwPreparation)
            {
                ThrowPreparation();
			}

            if (CatchTarget != null)
            {
                if (CatchTarget.State == ThrownState.Catch)
                {
                    if (inputer.GetPlayerThrowStart())
                    {
                        StartThrowPreparation();
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
            if (inputer.GetPlayerCatch() && CatchTarget == null)
            {
                if (!PlayingCatchMotion)
                {
                    Catching();
                }
            }
        }
    }
}