using Audio;

namespace Ui.VolumeSettingSliders
{
    interface IVolumeSettable
    {
        /// <summary>
        /// 音声データを取得する
        /// </summary>
        public VolumeData GetVolumeData();

        /// <summary>
        /// マスター音量を設定する
        /// </summary>
        /// <param name="volume">0～1までの音量</param>
        public void SetMasterVolume(float volume);

        /// <summary>
        /// BGM音量を設定する
        /// </summary>
        /// <param name="volume">0～1までの音量</param>
        public void SetBgmVolume(float volume);

        /// <summary>
        /// SE音量を設定する
        /// </summary>
        /// <param name="volume">0～1までの音量</param>
        public void SetSeVolume(float volume);

        /// <summary>
        /// 音量データを保存する
        /// </summary>
        public void SaveVolumeData();
    }
}