using StageObject;
using System;
using UnityEngine;

namespace Stage
{
    public class PlayerInRoomChecker : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D confiner;

        public event Action OnEnterPlayer;
        public event Action OnExitPlayer;

        public BoxCollider2D Confiner => confiner;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Player>() != null)
            {
                OnEnterPlayer?.Invoke();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<Player>() != null)
            {
                OnEnterPlayer?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Player>() != null)
            {
                OnExitPlayer?.Invoke();
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<Player>() != null)
            {
                OnExitPlayer?.Invoke();
            }
        }
    }
}
