using UnityEngine;

namespace StageObject.Buff
{
    public class Burn : Buff
    {
        public override BuffID ID => BuffID.Burn;
        public override BuffType Type => BuffType.Debuff;
        public override bool TimeLimited => true;
        public override bool ShowedScreen => true;

        public const float DAMAGE_INTERVAL = 1;
        
        private float nowTime;
        private CharacterBase character;

        protected override void Start_Virtual(BuffData data, StageObjectBase target)
        {
            base.Start_Virtual(data, target);
            character = target.GetComponent<CharacterBase>();
        }

        protected override void Update_Virtual()
        {
            base.Update_Virtual();
            if (character == null) return;

            nowTime += Time.deltaTime;
            if(nowTime >= DAMAGE_INTERVAL)
            {
                nowTime = 0;
                character.Damage(data.Value);
                Debug.Log("BURN DMG");
            }
        }
    }
}