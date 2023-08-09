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
        /// ������摜������������
        /// </summary>
        /// <param name="iconSprite">�A�C�R���摜</param>
        /// <param name="titleStr">�^�C�g����</param>
        /// <param name="explanationStr">����</param>
        public void Init(Sprite iconSprite, string titleStr, string explanationStr)
        {
            iconImage.sprite = iconSprite;
            titleText.text = titleStr;
            explanationText.text = explanationStr;
        }

        /// <summary>
        /// ���M�̃I�u�W�F�N�g��j������
        /// </summary>
        public void ObjDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}
