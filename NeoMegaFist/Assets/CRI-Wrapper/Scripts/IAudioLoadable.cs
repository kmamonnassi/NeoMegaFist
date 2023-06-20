using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using CriWare;
using System;

namespace Audio
{
    interface IAudioLoadable
    {
        /// <summary>
        /// キューシートをロードする
        /// </summary>
        /// <param name="acbNameList">キューシート名が格納されたList</param>
        public UniTask LoadCueSheet(List<string> acbNameList);

        /// <summary>
        /// 音声のデータのロード完了のコールバック
        /// </summary>
        public event Action OnCompleteAudioLoad;

        /// <summary>
        /// Acbを取得する
        /// </summary>
        /// <param name="cueSheetName">キューシート名</param>
        public CriAtomExAcb GetAcbData(string cueSheetName);
    }
}