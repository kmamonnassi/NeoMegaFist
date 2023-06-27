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

        public int RotationPriority => 1;
        public float Rotation { get; private set; }
        public bool RotationIsActive { get; private set; }

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
                RotationIsActive = true;
                player.SetSpeed(player.Speed - decressSpeed);
            }
            else
               if (inputer.GetPlayerPunch())
            {
                AudioReserveManager.AudioReserve("ÉvÉåÉCÉÑÅ[", "çUåÇ", transform);
                Rotation = GetAngle(transform.position, Camera.main.ScreenToWorldPoint(inputer.GetMousePosition())) + 90;
            }
            else
               if (inputer.GetPlayerPunchEnd())
            {
                animator.SetBool("Punch", false);
                RotationIsActive = false;
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