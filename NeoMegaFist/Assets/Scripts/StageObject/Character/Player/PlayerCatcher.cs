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
        [SerializeField] private GameObject overhandThrowMark;//上投げで投げる場所を示すマーク
        [SerializeField] private float catchInterval = 0.35f;//掴み動作のクールタイム
        [SerializeField] private float baseThrowPower = 50;//投げる力
        [SerializeField] private float baseOverhandThrowDuration = 0.5f;//上投げの最大飛距離
        [SerializeField] private float baseOverhandThrowDistance = 50;//上投げの最大飛距離

        [Inject] private IInputer inputer;
        [Inject] private IPostEffectCamera cam;

        public int RotationPriority => 2;
        public float Rotation { get; private set; }
        public bool RotationIsActive { get; private set; }

        private bool isCatching = false;//掴み動作を行っているか
        private bool overhandThrowPreparation = false;//上投げの準備中か
        private StageObjectCatchAndThrow catchTarget;//掴んでいるオブジェクト

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
            isCatching = true;
            Rotation = GetAngle(transform.position, cam.ScreenToWorldPoint(inputer.GetMousePosition())) + 90;
            DOVirtual.DelayedCall(catchInterval, () =>
            {
                isCatching = false;
                RotationIsActive = false;
            });
        }

        /// <summary>オブジェクトを掴む</summary>
        private void Catched(StageObjectBase stageObject)
        {
            if (catchTarget != null) return;
            catchTarget = stageObject.GetComponent<StageObjectCatchAndThrow>();
            catchTarget.Catched();

            //掴んでいるものが離れた時、掴んでいるものを示す変数をnullにする
            void OnReleased()
            {
                catchTarget.OnReleased -= OnReleased;
                catchTarget = null;
            }
            catchTarget.OnReleased += OnReleased;
        }

        /// <summary>掴んだオブジェクトを投げる</summary>
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
            //掴んでいるものがあれば、それを自身に追従させる
            if(catchTarget != null)
            {
                if(catchTarget.State == ThrownState.Catch)
                {
                    catchTarget.transform.position = transform.position;
                }
            }

            //スタンしていると何もできない
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
            //マウスが押されたとき、掴んでいるものがなければ掴み動作、あればそれを投げる
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