using System;

namespace Audio
{
    public class AcfEnumInfo : IGettableAcfEnumInfo
    {
        private NeoMegaFist_acf.Category.CategoryGroup categoryEnum;

        private int categoryKindNum;
        public int categoryKindNumProp => categoryKindNum;

        private int bgmCategoryNum;
        public int bgmCategoryNumProp => bgmCategoryNum;

        private int seCategoryNum;
        public int seCategoryNumProp => seCategoryNum;

        private string bgmCategoryName;
        public string bgmCategoryNameProp => bgmCategoryName;

        private string seCategoryName;
        public string seCategoryNameProp => seCategoryName;

        public AcfEnumInfo()
        {
            categoryKindNum = Enum.GetValues(categoryEnum.GetType()).Length;

            bgmCategoryNum = (int)NeoMegaFist_acf.Category.CategoryGroup.BGM;
            seCategoryNum = (int)NeoMegaFist_acf.Category.CategoryGroup.SE;

            bgmCategoryName = NeoMegaFist_acf.Category.CategoryGroup.BGM.ToString();
            seCategoryName = NeoMegaFist_acf.Category.CategoryGroup.SE.ToString();
        }

        int IGettableAcfEnumInfo.GetEnumNumFromString(string categoryName)
        {
            return (int)Enum.Parse(categoryEnum.GetType(), categoryName);
        }

        string IGettableAcfEnumInfo.GetCategoryNameFromNum(int num)
        {
            return ((NeoMegaFist_acf.Category.CategoryGroup)Enum.ToObject(categoryEnum.GetType(), num)).ToString();
        }
    }
}