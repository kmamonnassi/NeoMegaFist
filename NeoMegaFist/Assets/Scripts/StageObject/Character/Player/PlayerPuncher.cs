using UnityEngine;
using Utility;

namespace StageObject
{
    public class PlayerPuncher : MonoBehaviour, IPlayerRotate, IUpdate
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerRotater rotater;
        [SerializeField] private Player player;
        [SerializeField] private float decressSpeed = 0.8f;

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
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("Punch", true);
                IsActive = true;
                player.SetSpeed(player.Speed - decressSpeed);
            }
            else
               if (Input.GetMouseButton(0))
            {
                Rotation = GetAngle(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) + 90;
            }
            else
               if (Input.GetMouseButtonUp(0))
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