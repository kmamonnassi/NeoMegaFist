using System;
using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace Stage
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private PlayerInRoomChecker checker;
        [Header("カメラに追従するオブジェクトを指定できるよ。nullでプレイヤー追従")]
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private GameObject stageObjects;

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
            stageObjects.SetActive(true);
            fixedFollowTarget.SetConfiner(checker.CameraConfiner);
            fixedFollowTarget.SetTarget(cameraTarget);
            OnEnterPlayer?.Invoke();
        }

        private void OnExitRoom()
        {
            stageObjects.SetActive(false);
            OnExitPlayer?.Invoke();
        }
    }
}