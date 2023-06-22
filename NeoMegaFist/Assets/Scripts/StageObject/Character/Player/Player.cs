using StageObject.Buff;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
    [RequireComponent(typeof(StageObjectBuffManager))]
    public class Player : CharacterBase
    {
        public override StageObjectID ID => StageObjectID.Player;
        public override Size DefaultSize => Size.Big;
        public override StageObjectType Type => StageObjectType.Player;

    }
}