namespace Audio
{
    interface ICategoryAudioControllable
    {
        /// <summary>
        /// 対象カテゴリをミュートする
        /// </summary>
        /// <param name="category">ミュートするカテゴリ</param>
        void CategoryMute(AudioCategory category);

        /// <summary>
        /// 対象カテゴリのミュートを解除する
        /// </summary>
        /// <param name="category">ミュート解除するカテゴリ</param>
        void CategoryReMute(AudioCategory category);

        /// <summary>
        /// 対象カテゴリの音を全部止める
        /// </summary>
        /// <param name="category">対象カテゴリ</param>
        /// <param name="ignoresReleaseTime">リリース時間を無視するかどうか</param>
        void CategoryStop(AudioCategory category, bool ignoresReleaseTime = true);

        /// <summary>
        /// カテゴリごとのサウンドが再生完了したか調べる
        /// </summary>
        /// <param name="categoryNum">カテゴリの番号</param>
        bool CheckCategoryAudioStop(int categoryNum);

        /// <summary>
        /// BGM以外のサウンドが止まったか調べる
        /// </summary>
        bool CheckAudioStopWithoutBgm();

        /// <summary>
        /// すべてのサウンドが止まったか調べる
        /// </summary>
        bool CheckAllAudioStop();
    }
}