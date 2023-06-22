using StageObject.Buff;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
    [RequireComponent(typeof(StageObjectBuffManager))]
    public class Player : CharacterBase
    {
        [Inject] private IUpdater updater;

        public override StageObjectID ID => StageObjectID.Player;
        public override Size DefaultSize => Size.Big;
        public override StageObjectType Type => StageObjectType.Player;

        protected override void OnAwake_Virtual()
        {
            base.OnAwake_Virtual();

            //�A�b�v�f�[�g�֐��͂ЂƂ܂Ƃ܂�ɂ��ĉ�
            foreach(IUpdate update in GetComponents<IUpdate>())
            {
                updater.AddUpdate(update);
            }
            foreach (IFixedUpdate update in GetComponents<IFixedUpdate>())
            {
                updater.AddFixedUpdate(update);
            }
        }

        protected override void OnKill_Virtual()
        {
            //�񂳂ꂽ�A�b�v�f�[�g�֐���S�č폜����
            foreach (IUpdate update in GetComponents<IUpdate>())
            {
                updater.RemoveUpdate(update);
            }
            foreach (IFixedUpdate update in GetComponents<IFixedUpdate>())
            {
                updater.RemoveFixedUpdate(update);
            }
        }
    }
}