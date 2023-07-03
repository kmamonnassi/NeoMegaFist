using Cysharp.Threading.Tasks;
using DG.Tweening;
using InputControl;
using UnityEngine;
using Utility;
using Utility.PostEffect;
using Zenject;

namespace StageObject
{
    public class PlayerPuncher : MonoBehaviour, IPlayerRotate, IUpdate
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerRotater rotater;
        [SerializeField] private Player player;
        [SerializeField] private float decressSpeed = 0.8f;
        [SerializeField] private float[] punchTimes;
        [SerializeField] private float nextPunchDuration;
        [SerializeField] private PlayerCatcher catcher;

        [Inject] private IInputer inputer;
        [Inject] private IPostEffectCamera cam;

        public int RotationPriority => 1;
        public float Rotation { get; private set; }
        public bool RotationIsActive { get; private set; }

        private int nowPunchId;
        private bool isPunching = false;
        private bool nextPunchPlay = false;
        private Tween punchWaitTween;
        private Tween nextPunchWaitTween;

        private void Awake()
        {
            rotater.Add(this);
        }

        public void ManagedUpdate()
        {
            if (player.IsStun || catcher.CatchTarget != null) return;

            if ((inputer.GetPlayerPunch() || nextPunchPlay) && !isPunching)
            {
                switch (nowPunchId)
                {
                    case 0:
                        Punch(1);
                        animator.Play("Punch_1");
                        AudioReserveManager.AudioReserve("プレイヤー", "攻撃_1", transform);
                        break;
                    case 1:
                        Punch(2);
                        animator.Play("Punch_2");
                        AudioReserveManager.AudioReserve("プレイヤー", "攻撃_2", transform);
                        break;
                    case 2:
                        Punch(3);
                        animator.Play("Punch_3");
                        AudioReserveManager.AudioReserve("プレイヤー", "攻撃_3", transform);
                        break;
                }
            }
        }

        private async void Punch(int id)
        {
            player.SetSpeed(player.Speed - decressSpeed);
            isPunching = true;
            RotationIsActive = true;
            nextPunchPlay = false;
            Rotation = GetAngle(transform.position, cam.ScreenToWorldPoint(inputer.GetMousePosition())) + 90;

            punchWaitTween = DOVirtual.DelayedCall(punchTimes[nowPunchId], () =>
            {
                RotationIsActive = false;
                isPunching = false;
                player.SetSpeed(player.Speed + decressSpeed);
            });
            nextPunchWaitTween?.Kill();
            nextPunchWaitTween = DOVirtual.DelayedCall(nextPunchDuration, () =>
            {
                nowPunchId = 0;
            });
            await UniTask.DelayFrame(1);
            punchWaitTween.onUpdate += () =>
            {
                if (inputer.GetPlayerPunch())
                {
                    nextPunchPlay = true;
                }
            };
            nowPunchId = id;
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