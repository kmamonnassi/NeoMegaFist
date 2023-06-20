using StageObject.Buff;
using System;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
    [RequireComponent(typeof(StageObjectBuffManager), typeof(Rigidbody2D), typeof(StageObjectCatchAndThrow))]
    public abstract class StageObjectBase : MonoBehaviour, IUpdate, IFixedUpdate
    {
        [Inject] private IUpdater updater;

        public abstract StageObjectID ID { get; }
        public abstract StageObjectType Type { get; }
        public abstract Size DefaultSize { get; }

        public Size Size { get; private set; }
        public float Speed { get; private set; } = 1;
        public bool IsKilled { get; private set; }

        public event Action OnKill;
        public event Action<Size> OnSetSize;
        public event Action<float> OnSetSpeed;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            SetSize(DefaultSize);
            OnAwake_Virtual();
            updater.AddUpdate(this);
            updater.AddFixedUpdate(this);
        }

        void IUpdate.ManagedUpdate()
        {
            OnUpdate_Virtual();
        }

        void IFixedUpdate.ManagedFixedUpdate()
        {
            OnFixedUpdate_Virtual();
        }

        /// <summary>このオブジェクトを破棄する</summary>
        public void Kill()
        {
            IsKilled = true;
            updater.RemoveUpdate(this);
            updater.RemoveFixedUpdate(this);

            Destroy(gameObject);
            OnKill_Virtual();
            OnKill?.Invoke();
        }

        /// <summary>サイズ変更</summary>
        public void SetSize(Size size)
        {
            Size = size;
            OnSetSize?.Invoke(size);
        }

        /// <summary>速度変更</summary>
        public void SetSpeed(float speed)
        {
            Speed = speed;
            OnSetSpeed?.Invoke(speed);
        }

        /// <summary>ノックバック</summary>
        public void KnockBack(Vector2 dir, float power)
        {
            rb.AddForce(dir * power, ForceMode2D.Impulse);
        }

        protected virtual void OnAwake_Virtual() { }
        protected virtual void OnUpdate_Virtual() { }
        protected virtual void OnFixedUpdate_Virtual() { }
        protected virtual void OnKill_Virtual() { }
    }
}