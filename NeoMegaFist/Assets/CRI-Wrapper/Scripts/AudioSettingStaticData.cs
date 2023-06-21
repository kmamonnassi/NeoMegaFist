namespace Audio
{
    public static class AudioSettingStaticData
    {
        /// <summary>
        /// CriAtomExPlaybackのオブジェクトプールの初期サイズ
        /// </summary>
        public const int PLAYBACK_INIT_SIZE = 5;

        /// <summary>
        /// CriAtomExPlaybackを一時的に保存するバッファーサイズ
        /// </summary>
        public const int PLAYBACK_BUFFER_SIZE = 16;

        /// <summary>
        /// マスター音量の初期値
        /// </summary>
        public const float START_VOLUME_MASTER = 0.5f;

        /// <summary>
        /// SE音量の初期値
        /// </summary>
        public const float START_VOLUME_SE = 0.5f;

        /// <summary>
        /// BGM音量の初期値
        /// </summary>
        public const float START_VOLUME_BGM = 0.5f;

        public const string JSON_DIRECTORY_PATH = "/JsonText";

        /// <summary>
        /// 音量設定の保存先パス
        /// </summary>
        public const string VOLUME_SETTING_PATH = "/JsonText/VolumeSettingData.json";

        /// <summary>
        /// 音量設定のJsonファイル名
        /// </summary>
        public const string VOLUME_SETTING_JSON_NAME = "VolumeSettingData.json";
    }
}
