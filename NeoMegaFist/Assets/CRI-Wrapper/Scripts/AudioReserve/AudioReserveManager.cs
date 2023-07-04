using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReserveManager : MonoBehaviour
{
    [SerializeField]
    private AudioTimerObjPool objPool;

    private static AudioReserveManager instance { get; set; }

    private AudioReserveContainer reserveContainer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        reserveContainer = new AudioReserveContainer();
    }

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        reserveContainer?.SaveLog();
#endif
    }

    /// <summary>
    /// 再生予定の音声をスピーカーの画像を表示して表現する
    /// </summary>
    /// <param name="speakerName">音を出すオブジェクト名</param>
    /// <param name="contentText">内容</param>
    /// <param name="speakerObjTrans">音を出すオブジェクトのTransform</param>
    /// <param name="audioTime">再生予定の長さ</param>
    public static void AudioReserve(string speakerName, string contentText, Transform speakerObjTrans, float audioTime = 0.5f)
    {
#if UNITY_EDITOR
        instance.AudioReservePrivate(speakerName, contentText, speakerObjTrans, audioTime);
#endif
    }

    /// <summary>
    /// 再生予定の音声をスピーカーの画像を表示して表現する
    /// </summary>
    /// <param name="speakerName">音を出すオブジェクト名</param>
    /// <param name="contentText">内容</param>
    /// <param name="speakerObjTrans">音を出すオブジェクトのTransform</param>
    /// <param name="imageColor">スピーカー画像の色</param>
    /// <param name="audioTime">再生予定の長さ</param>
    public static void AudioReserve(string speakerName, string contentText, Transform speakerObjTrans, Color imageColor, float audioTime = 0.5f)
    {
#if UNITY_EDITOR
        instance.AudioReservePrivate(speakerName, contentText, speakerObjTrans, imageColor, audioTime);
#endif
    }



    private void AudioReservePrivate(string speakerName, string contentText, Transform speakerObjTrans, float audioTime)
    {
        objPool.GetActiveFalseObj(speakerObjTrans, audioTime);
        reserveContainer.RegisterAudioReserve(speakerName, contentText);
    }

    private void AudioReservePrivate(string speakerName, string contentText, Transform speakerObjTrans, Color imageColor, float audioTime)
    {
        objPool.GetActiveFalseObj(speakerObjTrans, imageColor, audioTime);
        reserveContainer.RegisterAudioReserve(speakerName, contentText);
    }
}
