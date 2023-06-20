using UnityEngine;

namespace StageObject
{
    public class CharacterStatusUICreator : MonoBehaviour, ICharacterStatusUIManager
    {
        [SerializeField] private CharacterStatusUI prefab;

        public void Create(CharacterBase target)
        {
            CharacterStatusUI ui = Instantiate(prefab, transform);
            ui.Initalize(target);
        }
    }
}