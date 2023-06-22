using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Zenject;
using CriWare;

namespace Audio
{
    public class SceneAudioImporter : SceneAudioImporterBase
    {
        [Inject]
        private IAudioLoadable audio;

        [Inject]
        private IBgmPlayable bgmPlayer;

        [HideInInspector]
        public bool isSceneLoadToPlayBgm;

        [HideInInspector]
        public BGM bgmCueSheet;

        [HideInInspector]
        public int sceneLoadToPlayBgmNum;

        [HideInInspector]
        public string sceneLoadToPlayBgmName;

        private async UniTask Start()
        {
            base.CreateAcbNameData();
            await DataLoad();

            // ÉVÅ[ÉìÇÃèââÒÇ≈BGMÇçƒê∂Ç∑ÇÈÇÃÇ»ÇÁÇŒçƒê∂Ç∑ÇÈ
            if (isSceneLoadToPlayBgm)
            {
                string cueSheetName = sceneLoadToPlayBgmName;
                CriAtomExAcb acbData = audio.GetAcbData(cueSheetName);
                bgmPlayer.SceneLoadToPlay(cueSheetName, acbData.GetCueInfoList()[0].name);
            }
        }

        private async UniTask DataLoad()
        {
            await audio.LoadCueSheet(base.acbNameList);
        }
    }
}