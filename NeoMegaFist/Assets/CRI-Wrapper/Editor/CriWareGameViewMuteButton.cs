using CriWare;
using UnityEditor;

#if UNITY_EDITOR

[InitializeOnLoad]
public class CriWareGameViewMuteButton
{
    private static bool isAudioMute = false;

    static CriWareGameViewMuteButton()
    {
        EditorApplication.update += () =>
        {
            isAudioMute = EditorUtility.audioMasterMute;

#pragma warning disable CS0618 // Œ^‚Ü‚½‚Íƒƒ“ƒo[‚ª‹ŒŒ^®‚Å‚·
            CriAtomExAsr.SetBusVolume(0, isAudioMute ? 0f : 1f);
#pragma warning restore CS0618 // Œ^‚Ü‚½‚Íƒƒ“ƒo[‚ª‹ŒŒ^®‚Å‚·
        };
    }
}

#endif