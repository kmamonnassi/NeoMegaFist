namespace StageObject.Buff
{
    public class Freeze : Buff
    {
        public override BuffID ID => BuffID.Freeze;
        public override BuffType Type => BuffType.Debuff;
        public override bool TimeLimited => true;
        public override bool ShowedScreen => true;

        public const float DAMAGE_INTERVAL = 1;
        
        protected override void Start_Virtual(BuffData data, StageObjectBase target)
        {
            base.Start_Virtual(data, target);
            target.SetSpeed(target.Speed - (data.Value * 0.01f));
        }

        protected override void Remove_Virtual()
        {
            base.Remove_Virtual();
            target.SetSpeed(target.Speed + (data.Value * 0.01f));
        }
    }
}