using DG.Tweening;
using InputControl;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
    public class PlayerWalk : MonoBehaviour, IPlayerRotate, IUpdate, IFixedUpdate, IPlayerMove
    {
        [SerializeField] private Player player;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerRotater rotater;
        [SerializeField] private float moveSpeed = 200;

        [Inject] private IInputer inputer;

        private bool isInputMoveButton;

        public int RotationPriority => 0;
        public float Rotation { get; private set; }
        public bool RotationIsActive { get; private set; } = true;

        public int MovePriority => 0;
        public Vector2 MoveVelocity { get; private set; }
        public bool MoveIsActive { get; private set; } = true;

        private void Awake()
        {
            rotater.Add(this);
            playerMover.Add(this);
        }

        public void Move()
        {
            Rotation = GetAngle(Vector2.zero, MoveVelocity) + 90;
            RotationIsActive = true;
        }

        public void Stop()
        {
            MoveVelocity = Vector2.zero;
            RotationIsActive = false;
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
            MoveVelocity = Vector2.zero;
            if(!player.IsStun)
            {
                MoveVelocity = inputer.GetPlayerMove() * moveSpeed;
            }

            if (MoveVelocity != Vector2.zero)
            {
                isInputMoveButton = true;
            }
            else
            {
                isInputMoveButton = false;
            }
        }

        private void OnDestroy()
        {
            rotater.Remove(this);
            playerMover.Remove(this);
        }
    }
}
