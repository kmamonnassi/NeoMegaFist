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
        [SerializeField] private float catchInterval = 0.35f;

        public int Priority => 2;
        public float Rotation { get; private set; }
        public bool IsActive { get; private set; }

        private bool isCatching = false;
        private StageObjectCatchAndThrow catchTarget;

        private void Awake()
        {
            rotater.Add(this);
            catchCollider.OnCatch += Catch;
        }

        private void Catch(StageObjectBase stageObject)
        {
            catchTarget = stageObject.GetComponent<StageObjectCatchAndThrow>();
            catchTarget.Catched();
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
            if(catchTarget != null)
            {
                if(catchTarget.IsCatched)
                {
                    catchTarget.transform.position = transform.position;
                }
            }
            if (player.IsStun) return;
            if (Input.GetMouseButtonDown(1) && !isCatching)
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
        }
    }
}