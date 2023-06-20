using UnityEngine;
using Zenject;

namespace StageObject
{
    public class CharacterStatusUIInstaller : MonoInstaller
    {
        [SerializeField] private CharacterStatusUICreator creator;

        public override void InstallBindings()
        {
            Container.Bind<ICharacterStatusUIManager>().FromInstance(creator);
        }
    }
}
