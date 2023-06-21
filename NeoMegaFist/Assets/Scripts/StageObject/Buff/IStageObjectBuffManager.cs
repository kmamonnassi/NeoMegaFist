using System;

namespace StageObject.Buff
{
    public interface IStageObjectBuffManager
    {
        event Action<BuffBase> OnAdd;
        event Action<BuffBase> OnRemove;

        /// <summary>バフ追加</summary>
        void Add(BuffData data);
        /// <summary>バフの削除</summary>
        void Remove(BuffID id);
    }
}