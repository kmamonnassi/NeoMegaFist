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
        /// Bloom‚Ìİ’è‚·‚é‚×‚«İ’è‚ğæ“¾‚·‚é
        /// </summary>
        public BloomSettingData GetBloomSettingData()
        {
            return bloomSetting.GetBloomSettingData();
        }

        /// <summary>
        /// Bloom‚Ì—LŒø–³Œø‚ğİ’è‚·‚é
        /// </summary>
        /// <param name="enable">—LŒø–³Œø</param>
        public void SetBloomEnable(bool enable)
        {
            bloomSetting.SetBloomEnable(enable);
        }

        /// <summary>
        /// PPS‚Ìİ’è€–Ú‚ğ‚·‚×‚Ä•Û‘¶‚·‚é
        /// </summary>
        public void SavePpsData()
        {
            ppsVolumeSave.SavePostProcessingVolumeData();
        }
    }
}
