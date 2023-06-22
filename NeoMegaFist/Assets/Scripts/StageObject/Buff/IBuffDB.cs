namespace StageObject.Buff
{
    public interface IBuffDB
    {
        /// <summary>バフIDからバフを作成</summary>
        public BuffBase Create(BuffID id);
    }
}