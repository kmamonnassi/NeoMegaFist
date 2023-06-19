using StageObject.Buff;
using System;
using UnityEngine;

namespace StageObject
{
    [RequireComponent(typeof(StageObjectBuffManager))]
    public abstract class StageObjectBase : MonoBehaviour
    {
        public abstract StageObjectID ID { get; }
        public abstract StageObjectType Type { get; }
        public abstract Size DefaultSize { get; }
        public float Speed { get; private set; } = 1;

        public Size Size { get; private set; }

        public event Action OnKill;
        public event Action<Size> OnSetSize;
        public event Action<float> OnSetSpeed;

        private void Awake()
        {
            SetSize(DefaultSize);
            OnAwake_Virtual();
        }

        private void Update()
        {
            OnUpdate_Virtual();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate_Virtual();
        }

        public void Kill()
        {
            Destroy(gameObject);
            OnKill_Virtual();
            OnKill?.Invoke();
        }

        public void SetSize(Size size)
        {
            Size = size;
            OnSetSize?.Invoke(size);
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
            OnSetSpeed?.Invoke(speed);
        }

        protected virtual void OnAwake_Virtual() { }
        protected virtual void OnUpdate_Virtual() { }
        protected virtual void OnFixedUpdate_Virtual() { }
        protected virtual void OnKill_Virtual() { }
    }
}