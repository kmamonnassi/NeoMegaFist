namespace PostProcessingVolume
{
    interface IBloomSettable
    {
        /// <summary>
        /// ブルームの強さを設定する
        /// </summary>
        /// <param name="intensity">強さ</param>
        public void SetBloomIntensity(float intensity);
    }
}