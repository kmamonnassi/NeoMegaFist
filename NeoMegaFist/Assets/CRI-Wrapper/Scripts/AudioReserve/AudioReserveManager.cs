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
    /// �Đ��\��̉������X�s�[�J�[�̉摜��\�����ĕ\������
    /// </summary>
    /// <param name="speakerName">�����o���I�u�W�F�N�g��</param>
    /// <param name="contentText">���e</param>
    /// <param name="speakerObjTrans">�����o���I�u�W�F�N�g��Transform</param>
    /// <param name="audioTime">�Đ��\��̒���</param>
    public static void AudioReserve(string speakerName, string contentText, Transform speakerObjTrans, float audioTime = 0.5f)
    {
#if UNITY_EDITOR
        instance.AudioReservePrivate(speakerName, contentText, speakerObjTrans, audioTime);
#endif
    }

    /// <summary>
    /// �Đ��\��̉������X�s�[�J�[�̉摜��\�����ĕ\������
    /// </summary>
    /// <param name="speakerName">�����o���I�u�W�F�N�g��</param>
    /// <param name="contentText">���e</param>
    /// <param name="speakerObjTrans">�����o���I�u�W�F�N�g��Transform</param>
    /// <param name="imageColor">�X�s�[�J�[�摜�̐F</param>
    /// <param name="audioTime">�Đ��\��̒���</param>
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
