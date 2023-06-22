namespace Audio
{
    interface IBgmPlayable
    {
        /// <summary>
        /// �V�[���ōŏ���BGM���Đ�����
        /// </summary>
        /// <param name="cueSheetName">�L���[�V�[�g��</param>
        /// <param name="cueName">�T�E���h��</param>
        void SceneLoadToPlay(string cueSheetName, string cueName);

        /// <summary>
        /// BGM���Đ�����
        /// </summary>
        /// <param name="cueSheetName">�L���[�V�[�g��</param>
        /// <param name="cueName">�T�E���h��</param>
        void Play(string cueSheetName, string cueName);

        /// <summary>
        /// BGM���~�߂�
        /// </summary>
        void Stop();
    }
}
