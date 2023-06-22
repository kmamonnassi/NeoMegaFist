using System;
using UnityEngine;

namespace StageObject.Buff
{
    public abstract class BuffBase
    {
        /// <summary>バフID</summary>
        public abstract BuffID ID { get; }
        /// <summary>バフかデバフか</summary>
        public abstract BuffType Type { get; }
        /// <summary>持続時間終了で解除</summary>
        public abstract bool TimeLimited { get; }
        /// <summary>初期持続時間</summary>
        public float StartDuration { get; set; }
        /// <summary>持続時間</summary>
        public float Duration { get; set; }
        /// <summary>ゲーム画面に写すかどうか</summary>
        public abstract bool ShowedScreen { get; }

        /// <summary>バフが初めて追加されたとき</summary>
        public event Action<BuffData, StageObjectBase> OnInitalize;
        /// <summary>バフが削除されるとき</summary>
        public event Action OnRemove;
        /// <summary>持続時間が変更されたとき</summary>
        public event Action<float> OnSetDuration;
        /// <summary>毎フレーム呼ばれる</summary>
        public event Action OnUpdate;

        /// <summary>バフのデータ</summary>
        protected BuffData data { get; private set; }
        /// <summary>バフのターゲット</summary>
        protected StageObjectBase target { get; private set; }

        /// <summary>初期化</summary>
        public void Initalize(BuffData data, StageObjectBase target)
        {
            StartDuration = data.Duration;
            Duration = StartDuration;
            this.target = target;
            this.data = data;
            OnInitalize?.Invoke(data, target);
            Start_Virtual(data, target);
        }

        /// <summary>毎フレーム実行</summary>
        public void Update()
        {
            if(TimeLimited)
            {
                SetDuration(Duration - Time.deltaTime);
            }
            OnUpdate?.Invoke();
            Update_Virtual();
        }

        /// <summary>持続時間変更</summary>
        public void SetDuration(float duration)
        {
            Duration = duration;
            OnSetDuration?.Invoke(duration);
            SetDuration_Virtual(duration);
        }

        /// <summary>バフターゲットから削除されたときに外部から実行される</summary>
        public void CallRemove()
        {
            OnRemove?.Invoke();
            Remove_Virtual();
        }

        /// <summary>バフが初めて追加されたとき</summary>
        protected virtual void Start_Virtual(BuffData data, StageObjectBase target) { }
        /// <summary>持続時間が変更されたとき</summary>
        protected virtual void SetDuration_Virtual(float duration) { }
        /// <summary>毎フレーム呼ばれる</summary>
        protected virtual void Update_Virtual() { }
        /// <summary>バフが削除されるとき</summary>
        protected virtual void Remove_Virtual() { }
    }
}