using UnityEngine;
using Zenject;

namespace StageObject
{
    public class TestEnemy_2 : CharacterBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float shotInterval;

        public override StageObjectID ID => StageObjectID.Mushroom;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Small;

        [Inject] private Player player;

        protected override void OnUpdate_Virtual()
        {
            base.OnUpdate_Virtual();
        }
    }
}