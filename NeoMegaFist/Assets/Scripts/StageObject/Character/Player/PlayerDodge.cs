using InputControl;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
    public class PlayerDodge : MonoBehaviour, IPlayerMove, IUpdate, IFixedUpdate
    {
        [SerializeField] private Player player;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private AfterImage afterImage;
        [SerializeField] private ParticleSystem dodgeEffect;
        [SerializeField] private float baseDodgeSpeed;
        [SerializeField] private float baseDodgeDuration;
        [SerializeField] private float baseDodgeInterval;
        [SerializeField] private float invisibleDuration;

        [Inject] private IInputer inputer;

        public int MovePriority => 1;
        public bool MoveIsActive { get; private set; }
        public Vector2 MoveVelocity { get; private set; }

        private float dodgeDuration;
        private float dodgeInterval;

        private void Start()
        {
            playerMover.Add(this);
        }

        public void ManagedUpdate()
        {
            if(dodgeDuration == 0)
            {
                if (inputer.GetPlayerDodgeStart() && dodgeInterval == 0)
                {
                    AudioReserveManager.AudioReserve("ƒvƒŒƒCƒ„[", "‰ñ”ð", transform);
                    MoveIsActive = true;
                    dodgeDuration = baseDodgeDuration;
                    dodgeInterval = baseDodgeInterval;
                    player.Invisible(invisibleDuration);
                    player.gameObject.layer = LayerMask.NameToLayer("DodgePlayer");
                    afterImage.gameObject.SetActive(true);
                    dodgeEffect.Play();
                }
                else
                {
                    MoveIsActive = false;
                }
            }

            if (dodgeDuration > 0)
            {
                dodgeDuration -= Time.deltaTime;
                if (dodgeDuration < 0)
                {
                    dodgeDuration = 0;
                    player.gameObject.layer = LayerMask.NameToLayer("StageObject");
                    afterImage.gameObject.SetActive(false);
                    dodgeEffect.Stop();
                }
            }
            else
            if (dodgeInterval > 0 && dodgeDuration == 0)
            {
                dodgeInterval -= Time.deltaTime;
                if (dodgeInterval < 0)
                {
                    dodgeInterval = 0;
                }
            }
        }

        public void ManagedFixedUpdate()
        {
            MoveVelocity = inputer.GetPlayerMove() * baseDodgeSpeed * player.Speed;
        }
    }
}