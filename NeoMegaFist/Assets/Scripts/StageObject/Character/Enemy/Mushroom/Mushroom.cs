namespace StageObject
{
    public class Mushroom : CharacterBase
    {
        public override StageObjectID ID => StageObjectID.Mushroom;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Small;
    }
}