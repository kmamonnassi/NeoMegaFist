using StageObject.Buff;
using System;
using UnityEngine;

namespace StageObject
{
    [RequireComponent(typeof(StageObjectBuffManager))]
    public abstract class StageObject : MonoBehaviour
    {
        public event Action OnKilled;
        public event Action OnStart;
        public event Action OnUpdate;

        private void Start()
        {
            OnStart?.Invoke();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        public void Kill()
        {
            OnKilled?.Invoke();
            Destroy(gameObject);
        }
    }
}