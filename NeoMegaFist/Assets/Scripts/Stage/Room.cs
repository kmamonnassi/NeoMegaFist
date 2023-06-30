using System;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace Stage
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private PlayerInRoomChecker checker;
        [Tooltip("カメラに追従するオブジェクトを指定できるよ。nullでプレイヤー追従")]
        [SerializeField] private Transform cameraTarget;

        [Inject] private ICameraFollowTarget fixedFollowTarget;

        public event Action OnEnterPlayer;
        public event Action OnExitPlayer;

        private void Start()
        {
            checker.OnEnterPlayer += OnEnterRoom;
            checker.OnExitPlayer += OnExitRoom;
        }

        private void OnEnterRoom()
        {
            fixedFollowTarget.SetConfiner(checker.Confiner);
            fixedFollowTarget.SetTarget(cameraTarget);
            OnEnterPlayer?.Invoke();
        }

        private void OnExitRoom()
        {
            OnExitPlayer?.Invoke();
        }
    }
}