using UnityEngine;

namespace Ui.DisplayInteract
{
    interface IDisplayableInteractModel
    {
        /// <summary>
        /// �C���^���N�g�{�^����UI��\������
        /// </summary>
        /// <param name="spriteAsset">�\������{�^����Asset</param>
        /// <param name="pos">�\���ʒu</param>
        public void ShowUI(DisplayInteractSpriteAsset spriteAsset, Vector2 pos);
        
        /// <summary>
        /// UI���\���ɂ���
        /// </summary>
        public void HideUI();

        /// <summary>
        /// �\���ʒu��ύX����
        /// </summary>
        /// <param name="pos">�\���ʒu</param>
        public void SetPos(Vector2 pos);
    }
}