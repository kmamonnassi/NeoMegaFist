namespace Audio
{
    interface ICategoryAudioControllable
    {
        /// <summary>
        /// �ΏۃJ�e�S�����~���[�g����
        /// </summary>
        /// <param name="category">�~���[�g����J�e�S��</param>
        void CategoryMute(AudioCategory category);

        /// <summary>
        /// �ΏۃJ�e�S���̃~���[�g����������
        /// </summary>
        /// <param name="category">�~���[�g��������J�e�S��</param>
        void CategoryReMute(AudioCategory category);

        /// <summary>
        /// �ΏۃJ�e�S���̉���S���~�߂�
        /// </summary>
        /// <param name="category">�ΏۃJ�e�S��</param>
        /// <param name="ignoresReleaseTime">�����[�X���Ԃ𖳎����邩�ǂ���</param>
        void CategoryStop(AudioCategory category, bool ignoresReleaseTime = true);

        /// <summary>
        /// �J�e�S�����Ƃ̃T�E���h���Đ��������������ׂ�
        /// </summary>
        /// <param name="categoryNum">�J�e�S���̔ԍ�</param>
        bool CheckCategoryAudioStop(int categoryNum);

        /// <summary>
        /// BGM�ȊO�̃T�E���h���~�܂��������ׂ�
        /// </summary>
        bool CheckAudioStopWithoutBgm();

        /// <summary>
        /// ���ׂẴT�E���h���~�܂��������ׂ�
        /// </summary>
        bool CheckAllAudioStop();
    }
}