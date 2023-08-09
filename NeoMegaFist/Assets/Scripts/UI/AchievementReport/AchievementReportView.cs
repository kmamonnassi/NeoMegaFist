using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ui.AchievementReport
{
    public class AchievementReportView : MonoBehaviour
    {
        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private TextMeshProUGUI titleText;

        [SerializeField]
        private TextMeshProUGUI explanationText;

        /// <summary>
        /// 文字や画像を初期化する
        /// </summary>
        /// <param name="iconSprite">アイコン画像</param>
        /// <param name="titleStr">タイトル名</param>
        /// <param name="explanationStr">文章</param>
        public void Init(Sprite iconSprite, string titleStr, string explanationStr)
        {
            iconImage.sprite = iconSprite;
            titleText.text = titleStr;
            explanationText.text = explanationStr;
        }

        /// <summary>
        /// 自信のオブジェクトを破棄する
        /// </summary>
        public void ObjDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}
