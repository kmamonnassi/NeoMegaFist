using Audio;

namespace Ui.VolumeSettingSliders
{
    interface IVolumeSettable
    {
        /// <summary>
        /// �����f�[�^���擾����
        /// </summary>
        public VolumeData GetVolumeData();

        /// <summary>
        /// �}�X�^�[���ʂ�ݒ肷��
        /// </summary>
        /// <param name="volume">0�`1�܂ł̉���</param>
        public void SetMasterVolume(float volume);

        /// <summary>
        /// BGM���ʂ�ݒ肷��
        /// </summary>
        /// <param name="volume">0�`1�܂ł̉���</param>
        public void SetBgmVolume(float volume);

        /// <summary>
        /// SE���ʂ�ݒ肷��
        /// </summary>
        /// <param name="volume">0�`1�܂ł̉���</param>
        public void SetSeVolume(float volume);

        /// <summary>
        /// ���ʃf�[�^��ۑ�����
        /// </summary>
        public void SaveVolumeData();
    }
}