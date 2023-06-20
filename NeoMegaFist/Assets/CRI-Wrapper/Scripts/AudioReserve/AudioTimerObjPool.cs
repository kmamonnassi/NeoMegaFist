using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioTimerObjPool : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    private List<GameObject> timerObjPool;

    private void Awake()
    {
        int objPoolInitSize = AudioReserveStaticData.TIMER_OBJ_POOL_INIT_SIZE;
        timerObjPool = new List<GameObject>(objPoolInitSize);
        for (int i = 0; i < objPoolInitSize; i++)
        {
            GameObject timerObj = Instantiate(obj, gameObject.transform);
            timerObjPool.Add(timerObj);

            timerObj.SetActive(false);
        }
    }

    /// <summary>
    /// SetActive��false�ȃI�u�W�F�N�g���擾����
    /// </summary>
    /// <param name="linkTransform">�����N����I�u�W�F�N�g��Transform</param>
    /// <param name="audioTime">�Đ��\��̉��̒���</param>
    public GameObject GetActiveFalseObj(Transform linkTransform, float audioTime)
    {
        // SetActive��false�ȃI�u�W�F�N�g���擾
        foreach (var timerObj in timerObjPool)
        {
            if(timerObj.activeSelf == false)
            {
                AudioTimer audioTimer = timerObj.GetComponent<AudioTimer>();
                audioTimer.SetImage(linkTransform, audioTime);
                audioTimer.ChangeImageColor(Color.white);
                return timerObj;
            }
        }

        // ������ΐ���
        GameObject generatedTimerObj = Instantiate(obj, gameObject.transform);
        timerObjPool.Add(generatedTimerObj);
        AudioTimer generatedAudioTimer = generatedTimerObj.GetComponent<AudioTimer>();
        generatedAudioTimer.SetImage(linkTransform, audioTime);

        return generatedTimerObj;
    }

    /// <summary>
    /// SetActive��false�ȃI�u�W�F�N�g���擾
    /// </summary>
    /// <param name="linkTransform">�����N����I�u�W�F�N�g��Transform</param>
    /// <param name="imageColor">�X�s�[�J�[�摜�̐F</param>
    /// <param name="audioTime">�Đ��\��̉��̒���</param>
    public GameObject GetActiveFalseObj(Transform linkTransform, Color imageColor, float audioTime)
    {
        // SetActive��false�ȃI�u�W�F�N�g���擾
        foreach (var timerObj in timerObjPool)
        {
            if (timerObj.activeSelf == false)
            {
                AudioTimer audioTimer = timerObj.GetComponent<AudioTimer>();
                audioTimer.SetImage(linkTransform, audioTime);
                audioTimer.ChangeImageColor(imageColor);
                return timerObj;
            }
        }

        // ������ΐ���
        GameObject generatedTimerObj = Instantiate(obj, gameObject.transform);
        timerObjPool.Add(generatedTimerObj);
        AudioTimer generatedAudioTimer = generatedTimerObj.GetComponent<AudioTimer>();
        generatedAudioTimer.SetImage(linkTransform, audioTime);
        generatedAudioTimer.ChangeImageColor(imageColor);
        return generatedTimerObj;
    }
}
