using UnityEngine;
using UnityEngine.UI;

namespace Ui.SelectedUiFrame
{
    interface ISelectedUiFrameControllable
    {
        /// <summary>
        /// �I�𒆂̘g���ړ������A�K�؂ȃT�C�Y�Ɏ�����������
        /// </summary>
        /// <param name="pos">�ʒu</param>
        /// <param name="size">�傫��</param>
        /// <param name="root">�e��Transform</param>
        public void SetFramePos(Vector2 pos, Vector2 size, Transform root);
        
        /// <summary>
        /// �g��\�����邩
        /// </summary>
        /// <param name="enable">�\���A��\��</param>
        public void FrameEnable(bool enable);

        /// <summary>
        /// �ŏ��ɑI��ł��ԂɂȂ��Ă���UI��ς���
        /// </summary>
        /// <param name="selectableUi">�I���\��UI</param>
        public void SetFirstSelectedUi(Selectable selectableUi);
    }
}