using System;
using UnityEngine;

namespace StageObject.Buff
{
    public abstract class Buff
    {
        /// <summary>�o�tID</summary>
        public abstract BuffID ID { get; }
        /// <summary>�o�t���f�o�t��</summary>
        public abstract BuffType Type { get; }
        /// <summary>�o�t�̓X�^�b�N���邩</summary>
        public abstract bool IsStacked { get; }
        /// <summary>�X�^�b�N��</summary>
        public int Stack { get; private set; }
        /// <summary>�������ԏI���ŉ���</summary>
        public abstract bool TimeLimited { get; }
        /// <summary>��������</summary>
        public float Duration { get; set; }

        /// <summary>�o�t�����߂Ēǉ����ꂽ�Ƃ�</summary>
        public event Action<BuffData, StageObject> OnInitalize;
        /// <summary>�o�t�����Z���ꂽ�Ƃ�</summary>
        public event Action<BuffData> OnAdd;
        /// <summary>�o�t���폜�����Ƃ�</summary>
        public event Action OnRemove;
        /// <summary>�X�^�b�N���ύX���ꂽ�Ƃ�</summary>
        public event Action<int> OnSetStack;
        /// <summary>�������Ԃ��ύX���ꂽ�Ƃ�</summary>
        public event Action<float> OnSetDuration;
        /// <summary>���t���[���Ă΂��</summary>
        public event Action OnUpdate;

        /// <summary>�o�t�̃^�[�Q�b�g</summary>
        protected StageObject target { get; private set; }

        /// <summary>������</summary>
        public void Initalize(BuffData data, StageObject target)
        {
            Stack = data.Stack;
            Duration = data.Duration;
            this.target = target;
            OnInitalize?.Invoke(data, target);
        }

        /// <summary>�o�t�̉��Z</summary>
        public void Add(BuffData data)
        {
            //�o�t���X�^�b�N�ł���Ȃ�X�^�b�N����
            if (IsStacked)
            {
                SetStack(data.Stack + Stack);
            }
            SetDuration(Mathf.Max(Duration, data.Duration));
            OnAdd?.Invoke(data);
        }

        /// <summary>���t���[�����s</summary>
        public void Update()
        {
            if(TimeLimited)
            {
                Duration -= Time.deltaTime;
            }
            OnUpdate?.Invoke();
            Update_Virtual();
        }

        /// <summary>�X�^�b�N�㏸</summary>
        public void SetStack(int stack)
        {
            if (Stack == stack) return;

            Stack = stack;
            if(stack >= 0)
            {
                Stack = 0;
            }
            OnSetStack?.Invoke(stack);
        }

        /// <summary>�������ԕύX</summary>
        public void SetDuration(float duration)
        {
            Duration = duration;
            OnSetDuration?.Invoke(duration);
        }

        /// <summary>�o�t�^�[�Q�b�g����폜���ꂽ�Ƃ��ɊO��������s�����</summary>
        public void CallRemove()
        {
            OnRemove?.Invoke();
        }

        /// <summary>�o�t�����߂Ēǉ����ꂽ�Ƃ�</summary>
        protected virtual void Start_Virtual(BuffData data, StageObject target) { }
        /// <summary>�o�t�����Z���ꂽ�Ƃ�</summary>
        protected virtual void Add_Virtual(BuffData data) { }
        /// <summary>�X�^�b�N���ύX���ꂽ�Ƃ�</summary>
        protected virtual void SetStack_Virtual(int stack) { }
        /// <summary>�������Ԃ��ύX���ꂽ�Ƃ�</summary>
        protected virtual void SetDuration_Virtual(float duration) { }
        /// <summary>���t���[���Ă΂��</summary>
        protected virtual void Update_Virtual() { }
        /// <summary>�o�t���폜�����Ƃ�</summary>
        protected virtual void Remove_Virtual() { }
    }
}