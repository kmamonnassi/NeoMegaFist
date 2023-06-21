using System;
using UnityEngine;

namespace StageObject.Buff
{
    public abstract class BuffBase
    {
        /// <summary>�o�tID</summary>
        public abstract BuffID ID { get; }
        /// <summary>�o�t���f�o�t��</summary>
        public abstract BuffType Type { get; }
        /// <summary>�������ԏI���ŉ���</summary>
        public abstract bool TimeLimited { get; }
        /// <summary>������������</summary>
        public float StartDuration { get; set; }
        /// <summary>��������</summary>
        public float Duration { get; set; }
        /// <summary>�Q�[����ʂɎʂ����ǂ���</summary>
        public abstract bool ShowedScreen { get; }

        /// <summary>�o�t�����߂Ēǉ����ꂽ�Ƃ�</summary>
        public event Action<BuffData, StageObjectBase> OnInitalize;
        /// <summary>�o�t���폜�����Ƃ�</summary>
        public event Action OnRemove;
        /// <summary>�������Ԃ��ύX���ꂽ�Ƃ�</summary>
        public event Action<float> OnSetDuration;
        /// <summary>���t���[���Ă΂��</summary>
        public event Action OnUpdate;

        /// <summary>�o�t�̃f�[�^</summary>
        protected BuffData data { get; private set; }
        /// <summary>�o�t�̃^�[�Q�b�g</summary>
        protected StageObjectBase target { get; private set; }

        /// <summary>������</summary>
        public void Initalize(BuffData data, StageObjectBase target)
        {
            StartDuration = data.Duration;
            Duration = StartDuration;
            this.target = target;
            this.data = data;
            OnInitalize?.Invoke(data, target);
            Start_Virtual(data, target);
        }

        /// <summary>���t���[�����s</summary>
        public void Update()
        {
            if(TimeLimited)
            {
                SetDuration(Duration - Time.deltaTime);
            }
            OnUpdate?.Invoke();
            Update_Virtual();
        }

        /// <summary>�������ԕύX</summary>
        public void SetDuration(float duration)
        {
            Duration = duration;
            OnSetDuration?.Invoke(duration);
            SetDuration_Virtual(duration);
        }

        /// <summary>�o�t�^�[�Q�b�g����폜���ꂽ�Ƃ��ɊO��������s�����</summary>
        public void CallRemove()
        {
            OnRemove?.Invoke();
            Remove_Virtual();
        }

        /// <summary>�o�t�����߂Ēǉ����ꂽ�Ƃ�</summary>
        protected virtual void Start_Virtual(BuffData data, StageObjectBase target) { }
        /// <summary>�������Ԃ��ύX���ꂽ�Ƃ�</summary>
        protected virtual void SetDuration_Virtual(float duration) { }
        /// <summary>���t���[���Ă΂��</summary>
        protected virtual void Update_Virtual() { }
        /// <summary>�o�t���폜�����Ƃ�</summary>
        protected virtual void Remove_Virtual() { }
    }
}