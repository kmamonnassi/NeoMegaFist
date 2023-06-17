using System;
using UnityEngine;

namespace StageObject.Buff
{
    public abstract class Buff
    {
        /// <summary>バフID</summary>
        public abstract BuffID ID { get; }
        /// <summary>バフかデバフか</summary>
        public abstract BuffType Type { get; }
        /// <summary>バフはスタックするか</summary>
        public abstract bool IsStacked { get; }
        /// <summary>スタック数</summary>
        public int Stack { get; private set; }
        /// <summary>持続時間終了で解除</summary>
        public abstract bool TimeLimited { get; }
        /// <summary>持続時間</summary>
        public float Duration { get; set; }

        /// <summary>バフが初めて追加されたとき</summary>
        public event Action<BuffData, StageObject> OnInitalize;
        /// <summary>バフが加算されたとき</summary>
        public event Action<BuffData> OnAdd;
        /// <summary>バフが削除されるとき</summary>
        public event Action OnRemove;
        /// <summary>スタックが変更されたとき</summary>
        public event Action<int> OnSetStack;
        /// <summary>持続時間が変更されたとき</summary>
        public event Action<float> OnSetDuration;
        /// <summary>毎フレーム呼ばれる</summary>
        public event Action OnUpdate;

        /// <summary>バフのターゲット</summary>
        protected StageObject target { get; private set; }

        /// <summary>初期化</summary>
        public void Initalize(BuffData data, StageObject target)
        {
            Stack = data.Stack;
            Duration = data.Duration;
            this.target = target;
            OnInitalize?.Invoke(data, target);
        }

        /// <summary>バフの加算</summary>
        public void Add(BuffData data)
        {
            //バフがスタックできるならスタックする
            if (IsStacked)
            {
                SetStack(data.Stack + Stack);
            }
            SetDuration(Mathf.Max(Duration, data.Duration));
            OnAdd?.Invoke(data);
        }

        /// <summary>毎フレーム実行</summary>
        public void Update()
        {
            if(TimeLimited)
            {
                Duration -= Time.deltaTime;
            }
            OnUpdate?.Invoke();
            Update_Virtual();
        }

        /// <summary>スタック上昇</summary>
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

        /// <summary>持続時間変更</summary>
        public void SetDuration(float duration)
        {
            Duration = duration;
            OnSetDuration?.Invoke(duration);
        }

        /// <summary>バフターゲットから削除されたときに外部から実行される</summary>
        public void CallRemove()
        {
            OnRemove?.Invoke();
        }

        /// <summary>バフが初めて追加されたとき</summary>
        protected virtual void Start_Virtual(BuffData data, StageObject target) { }
        /// <summary>バフが加算されたとき</summary>
        protected virtual void Add_Virtual(BuffData data) { }
        /// <summary>スタックが変更されたとき</summary>
        protected virtual void SetStack_Virtual(int stack) { }
        /// <summary>持続時間が変更されたとき</summary>
        protected virtual void SetDuration_Virtual(float duration) { }
        /// <summary>毎フレーム呼ばれる</summary>
        protected virtual void Update_Virtual() { }
        /// <summary>バフが削除されるとき</summary>
        protected virtual void Remove_Virtual() { }
    }
}