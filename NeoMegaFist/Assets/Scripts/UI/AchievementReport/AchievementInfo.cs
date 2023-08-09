using UnityEngine;

namespace Ui.AchievementReport
{
    public struct AchievementInfo
    {
        public Sprite iconSprite;
        public string titleStr;
        public string explanationStr;

        public AchievementInfo(Sprite iconSprite, string titleStr, string explanationStr)
        {
            this.iconSprite = iconSprite;
            this.titleStr = titleStr;
            this.explanationStr = explanationStr;
        }
    }
}
