namespace Audio
{
    interface IGettableAcfEnumInfo
    {
        /// <summary>
        /// カテゴリの種類の総数
        /// </summary>
        public int categoryKindNumProp { get; }

        /// <summary>
        /// BGMカテゴリの番号
        /// </summary>
        public int bgmCategoryNumProp { get; }

        /// <summary>
        /// GameSEカテゴリの番号
        /// </summary>
        public int seCategoryNumProp { get; }

        /// <summary>
        /// BGMカテゴリの名前
        /// </summary>
        public string bgmCategoryNameProp { get; }

        /// <summary>
        /// GameSEカテゴリの名前
        /// </summary>
        public string seCategoryNameProp { get; }

        /// <summary>
        /// カテゴリ名からEnumの番号を取得する
        /// </summary>
        /// <param name="categoryName">カテゴリ名</param>
        public int GetEnumNumFromString(string categoryName);

        /// <summary>
        /// Enumの番号からStringを取得する
        /// </summary>
        /// <param name="num">Enumの番号</param>
        public string GetCategoryNameFromNum(int num);
    }
}