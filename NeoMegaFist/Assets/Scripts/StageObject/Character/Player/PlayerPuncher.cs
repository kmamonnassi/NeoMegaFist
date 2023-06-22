using InputControl;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
    public class PlayerPuncher : MonoBehaviour, IPlayerRotate, IUpdate
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerRotater rotater;
        [SerializeField] private Player player;
        [SerializeField] private float decressSpeed = 0.8f;

        [Inject] private IInputer inputer; 

        public int Priority => 1;
        public float Rotation { get; private set; }
        public bool IsActive { get; private set; }

        private void Awake()
        {
            rotater.Add(this);
        }

        public void ManagedUpdate()
        {
            if (player.IsStun) return;
            if (inputer.GetPlayerPunchStart())
            {
                animator.SetBool("Punch", true);
                IsActive = true;
                player.SetSpeed(player.Speed - decressSpeed);
            }
            else
               if (inputer.GetPlayerPunch())
            {
                Rotation = GetAngle(transform.position, Camera.main.ScreenToWorldPoint(inputer.GetMousePosition())) + 90;
            }
            else
               if (inputer.GetPlayerPunchEnd())
            {
                animator.SetBool("Punch", false);
                IsActive = false;
                player.SetSpeed(player.Speed + decressSpeed);
            }
        }

        private float GetAngle(Vector2 start, Vector2 target)
        {
            Vector2 dt = target - start;
            float rad = Mathf.Atan2(dt.y, dt.x);
            float degree = rad * Mathf.Rad2Deg;

            return degree;
        }
    }
}