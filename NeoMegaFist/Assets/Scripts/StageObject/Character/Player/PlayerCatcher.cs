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
        [SerializeField] private float catchInterval = 0.35f;//掴み動作のクールタイム
        [SerializeField] private float baseThrowPower = 50;//投げる力

        public int Priority => 2;
        public float Rotation { get; private set; }
        public bool IsActive { get; private set; }

        private bool isCatching = false;//掴み動作を行っているか
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
            IsActive = true;
            isCatching = true;
            Rotation = GetAngle(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) + 90;
            DOVirtual.DelayedCall(catchInterval, () =>
            {
                isCatching = false;
                IsActive = false;
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
                if(catchTarget.IsCatched)
                {
                    catchTarget.transform.position = transform.position;
                }
            }

            //スタンしていると何もできない
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
            //マウスが押されたとき、掴んでいるものがなければ掴み動作、あればそれを投げる
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