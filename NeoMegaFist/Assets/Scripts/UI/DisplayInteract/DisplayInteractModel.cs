using UnityEngine;
using InputControl;
using Zenject;
using UniRx;

namespace UI.DisplayInteract
{
    public class DisplayInteractModel : MonoBehaviour, IDisplayableInteractModel
    {
        [Inject]
        private IInputer inputer;

        private ControllerType controllerType;
        private ControllerType beforeControllerType;

        [SerializeField]
        private GameObject displayInteractCanvasObj;

        public Subject<DisplayInteractSpriteAsset> onSetAsset = new Subject<DisplayInteractSpriteAsset>();
        public Subject<ControllerType> onChangeControllerType = new Subject<ControllerType>();
        public Subject<Unit> onSetSpriteAndImage = new Subject<Unit>();

        void Start()
        {
            displayInteractCanvasObj.SetActive(false);

            controllerType = inputer.GetControllerType();
            beforeControllerType = controllerType;

            onChangeControllerType.OnNext(controllerType);
        }

        void Update()
        {
            controllerType = inputer.GetControllerType();
            if (controllerType != beforeControllerType)
            {
                onChangeControllerType.OnNext(controllerType);
                onSetSpriteAndImage.OnNext(default);
                beforeControllerType = controllerType;
            }
        }

        void IDisplayableInteractModel.ShowUI(DisplayInteractSpriteAsset spriteAsset, Vector2 pos)
        {
            onSetAsset.OnNext(spriteAsset);
            onSetSpriteAndImage.OnNext(default);
            displayInteractCanvasObj.SetActive(true);
            displayInteractCanvasObj.transform.position = pos;
        }

        void IDisplayableInteractModel.HideUI()
        {
            displayInteractCanvasObj.SetActive(false);
        }

        void IDisplayableInteractModel.SetPos(Vector2 pos)
        {
            displayInteractCanvasObj.transform.position = pos;
        }
    }
}
