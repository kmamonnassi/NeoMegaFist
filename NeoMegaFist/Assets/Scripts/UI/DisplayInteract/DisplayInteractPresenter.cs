using Zenject;
using UniRx;

namespace Ui.DisplayInteract
{
    public class DisplayInteractPresenter : IInitializable
    {
        [Inject]
        private DisplayInteractModel model;

        [Inject]
        private DisplayInteractCanvasView canvasView;

        public void Initialize()
        {
            model.onSetAsset.Subscribe(asset => canvasView.SetAsset(asset));
            model.onChangeControllerType.Subscribe(type => canvasView.SetControllerType(type));
            model.onSetSpriteAndImage.Subscribe(_ => canvasView.SetImageAndText());
        }
    }
}
