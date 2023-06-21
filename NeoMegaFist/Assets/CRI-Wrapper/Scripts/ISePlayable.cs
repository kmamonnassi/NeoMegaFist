namespace Audio
{
    interface ISePlayable
    {
        /// <summary>
        /// 音を再生する<br>連続再生可能</br>
        /// </summary>
        /// <param name="cueSheetName">キューシート名</param>
        /// <param name="cueName">サウンド名</param>
        /// <param name="gameObjectInstanceID">gameObject.GetInstanceID();で取れるGameObjectごとの固有番号</param>
        public void Play(string cueSheetName, string cueName, int gameObjectInstanceID);

        /// <summary>
        /// 音を止める
        /// </summary>
        /// <param name="cueName">サウンド名</param>
        /// <param name="gameObjectInstanceID">gameObject.GetInstanceID();で取れるGameObjectごとの固有番号</param>
        /// <param name="ignoresReleaseTime">リリース時間を無視するかどうか</param>
        public void Stop(string cueName, int gameObjectInstanceID, bool ignoresReleaseTime = true);

    }
}