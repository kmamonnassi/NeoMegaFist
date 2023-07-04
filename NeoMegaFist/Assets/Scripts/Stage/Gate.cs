using StageObject;
using UnityEngine;
using Zenject;

namespace Stage
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;
        [SerializeField] private Vector2 playerPosition;

        [Inject] private Player player;

        private void OnTriggerEnter2D(Collider2D col)
        {
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
        }
    }
}
