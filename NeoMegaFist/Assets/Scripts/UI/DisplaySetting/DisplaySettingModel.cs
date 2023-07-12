using Zenject;
using PostProcessingVolume;
using UniRx;

namespace Ui.DisplaySetting
{
    public class DisplaySettingModel : IInitializable
    {
        [Inject]
        private BloomSetting bloomSetting;

        [Inject]
        private IPostProcessingVolumeSavable ppsVolumeSave;

        public Subject<BloomSettingData> bloomSetHandler = new Subject<BloomSettingData>();

        public void Initialize()
        {
            bloomSetHandler.OnNext(GetBloomSettingData());
        }

        /// <summary>
        /// Bloomの設定するべき設定を取得する
        /// </summary>
        public BloomSettingData GetBloomSettingData()
        {
            return bloomSetting.GetBloomSettingData();
        }

        /// <summary>
        /// Bloomの有効無効を設定する
        /// </summary>
        /// <param name="enable">有効無効</param>
        public void SetBloomEnable(bool enable)
        {
            bloomSetting.SetBloomEnable(enable);
        }

        /// <summary>
        /// PPSの設定項目をすべて保存する
        /// </summary>
        public void SavePpsData()
        {
            ppsVolumeSave.SavePostProcessingVolumeData();
        }
    }
}
