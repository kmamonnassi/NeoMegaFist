using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using CriWare;
using System.Linq;

namespace Audio
{
    public class PlaybackBuffers
    {
        private List<string> idList;
        private List<CriAtomExPlayback> playbacks;

        public PlaybackBuffers(int initSize)
        {
            idList = Enumerable.Repeat("", initSize).ToList();
            playbacks = Enumerable.Repeat(new CriAtomExPlayback(), initSize).ToList();
        }

        /// <summary>
        /// Playbackを登録する
        /// </summary>
        /// <param name="id">識別ID</param>
        /// <param name="playback">CriAtomExPlayback</param>
        public void RegisterPlayback(string id, CriAtomExPlayback playback)
        {
            idList.Add(id);
            playbacks.Add(playback);

            idList.RemoveAt(0);
            playbacks.RemoveAt(0);
        }

        /// <summary>
        /// 指定IDのPlaybackを消す。再生中の音は再生が停止する
        /// </summary>
        /// <param name="id">識別ID</param>
        /// <param name="ignoresReleaseTime">リリース時間を無視するかどうか</param>
        public void RemovePlayback(string id, bool ignoresReleaseTime)
        {
            int playbackListIndex = idList.IndexOf(id);

            if (playbackListIndex == -1)
            {
                return;
            }

            playbacks[playbackListIndex].Stop(ignoresReleaseTime);

            idList[playbackListIndex] = "";
            playbacks[playbackListIndex] = default;
        }
    }
}
