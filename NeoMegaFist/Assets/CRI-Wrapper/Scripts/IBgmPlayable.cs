namespace Audio
{
    interface IBgmPlayable
    {
        /// <summary>
        /// シーンで最初にBGMを再生する
        /// </summary>
        /// <param name="cueSheetName">キューシート名</param>
        /// <param name="cueName">サウンド名</param>
        void SceneLoadToPlay(string cueSheetName, string cueName);

        /// <summary>
        /// BGMを再生する
        /// </summary>
        /// <param name="cueSheetName">キューシート名</param>
        /// <param name="cueName">サウンド名</param>
        void Play(string cueSheetName, string cueName);

        /// <summary>
        /// BGMを止める
        /// </summary>
        void Stop();
    }
}
