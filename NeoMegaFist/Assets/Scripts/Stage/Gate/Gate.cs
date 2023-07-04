using StageObject;
using System;
using UnityEngine;
using Zenject;

namespace Stage
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private Collider2D col;

        [Inject] private Player player;

        public event Action OnOpen;

        public void Open()
        {
            Physics2D.IgnoreCollision(col, player.GetComponent<Collider2D>());
            OnOpen?.Invoke();
        }
    }
}
