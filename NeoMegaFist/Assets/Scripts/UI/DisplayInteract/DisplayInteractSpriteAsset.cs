using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControl;

namespace Ui.DisplayInteract
{
    public class DisplayInteractSpriteAsset : ScriptableObject
    {
		[SerializeField, Header("�L�[�{�[�h���͎��̉摜")]
		private Sprite keyboardInteractSprite;
		public Sprite keyboardInteractSpriteProp => keyboardInteractSprite;

		[SerializeField, Header("�R���g���[���[���͎��̉摜")]
		private Sprite controllerInteractSprite;
		public Sprite controllerInteractSpriteProp => controllerInteractSprite;

		[SerializeField, Header("�\������e�L�X�g")]
		private string displayText;
		public string displayTextProp => displayText;

        /// <summary>
        /// �R���g���[���[���Ƃ̉摜��Ԃ�
        /// </summary>
        /// <param name="controllerType">�R���g���[���[�̎��</param>
        public Sprite GetKeyBindingSprite(ControllerType controllerType)
        {
            switch (controllerType)
            {
                case ControllerType.Keyboard:
                    return keyboardInteractSpriteProp;
                case ControllerType.Gamepad:
                    return controllerInteractSpriteProp;
                default:
                    return null;
            }
        }
    }
}
