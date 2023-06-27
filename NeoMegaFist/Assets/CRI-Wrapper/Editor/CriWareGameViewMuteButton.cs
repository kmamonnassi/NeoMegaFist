using CriWare;
using UnityEditor;

// TODO:‚±‚ê‚Å‚¢‚¢‚Ì‚©‚í‚©‚ç‚ñ
[InitializeOnLoad]
public class CriWareGameViewMuteButton
{
    private static bool isAudioMute = false;

    static CriWareGameViewMuteButton()
    {
        EditorApplication.update += () =>
        {
            if (isAudioMute == EditorUtility.audioMasterMute) return;

            isAudioMute = EditorUtility.audioMasterMute;

            CriAtomExAsr.SetBusVolume(0, isAudioMute ? 0f : 1f);
        };
    }
}
