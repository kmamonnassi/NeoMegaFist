using System;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace Stage
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private PlayerInRoomChecker checker;

        [Inject] private IPostEffectCamera cam;

        public event Action OnEnterPlayer;
        public event Action OnExitPlayer;

        private void Start()
        {
            checker.OnEnterPlayer += OnEnterRoom;
            checker.OnExitPlayer += OnExitRoom;
        }

        private void OnEnterRoom()
        {
            cam.SetConfiner(checker.Confiner);
            OnEnterPlayer?.Invoke();
        }

        private void OnExitRoom()
        {
            cam.SetConfiner(null);
            OnExitPlayer?.Invoke();
        }
    }
}