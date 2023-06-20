namespace Audio
{
    public static class AudioSettingStaticData
    {
        /// <summary>
        /// CriAtomExPlayback�̃I�u�W�F�N�g�v�[���̏����T�C�Y
        /// </summary>
        public const int PLAYBACK_INIT_SIZE = 5;

        /// <summary>
        /// CriAtomExPlayback���ꎞ�I�ɕۑ�����o�b�t�@�[�T�C�Y
        /// </summary>
        public const int PLAYBACK_BUFFER_SIZE = 16;

        /// <summary>
        /// �}�X�^�[���ʂ̏����l
        /// </summary>
        public const float START_VOLUME_MASTER = 0.5f;

        /// <summary>
        /// SE���ʂ̏����l
        /// </summary>
        public const float START_VOLUME_SE = 0.5f;

        /// <summary>
        /// BGM���ʂ̏����l
        /// </summary>
        public const float START_VOLUME_BGM = 0.5f;

        public const string JSON_DIRECTORY_PATH = "/JsonText";

        /// <summary>
        /// ���ʐݒ�̕ۑ���p�X
        /// </summary>
        public const string VOLUME_SETTING_PATH = "/JsonText/VolumeSettingData.json";

        /// <summary>
        /// ���ʐݒ��Json�t�@�C����
        /// </summary>
        public const string VOLUME_SETTING_JSON_NAME = "VolumeSettingData.json";
    }
}
