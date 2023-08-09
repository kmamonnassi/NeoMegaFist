using System.Collections.Generic;
using UnityEngine;

namespace Ui.AchievementReport
{
    public class AchievementReportController : MonoBehaviour
    {
        [SerializeField]
        private GameObject achievementReportObj;

        // 表示予定の実績
        private List<AchievementInfo> achievementInfoList = new List<AchievementInfo>();

        GameObject makedAchievementReportObj = null;

        /// <summary>
        /// 実績解除UIを表示する
        /// </summary>
        /// <param name="iconSprite">アイコン画像</param>
        /// <param name="titleStr">タイトル名</param>
        /// <param name="explanationStr">説明文</param>
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
