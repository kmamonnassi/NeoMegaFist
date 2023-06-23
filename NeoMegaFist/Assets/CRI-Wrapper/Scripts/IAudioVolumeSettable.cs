namespace Audio
{
    interface IAudioVolumeSettable
    {
        /// <summary>
        /// �}�X�^�[����
        /// </summary>
        public float masterVolumeProp { get; }

        /// <summary>
        /// SE����
        /// </summary>
        public float seVolumeProp { get; }

        /// <summary>
        /// BGM����
        /// </summary>
        public float bgmVolumeProp { get; }

        /// <summary>
        /// �}�X�^�[���ʂ����߂�
        /// </summary>
        /// <param name="ratio">����</param>
        void SetMasterVolume(float ratio);

        /// <summary>
        /// SE���ʂ����߂�
        /// </summary>
        /// <param name="ratio">����</param>
        void SetSeVolume(float ratio);

        /// <summary>
        /// BGM���ʂ����߂�
        /// </summary>
        /// <param name="ratio">����</param>
        void SetBgmVolume(float ratio);

        /// <summary>
        /// ���ʂ�ۑ�����
        /// </summary>
        void SaveVolumeData();

        /// <summary>
        /// ���ʂ��擾����
        /// </summary>
        VolumeData GetVolumeData();
    }
}