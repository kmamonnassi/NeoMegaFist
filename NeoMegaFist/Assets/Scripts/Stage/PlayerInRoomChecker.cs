using StageObject;
using System;
using UnityEngine;
using Zenject;

namespace Stage
{
    public class PlayerInRoomChecker : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D playerConfiner;
        [SerializeField] private BoxCollider2D cameraConfiner;
        [Inject] private Player player;

        public event Action OnEnterPlayer;
        public event Action OnExitPlayer;

        public BoxCollider2D PlayerConfiner => playerConfiner;
        public BoxCollider2D CameraConfiner => cameraConfiner;

        private bool isEnter = false;

        private void Update()
        {
            float minX = playerConfiner.transform.position.x + playerConfiner.offset.x - (playerConfiner.size.x / 2);
            float maxX = playerConfiner.transform.position.x + playerConfiner.offset.x + (playerConfiner.size.x / 2);
            float minY = playerConfiner.transform.position.y + playerConfiner.offset.y - (playerConfiner.size.y / 2);
            float maxY = playerConfiner.transform.position.y + playerConfiner.offset.y + (playerConfiner.size.y / 2);
            float nowX = player.transform.position.x;
            float nowY = player.transform.position.y;

            if(isEnter)
            {
                if (minX > nowX || maxX < nowX)
                {
                    OnExitPlayer?.Invoke();
                    isEnter = false;
                }
                if (minY > nowY || maxY < nowY)
                {
                    OnExitPlayer?.Invoke();
                    isEnter = false;
                    Debug.Log("OK" + transform.parent.name);
                }
            }
            else
            {
                if (minX <= nowX && maxX >= nowX)
                {
                    if (minY <= nowY && maxY >= nowY)
                    {
                        OnEnterPlayer?.Invoke();
                        isEnter = true;
                        Debug.Log("OK2" + transform.parent.name);
                    }
                }
                
            }
        }

        //private void OnTriggerEnter2D(Collider2D col)
        //{
        //    if (col.gameObject.GetComponent<Player>() != null)
        //    {
        //        OnEnterPlayer?.Invoke();
        //    }
        //}

        //private void OnCollisionEnter2D(Collision2D col)
        //{
        //    if (col.gameObject.GetComponent<Player>() != null)
        //    {
        //        OnEnterPlayer?.Invoke();
        //    }
        //}

        //private void OnTriggerExit2D(Collider2D col)
        //{
        //    if (col.gameObject.GetComponent<Player>() != null)
        //    {
        //        OnExitPlayer?.Invoke();
        //    }
        //}

        //private void OnCollisionExit2D(Collision2D col)
        //{
        //    if (col.gameObject.GetComponent<Player>() != null)
        //    {
        //        OnExitPlayer?.Invoke();
        //    }
        //}
    }
}
