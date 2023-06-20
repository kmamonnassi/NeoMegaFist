namespace StageObject.Buff
{
    public interface IStageObjectBuffManager
    {
        /// <summary>バフ追加</summary>
        void Add(BuffData data);

        /// <summary>バフの削除</summary>
        void Remove(BuffID id);
    }
}