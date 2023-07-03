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
        [SerializeField] private GameObject overhandThrowMark;//上投げで投げる場所を示すマーク
        [SerializeField] private float catchInterval = 0.35f;//掴み動作のクールタイム
        [SerializeField] private float baseThrowPower = 50;//投げる力
        [SerializeField] private float baseOverhandThrowDuration = 0.5f;//上投げの最大飛距離
        [SerializeField] private float baseOverhandThrowDistance = 50;//上投げの最大飛距離
        [SerializeField] private float raycastOffset = 4.1f;
        [SerializeField] private float throwPreparationLineLength = 320f;

        [Inject] private IInputer inputer;
        [Inject] private IPostEffectCamera cam;

        public int RotationPriority => 2;
        public float Rotation { get; private set; }
        public bool RotationIsActive { get; private set; }

        public bool PlayingCatchMotion { get; private set; } = false;//掴み動作を行っているか
        private bool throwPreparation = false;//投げの準備中
        private bool overhandThrowPreparation = false;//上投げの準備中
        public StageObjectCatchAndThrow CatchTarget { get; private set; }//掴んでいるオブジェクト

        private void Awake()
        {
            rotater.Add(this);
            //掴む範囲のコライダーに接触したら掴むメソッドを発火
            catchCollider.OnCatch += Catched;
        }

        /// <summary>掴み動作</summary>
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

        /// <summary>オブジェクトを掴む</summary>
        private void Catched(StageObjectBase stageObject)
        {
            if (CatchTarget != null) return;
            CatchTarget = stageObject.GetComponent<StageObjectCatchAndThrow>();
            CatchTarget.Catched();

            //掴んでいるものが離れた時、掴んでいるものを示す変数をnullにする
            void OnReleased()
            {
                CatchTarget.OnReleased -= OnReleased;
                CatchTarget = null;
            }
            CatchTarget.OnReleased += OnReleased;
        }

        /// <summary>掴んだオブジェクトを投げる</summary>
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
            //掴んでいるものがあれば、それを自身に追従させる
            if(CatchTarget != null)
            {
                if(CatchTarget.State == ThrownState.Catch)
                {
                    CatchTarget.transform.position = transform.position;
                }
            }

            //スタンしていると何もできない
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
            //マウスが押されたとき、掴んでいるものがなければ掴み動作、あればそれを投げる
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