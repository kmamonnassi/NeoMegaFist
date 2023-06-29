using System;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace Stage
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private PlayerInRoomChecker checker;

        [Inject] private ICameraFollowTarget followTarget;

        public event Action OnEnterPlayer;
        public event Action OnExitPlayer;

        private void Start()
        {
            checker.OnEnterPlayer += OnEnterRoom;
            checker.OnExitPlayer += OnExitRoom;
        }

        private void OnEnterRoom()
        {
            followTarget.SetConfiner(checker.Confiner);
            OnEnterPlayer?.Invoke();
        }

        private void OnExitRoom()
        {
            OnExitPlayer?.Invoke();
        }
    }
}