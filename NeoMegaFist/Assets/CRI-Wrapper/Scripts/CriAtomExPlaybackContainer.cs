using System.Collections.Generic;
using CriWare;
using System.Linq;
using UniRx;

namespace Audio
{
    public class CriAtomExPlaybackContainer
    {
        private List<CriAtomExPlayback> playbackList;

        private PlaybackBuffers playbackBuffers;

        private CriAtomExPlayback bgmPlayback;

        public CriAtomExPlaybackContainer()
        {
            playbackBuffers = new PlaybackBuffers(AudioSettingStaticData.PLAYBACK_BUFFER_SIZE);
            playbackList = Enumerable.Repeat(new CriAtomExPlayback(), AudioSettingStaticData.PLAYBACK_INIT_SIZE).ToList();
        }

        /// <summary>
        /// PlaybackのStatusをStart状態に設定する
        /// </summary>
        /// <param name="playback">CriAtomExPlayback</param>
        /// <param name="id">だれのPlaybackなのか判断するID</param>
        public void SetPlaybackStartStatusInPool(CriAtomExPlayback playback, string id)
        {
            // removed状態のPlaybackに代入
            for (int i = 0; i < playbackList.Count; i++)
            {
                bool isPlaybackRemoved = playbackList[i].GetStatus() == CriAtomExPlayback.Status.Removed;
                if (isPlaybackRemoved)
                {
                    playbackList[i] = playback;
                    playbackBuffers.RegisterPlayback(id, playback);
                    return;
                }
            }

            // removed状態が無ければ追加
            CriAtomExPlayback newPlayback = new CriAtomExPlayback();
            newPlayback = playback;
            playbackList.Add(newPlayback);
            playbackBuffers.RegisterPlayback(id, playback);
        }

        /// <summary>
        /// PlaybackのStatusをRemove状態に設定する
        /// </summary>
        /// <param name="id">識別ID</param>
        /// <param name="ignoresReleaseTime">リリース時間を無視するかどうか</param>
        public void SetPlaybackRemoveStatusInPool(string id, bool ignoresReleaseTime)
        {
            playbackBuffers.RemovePlayback(id, ignoresReleaseTime);
        }

        /// <summary>
        /// BGMのPlaybackをStart状態にする
        /// </summary>
        /// <param name="playback">CriAtomExPlayback</param>
        public void SetBgmPlaybackStartStatus(CriAtomExPlayback playback)
        {
            bgmPlayback = playback;
        }

        /// <summary>
        /// BGMのPlaybackをStop状態にする
        /// </summary>
        public void SetBgmPlaybaclStopStatus()
        {
            bgmPlayback.Stop();
        }
    }
}
