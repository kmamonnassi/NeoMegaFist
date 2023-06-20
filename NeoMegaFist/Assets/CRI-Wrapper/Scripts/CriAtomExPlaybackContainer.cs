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
        /// Playback��Status��Start��Ԃɐݒ肷��
        /// </summary>
        /// <param name="playback">CriAtomExPlayback</param>
        /// <param name="id">�����Playback�Ȃ̂����f����ID</param>
        public void SetPlaybackStartStatusInPool(CriAtomExPlayback playback, string id)
        {
            // removed��Ԃ�Playback�ɑ��
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

            // removed��Ԃ�������Βǉ�
            CriAtomExPlayback newPlayback = new CriAtomExPlayback();
            newPlayback = playback;
            playbackList.Add(newPlayback);
            playbackBuffers.RegisterPlayback(id, playback);
        }

        /// <summary>
        /// Playback��Status��Remove��Ԃɐݒ肷��
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="ignoresReleaseTime">�����[�X���Ԃ𖳎����邩�ǂ���</param>
        public void SetPlaybackRemoveStatusInPool(string id, bool ignoresReleaseTime)
        {
            playbackBuffers.RemovePlayback(id, ignoresReleaseTime);
        }

        /// <summary>
        /// BGM��Playback��Start��Ԃɂ���
        /// </summary>
        /// <param name="playback">CriAtomExPlayback</param>
        public void SetBgmPlaybackStartStatus(CriAtomExPlayback playback)
        {
            bgmPlayback = playback;
        }

        /// <summary>
        /// BGM��Playback��Stop��Ԃɂ���
        /// </summary>
        public void SetBgmPlaybaclStopStatus()
        {
            bgmPlayback.Stop();
        }
    }
}
