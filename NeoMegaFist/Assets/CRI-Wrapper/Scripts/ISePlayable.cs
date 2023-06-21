namespace Audio
{
    interface ISePlayable
    {
        /// <summary>
        /// �����Đ�����<br>�A���Đ��\</br>
        /// </summary>
        /// <param name="cueSheetName">�L���[�V�[�g��</param>
        /// <param name="cueName">�T�E���h��</param>
        /// <param name="gameObjectInstanceID">gameObject.GetInstanceID();�Ŏ���GameObject���Ƃ̌ŗL�ԍ�</param>
        public void Play(string cueSheetName, string cueName, int gameObjectInstanceID);

        /// <summary>
        /// �����~�߂�
        /// </summary>
        /// <param name="cueName">�T�E���h��</param>
        /// <param name="gameObjectInstanceID">gameObject.GetInstanceID();�Ŏ���GameObject���Ƃ̌ŗL�ԍ�</param>
        /// <param name="ignoresReleaseTime">�����[�X���Ԃ𖳎����邩�ǂ���</param>
        public void Stop(string cueName, int gameObjectInstanceID, bool ignoresReleaseTime = true);

    }
}