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
        [SerializeField] private SpriteRenderer[] spriteRenderers;
        [Inject] private IUpdater updater;

        public abstract StageObjectID ID { get; }
        public abstract StageObjectType Type { get; }
        public abstract Size DefaultSize { get; }

        public Size Size { get; private set; }
        public float Speed { get; private set; } = 1;
        public bool IsKilled { get; private set; }
        public SpriteRenderer[] SpriteRenderers => spriteRenderers;

        public event Action OnKill;
        public event Action<Size> OnSetSize;
        public event Action<float> OnSetSpeed;

        protected Rigidbody2D rb { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            SetSize(DefaultSize);
            OnAwake_Virtual();
            updater.AddUpdate(this);
            updater.AddFixedUpdate(this);

            //アップデート関数はひとまとまりにして回す
            foreach (IUpdate update in GetComponents<IUpdate>())
            {
                updater.AddUpdate(update);
            }
            foreach (IFixedUpdate update in GetComponents<IFixedUpdate>())
            {
                updater.AddFixedUpdate(update);
            }
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

            //回されたアップデート関数を全て削除する
            foreach (IUpdate update in GetComponents<IUpdate>())
            {
                updater.RemoveUpdate(update);
            }
            foreach (IFixedUpdate update in GetComponents<IFixedUpdate>())
            {
                updater.RemoveFixedUpdate(update);
            }

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