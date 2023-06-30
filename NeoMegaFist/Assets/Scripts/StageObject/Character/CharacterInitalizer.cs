using UnityEngine;

namespace StageObject
{
    public class CharacterInitalizer : MonoBehaviour, ICharacterInitalizer
    {
        [SerializeField] private CharacterDamageEffect dmgEffect;

        public void Initalize(CharacterBase chara)
        {
            CharacterDamageEffect dmgEffectInstance = Instantiate(dmgEffect, chara.transform);
            dmgEffectInstance.Initalize(chara);
        }
    }
}
