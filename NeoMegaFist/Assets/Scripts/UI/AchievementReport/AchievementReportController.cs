using System.Collections.Generic;
using UnityEngine;

namespace Ui.AchievementReport
{
    public class AchievementReportController : MonoBehaviour
    {
        [SerializeField]
        private GameObject achievementReportObj;

        // �\���\��̎���
        private List<AchievementInfo> achievementInfoList = new List<AchievementInfo>();

        GameObject makedAchievementReportObj = null;

        /// <summary>
        /// ���щ���UI��\������
        /// </summary>
        /// <param name="iconSprite">�A�C�R���摜</param>
        /// <param name="titleStr">�^�C�g����</param>
        /// <param name="explanationStr">������</param>
        public void MakeAchievementReport(Sprite iconSprite, string titleStr, string explanationStr)
        {
            achievementInfoList.Add(new AchievementInfo(iconSprite, titleStr, explanationStr));
        }

        void Update()
        {
            if(achievementInfoList.Count != 0)
            {
                if (makedAchievementReportObj == null)
                {
                    makedAchievementReportObj = Instantiate(achievementReportObj);
                    AchievementReportView achievementReportView = makedAchievementReportObj.GetComponent<AchievementReportView>();

                    AchievementInfo info = achievementInfoList[0];
                    achievementReportView.Init(info.iconSprite, info.titleStr, info.explanationStr);
                    achievementInfoList.RemoveAt(0);
                }
            }
        }
    }
}
