using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InputControl;

namespace Ui.DisplayInteract
{
    public class DisplayInteractCanvasView : MonoBehaviour
    {
        [SerializeField]
        private Image interactImage;

        [SerializeField]
        private TextMeshProUGUI interactText;

        private DisplayInteractSpriteAsset spriteAsset;
        private ControllerType controllerType = ControllerType.Keyboard;

        /// <summary>
        /// Asset���X�V����
        /// </summary>
        /// <param name="spriteAsset">Asset</param>
        public void SetAsset(DisplayInteractSpriteAsset spriteAsset)
        {
            this.spriteAsset = spriteAsset;
        }

        /// <summary>
        /// �R���g���[���[�^�C�v���X�V����
        /// </summary>
        /// <param name="controllerType">ControllerType</param>
        public void SetControllerType(ControllerType controllerType)
        {
            this.controllerType = controllerType;
        }

        /// <summary>
        /// Image��Text��ݒ肷��
        /// </summary>
        public void SetImageAndText()
        {
            interactImage.sprite = this.spriteAsset.GetKeyBindingSprite(controllerType);
            interactText.text = this.spriteAsset.displayTextProp;
        }
    }
}
