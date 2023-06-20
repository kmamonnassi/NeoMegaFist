namespace StageObject.Buff
{
    public interface IBuffDB
    {
        /// <summary>バフIDからバフを作成</summary>
        public Buff Create(BuffID id);
    }
}