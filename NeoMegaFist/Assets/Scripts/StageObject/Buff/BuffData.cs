namespace StageObject.Buff
{
    public class BuffData
    {
        public readonly BuffID ID;
        public readonly int Stack;
        public readonly float Duration;
        public readonly int Value;

        public BuffData(BuffID iD, int stack, float duration, int value)
        {
            ID = iD;
            Stack = stack;
            Duration = duration;
            Value = value;
        }
    }
}