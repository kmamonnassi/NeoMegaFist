using StageObject;
using UnityEngine;
using Zenject;

namespace Stage
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;
        [SerializeField] private Vector2 playerPosition;

        private void OnTriggerEnter2D(Collider2D col)
        {
            GoToNextRoom(col.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            GoToNextRoom(col.gameObject);
        }

        private bool GoToNextRoom(GameObject target)
        {
            if(target.GetComponent<Player>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
