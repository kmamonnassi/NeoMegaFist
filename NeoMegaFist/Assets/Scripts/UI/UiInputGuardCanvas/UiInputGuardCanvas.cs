using UnityEngine;

namespace Ui.InputGuardCanvas
{
    public class UiInputGuardCanvas : MonoBehaviour, IInputGuardable
    {
        /// <summary>
        /// �őO�ʂɉ����`�悵�Ȃ��L�����o�X��z�u���ăN���b�N�����̂�h��
        /// </summary>
        /// <param name="enable">�L���A������</param>
        public void InputGuardEnable(bool enable)
        {
            this.gameObject.SetActive(enable);
        }
    }
}
