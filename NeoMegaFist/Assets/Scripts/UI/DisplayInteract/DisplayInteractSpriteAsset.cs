using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControl;

namespace Ui.DisplayInteract
{
    public class DisplayInteractSpriteAsset : ScriptableObject
    {
		[SerializeField, Header("キーボード入力時の画像")]
		private Sprite keyboardInteractSprite;
		public Sprite keyboardInteractSpriteProp => keyboardInteractSprite;

		[SerializeField, Header("コントローラー入力時の画像")]
		private Sprite controllerInteractSprite;
		public Sprite controllerInteractSpriteProp => controllerInteractSprite;

		[SerializeField, Header("表示するテキスト")]
		private string displayText;
		public string displayTextProp => displayText;

        /// <summary>
        /// コントローラーごとの画像を返す
        /// </summary>
        /// <param name="controllerType">コントローラーの種類</param>
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
