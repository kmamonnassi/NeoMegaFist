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

            //�A�b�v�f�[�g�֐��͂ЂƂ܂Ƃ܂�ɂ��ĉ�
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

        /// <summary>���̃I�u�W�F�N�g��j������</summary>
        public void Kill()
        {
            IsKilled = true;
            updater.RemoveUpdate(this);
            updater.RemoveFixedUpdate(this);

            //�񂳂ꂽ�A�b�v�f�[�g�֐���S�č폜����
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

        /// <summary>�T�C�Y�ύX</summary>
        public void SetSize(Size size)
        {
            Size = size;
            OnSetSize?.Invoke(size);
        }

        /// <summary>���x�ύX</summary>
        public void SetSpeed(float speed)
        {
            Speed = speed;
            OnSetSpeed?.Invoke(speed);
        }

        /// <summary>�m�b�N�o�b�N</summary>
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