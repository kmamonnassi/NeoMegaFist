namespace Audio
{
    interface IAudioVolumeSettable
    {
        /// <summary>
        /// マスター音量
        /// </summary>
        public float masterVolumeProp { get; }

        /// <summary>
        /// SE音量
        /// </summary>
        public float seVolumeProp { get; }

        /// <summary>
        /// BGM音量
        /// </summary>
        public float bgmVolumeProp { get; }

        /// <summary>
        /// マスター音量を決める
        /// </summary>
        /// <param name="ratio">割合</param>
        void SetMasterVolume(float ratio);

        /// <summary>
        /// SE音量を決める
        /// </summary>
        /// <param name="ratio">割合</param>
        void SetSeVolume(float ratio);

        /// <summary>
        /// BGM音量を決める
        /// </summary>
        /// <param name="ratio">割合</param>
        void SetBgmVolume(float ratio);

        /// <summary>
        /// 音量を保存する
        /// </summary>
        void SaveVolumeData();

        /// <summary>
        /// 音量を取得する
        /// </summary>
        VolumeData GetVolumeData();
    }
}