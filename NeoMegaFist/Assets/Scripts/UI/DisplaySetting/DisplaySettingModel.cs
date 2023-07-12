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
        /// Bloom�̐ݒ肷��ׂ��ݒ���擾����
        /// </summary>
        public BloomSettingData GetBloomSettingData()
        {
            return bloomSetting.GetBloomSettingData();
        }

        /// <summary>
        /// Bloom�̗L��������ݒ肷��
        /// </summary>
        /// <param name="enable">�L������</param>
        public void SetBloomEnable(bool enable)
        {
            bloomSetting.SetBloomEnable(enable);
        }

        /// <summary>
        /// PPS�̐ݒ荀�ڂ����ׂĕۑ�����
        /// </summary>
        public void SavePpsData()
        {
            ppsVolumeSave.SavePostProcessingVolumeData();
        }
    }
}
