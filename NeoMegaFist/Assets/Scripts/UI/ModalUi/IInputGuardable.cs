namespace Ui.Modal
{
    interface IInputGuardable
    {
        /// <summary>
        /// ���[�_�����A�j���[�V���������H
        /// </summary>
        public bool isAnimationProp { get; }

        /// <summary>
        /// �N���b�N��h��Canvas��L������������
        /// </summary>
        /// <param name="enable">�L���A����</param>
        public void InputGuardEnable(bool enable);
    }
}
