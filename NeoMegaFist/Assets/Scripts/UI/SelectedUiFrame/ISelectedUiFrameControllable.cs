using UnityEngine;
using UnityEngine.UI;

namespace Ui.SelectedUiFrame
{
    interface ISelectedUiFrameControllable
    {
        /// <summary>
        /// �I�𒆂̘g���ړ������A�K�؂ȃT�C�Y�Ɏ�����������
        /// </summary>
        /// <param name="anchorPreset">�A���J�[�̃v���Z�b�g</param>
        /// <param name="pos">�ʒu</param>
        /// <param name="anchorMax">anchorMax</param>
        /// <param name="anchorMin">anchorMin</param>
        /// <param name="size">�傫��</param>
        /// <param name="root">�e��Transform</param>
        public void SetFramePos(AnchorPresets anchorPreset, Vector2 pos, Vector2 anchorMax, Vector2 anchorMin, Vector2 size, Transform root);

        /// <summary>
        /// �A���J�[��ς���
        /// </summary>
        /// <param name="anchorMax">anchorMax</param>
        /// <param name="anchorMin">anchorMin</param>
        public void ChangeAnchor(Vector2 anchorMax, Vector2 anchorMin);
        
        /// <summary>
        /// �g��\�����邩
        /// </summary>
        /// <param name="enable">�\���A��\��</param>
        public void FrameEnable(bool enable);

        ///// <summary>
        ///// �ŏ��ɑI��ł��ԂɂȂ��Ă���UI��ς���
        ///// </summary>
        ///// <param name="selectableUi">�I���\��UI</param>
        //public void SetFirstSelectedUi(Selectable selectableUi);

        /// <summary>
        /// �g�̐e�I�u�W�F�N�g�����Z�b�g����
        /// </summary>
        public void ResetFrameParent();

    }
}