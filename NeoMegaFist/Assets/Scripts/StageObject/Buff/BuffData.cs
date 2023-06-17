namespace StageObject.Buff
{
    public class BuffData
    {
        public readonly BuffID ID;
        public readonly int Stack;
        public readonly float Duration;

        public BuffData(BuffID iD, int stack, float duration)
        {
            ID = iD;
            Stack = stack;
            Duration = duration;
        }

        public BuffData(BuffID iD, int stack)
        {
            ID = iD;
            Stack = stack;
        }

        public BuffData(BuffID iD, float duration)
        {
            ID = iD;
            Duration = duration;
        }

        public BuffData(BuffID iD)
        {
            ID = iD;
        }
    }
}