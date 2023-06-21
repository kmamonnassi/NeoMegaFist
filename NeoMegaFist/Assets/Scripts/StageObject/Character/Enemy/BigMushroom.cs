namespace StageObject
{
    public class BigMushroom : CharacterBase
    {
        public override StageObjectID ID => StageObjectID.BigMushroom;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Big;

        protected override void OnUpdate_Virtual()
        {

        }
    }
}