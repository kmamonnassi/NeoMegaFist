using DG.Tweening;
using InputControl;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
    public class PlayerMove : MonoBehaviour, IPlayerRotate, IUpdate, IFixedUpdate
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Player player;
        [SerializeField] private PlayerRotater rotater;
        [SerializeField] private float moveSpeed = 200;

        [Inject] private IInputer inputer;

        private Vector2 moveDir;
        private bool isInputMoveButton;

        public int Priority => 0;
        public float Rotation { get; private set; }
        public bool IsActive { get; private set; } = true;

        private void Awake()
        {
            rotater.Add(this);
        }

        public void Move()
        {
            if(player.Speed > 0) rb.velocity = moveDir * moveSpeed * player.Speed;
            Rotation = GetAngle(Vector2.zero, rb.velocity) + 90;
            IsActive = true;
        }

        public void Stop()
        {
            rb.velocity = Vector2.zero;
            IsActive = false;
        }

        private float GetAngle(Vector2 start, Vector2 target)
        {
            Vector2 dt = target - start;
            float rad = Mathf.Atan2(dt.y, dt.x);
            float degree = rad * Mathf.Rad2Deg;

            return degree;
        }

        public void ManagedFixedUpdate()
        {
            if (isInputMoveButton)
            {
                Move();
            }
            else
            {
                Stop();
            }
        }

        public void ManagedUpdate()
        {
            moveDir = Vector2.zero;
            if(!player.IsStun)
            {
                moveDir = inputer.GetPlayerMove();
            }

            if (moveDir != Vector2.zero)
            {
                isInputMoveButton = true;
            }
            else
            {
                isInputMoveButton = false;
            }
        }
    }
}
