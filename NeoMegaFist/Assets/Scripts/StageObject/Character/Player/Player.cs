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

            //アップデート関数はひとまとまりにして回す
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
            //回されたアップデート関数を全て削除する
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