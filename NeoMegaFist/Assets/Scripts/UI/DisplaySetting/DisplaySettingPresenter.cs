using Zenject;
using UniRx;

namespace Ui.DisplaySetting
{
    public class DisplaySettingPresenter : IInitializable
    {
        [Inject]
        private DisplaySettingView view;

        [Inject]
        private DisplaySettingModel model;

        public void Initialize()
        {
            view.bloomEnableValueProp.Skip(1)
                .Subscribe(value => model.SetBloomEnable(value));

            model.bloomSetHandler.Subscribe(value => view.SetAllBloomSettingData(value));
        }
    }
}
