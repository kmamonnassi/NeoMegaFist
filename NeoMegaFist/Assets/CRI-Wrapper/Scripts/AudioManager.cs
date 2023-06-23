using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CriWare;
using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using System;

namespace Audio
{
    public class AudioManager : IInitializable, IAudioLoadable, ISePlayable, IBgmPlayable, ICategoryAudioControllable
    {
        [Inject]
        private IGettableAcfEnumInfo acfEnumInfo;

        public event Action OnCompleteAudioLoad;

        private List<string> loadedAcbNameList = new List<string>();
        private Dictionary<string, string> cueAndCategoryTable = new Dictionary<string, string>();

        private CancellationTokenSource cts;

        private CriAtomExPlayer[] atomExPlayers;

        private CriAtomExPlaybackContainer criAtomExPlaybackContainer;

        private string beforeBgmCueSheetName = "";

        public AudioManager()
        {
            // Playbackのオブジェクトプールを作成
            criAtomExPlaybackContainer = new CriAtomExPlaybackContainer();

            // トークン作成
            cts = new CancellationTokenSource();
        }

        // Injectを使った初期化はここで行う
        public void Initialize()
        {
            // カテゴリの数分CriAtomExPlayerを生成
            int categoryCount = acfEnumInfo.categoryKindNumProp;
            atomExPlayers = new CriAtomExPlayer[categoryCount];
            for (int i = 0; i < atomExPlayers.Length; i++)
            {
                atomExPlayers[i] = new CriAtomExPlayer();
            }
        }

        async UniTask IAudioLoadable.LoadCueSheet(List<string> acbNameList)
        {
            // 更新データが同じなら早期リターン
            bool isSameList = Enumerable.SequenceEqual(acbNameList.OrderBy(e => e), loadedAcbNameList.OrderBy(e => e));
            if (isSameList)
            {
                OnCompleteAudioLoad?.Invoke();
                return;
            }

            // 更新データとの差分を消す
            List<string> removeCueSheets = loadedAcbNameList.Except(acbNameList).ToList();
            foreach (var removeCueSheet in removeCueSheets)
            {
                // ロード済みかを判断するstringを削除
                loadedAcbNameList.Remove(removeCueSheet);

                // ディクショナリーに登録されているCueとカテゴリ情報を削除する
                CriAtomExAcb acb = ((IAudioLoadable)this).GetAcbData(removeCueSheet);
                CriAtomEx.CueInfo[] cueInfos = acb.GetCueInfoList();
                foreach (var cue in cueInfos)
                {
                    cueAndCategoryTable.Remove(cue.name);
                }

                // キューシート削除
                CriAtom.RemoveCueSheet(removeCueSheet);
            }

            // 追加されてない分のデータをロードする
            foreach (var acbName in acbNameList)
            {
                if (!loadedAcbNameList.Contains(acbName))
                {
                    loadedAcbNameList.Add(acbName);
                    CriAtomCueSheet cueSheet = CriAtom.AddCueSheetAsync(acbName, acbName + ".acb", "");
                    await UniTask.WaitUntil(() => !cueSheet.IsLoading, cancellationToken: cts.Token);

                    // ディクショナリーにCueとカテゴリを結び付けて登録する
                    CriAtomExAcb acb = cueSheet?.acb;
                    CriAtomEx.CueInfo[] cueInfos = acb.GetCueInfoList();
                    foreach (var cue in cueInfos)
                    {
                        CriAtomExAcf.GetCategoryInfoByIndex(cue.categories[0], out CriAtomExAcf.CategoryInfo categoryInfo);
                        cueAndCategoryTable.Add(cue.name, categoryInfo.name);
                    }
                }
            }
            OnCompleteAudioLoad?.Invoke();
        }

        void ISePlayable.Play(string cueSheetName, string cueName, int gameObjectInstanceID)
        {
            // acbを取得する
            CriAtomExAcb acb = ((IAudioLoadable)this).GetAcbData(cueSheetName);

            // カテゴリに対応したCriAtomExPlayerで再生する
            string categoryName = cueAndCategoryTable[cueName];
            int atomExPlayerNum = acfEnumInfo.GetEnumNumFromString(categoryName);
            atomExPlayers[atomExPlayerNum].SetCue(acb, cueName);
            string id = cueName + gameObjectInstanceID.ToString();
            criAtomExPlaybackContainer.SetPlaybackStartStatusInPool(atomExPlayers[atomExPlayerNum].Start(), id);
        }

        void ISePlayable.Stop(string cueName, int gameObjectInstanceID, bool ignoresReleaseTime)
        {
            string id = cueName + gameObjectInstanceID.ToString();
            criAtomExPlaybackContainer.SetPlaybackRemoveStatusInPool(id, ignoresReleaseTime);
        }

        void IBgmPlayable.SceneLoadToPlay(string cueSheetName, string cueName)
        {
            if (cueSheetName != beforeBgmCueSheetName)
            {
                // acbを取得する
                CriAtomExAcb acb = ((IAudioLoadable)this).GetAcbData(cueSheetName);

                // BGMカテゴリに対応したCriAtomExPlayerで再生する
                int atomExPlayerNum = acfEnumInfo.bgmCategoryNumProp;

                // わからん
                //atomExPlayers[atomExPlayerNum].AttachFader();
                //atomExPlayers[atomExPlayerNum].SetFadeInStartOffset(-1000);
                //atomExPlayers[atomExPlayerNum].SetFadeInTime(1000);
                //atomExPlayers[atomExPlayerNum].SetFadeOutTime(1000);
                // わからん
                atomExPlayers[atomExPlayerNum].SetCue(acb, cueName);
                criAtomExPlaybackContainer.SetBgmPlaybackStartStatus(atomExPlayers[atomExPlayerNum].Start());

                // 以前と同じBGM名なのか保存する
                beforeBgmCueSheetName = cueSheetName;
            }
        }

        void IBgmPlayable.Play(string cueSheetName, string cueName)
        {
            // acbを取得する
            CriAtomExAcb acb = ((IAudioLoadable)this).GetAcbData(cueSheetName);

            // BGMカテゴリに対応したCriAtomExPlayerで再生する
            int atomExPlayerNum = acfEnumInfo.bgmCategoryNumProp;
            atomExPlayers[atomExPlayerNum].SetCue(acb, cueName);
            criAtomExPlaybackContainer.SetBgmPlaybackStartStatus(atomExPlayers[atomExPlayerNum].Start());

            // 以前と同じBGM名なのか保存する
            beforeBgmCueSheetName = cueSheetName;
        }

        void IBgmPlayable.Stop()
        {
            criAtomExPlaybackContainer.SetBgmPlaybaclStopStatus();
        }

        void ICategoryAudioControllable.CategoryMute(AudioCategory category)
        {
            CriAtomExCategory.Mute(category.ToString(), true);
        }

        void ICategoryAudioControllable.CategoryReMute(AudioCategory category)
        {
            CriAtomExCategory.Mute(category.ToString(), false);
        }

        void ICategoryAudioControllable.CategoryStop(AudioCategory category, bool ignoresReleaseTime)
        {
            int atomExPlayerNum = (int)category;
            atomExPlayers[atomExPlayerNum].Stop(ignoresReleaseTime);
        }

        bool ICategoryAudioControllable.CheckCategoryAudioStop(int categoryNum)
        {
            CriAtomExPlayer.Status status = atomExPlayers[categoryNum].GetStatus();
            bool stop = status == CriAtomExPlayer.Status.Stop;
            bool playEnd = status == CriAtomExPlayer.Status.PlayEnd;
            if (stop || playEnd)
            {
                return true;
            }

            return false;
        }

        bool ICategoryAudioControllable.CheckAudioStopWithoutBgm()
        {
            int bgmCategoryNum = acfEnumInfo.bgmCategoryNumProp;

            for (int i = 0; i < atomExPlayers.Length; i++)
            {
                // BGMならコンテニュー
                if (i == bgmCategoryNum)
                {
                    continue;
                }

                bool checkStop = ((ICategoryAudioControllable)this).CheckCategoryAudioStop(i);

                if (!checkStop)
                {
                    return false;
                }
            }
            return true;
        }

        bool ICategoryAudioControllable.CheckAllAudioStop()
        {
            for (int i = 0; i < atomExPlayers.Length; i++)
            {
                bool checkStop = ((ICategoryAudioControllable)this).CheckCategoryAudioStop(i);
                if (!checkStop)
                {
                    return false;
                }
            }
            return true;
        }

        CriAtomExAcb IAudioLoadable.GetAcbData(string cueSheetName)
        {
            CriAtomCueSheet cueSheet = CriAtom.GetCueSheet(cueSheetName);
            if (cueSheet == null)
            {
                Debug.LogError(cueSheetName + ":キューシートが無いです");
                return null;
            }
            return cueSheet.acb;
        }
    }
}
