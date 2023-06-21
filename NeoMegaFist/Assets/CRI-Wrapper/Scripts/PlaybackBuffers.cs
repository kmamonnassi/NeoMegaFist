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
        /// Playback‚ğ“o˜^‚·‚é
        /// </summary>
        /// <param name="id">¯•ÊID</param>
        /// <param name="playback">CriAtomExPlayback</param>
        public void RegisterPlayback(string id, CriAtomExPlayback playback)
        {
            idList.Add(id);
            playbacks.Add(playback);

            idList.RemoveAt(0);
            playbacks.RemoveAt(0);
        }

        /// <summary>
        /// w’èID‚ÌPlayback‚ğÁ‚·BÄ¶’†‚Ì‰¹‚ÍÄ¶‚ª’â~‚·‚é
        /// </summary>
        /// <param name="id">¯•ÊID</param>
        /// <param name="ignoresReleaseTime">ƒŠƒŠ[ƒXŠÔ‚ğ–³‹‚·‚é‚©‚Ç‚¤‚©</param>
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
