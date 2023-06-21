namespace Audio
{
    interface IGettableAcfEnumInfo
    {
        /// <summary>
        /// �J�e�S���̎�ނ̑���
        /// </summary>
        public int categoryKindNumProp { get; }

        /// <summary>
        /// BGM�J�e�S���̔ԍ�
        /// </summary>
        public int bgmCategoryNumProp { get; }

        /// <summary>
        /// GameSE�J�e�S���̔ԍ�
        /// </summary>
        public int seCategoryNumProp { get; }

        /// <summary>
        /// BGM�J�e�S���̖��O
        /// </summary>
        public string bgmCategoryNameProp { get; }

        /// <summary>
        /// GameSE�J�e�S���̖��O
        /// </summary>
        public string seCategoryNameProp { get; }

        /// <summary>
        /// �J�e�S��������Enum�̔ԍ����擾����
        /// </summary>
        /// <param name="categoryName">�J�e�S����</param>
        public int GetEnumNumFromString(string categoryName);

        /// <summary>
        /// Enum�̔ԍ�����String���擾����
        /// </summary>
        /// <param name="num">Enum�̔ԍ�</param>
        public string GetCategoryNameFromNum(int num);
    }
}