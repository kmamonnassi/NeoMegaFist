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
        /// Playback‚ÌStatus‚ğStartó‘Ô‚Éİ’è‚·‚é
        /// </summary>
        /// <param name="playback">CriAtomExPlayback</param>
        /// <param name="id">‚¾‚ê‚ÌPlayback‚È‚Ì‚©”»’f‚·‚éID</param>
        public void SetPlaybackStartStatusInPool(CriAtomExPlayback playback, string id)
        {
            // removedó‘Ô‚ÌPlayback‚É‘ã“ü
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

            // removedó‘Ô‚ª–³‚¯‚ê‚Î’Ç‰Á
            CriAtomExPlayback newPlayback = new CriAtomExPlayback();
            newPlayback = playback;
            playbackList.Add(newPlayback);
            playbackBuffers.RegisterPlayback(id, playback);
        }

        /// <summary>
        /// Playback‚ÌStatus‚ğRemoveó‘Ô‚Éİ’è‚·‚é
        /// </summary>
        /// <param name="id">¯•ÊID</param>
        /// <param name="ignoresReleaseTime">ƒŠƒŠ[ƒXŠÔ‚ğ–³‹‚·‚é‚©‚Ç‚¤‚©</param>
        public void SetPlaybackRemoveStatusInPool(string id, bool ignoresReleaseTime)
        {
            playbackBuffers.RemovePlayback(id, ignoresReleaseTime);
        }

        /// <summary>
        /// BGM‚ÌPlayback‚ğStartó‘Ô‚É‚·‚é
        /// </summary>
        /// <param name="playback">CriAtomExPlayback</param>
        public void SetBgmPlaybackStartStatus(CriAtomExPlayback playback)
        {
            bgmPlayback = playback;
        }

        /// <summary>
        /// BGM‚ÌPlayback‚ğStopó‘Ô‚É‚·‚é
        /// </summary>
        public void SetBgmPlaybaclStopStatus()
        {
            bgmPlayback.Stop();
        }
    }
}
