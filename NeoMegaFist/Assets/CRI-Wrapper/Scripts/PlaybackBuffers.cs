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
        /// Playback��o�^����
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="playback">CriAtomExPlayback</param>
        public void RegisterPlayback(string id, CriAtomExPlayback playback)
        {
            idList.Add(id);
            playbacks.Add(playback);

            idList.RemoveAt(0);
            playbacks.RemoveAt(0);
        }

        /// <summary>
        /// �w��ID��Playback�������B�Đ����̉��͍Đ�����~����
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="ignoresReleaseTime">�����[�X���Ԃ𖳎����邩�ǂ���</param>
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
