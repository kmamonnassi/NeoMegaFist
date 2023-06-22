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
    /// SetActiveがfalseなオブジェクトを取得する
    /// </summary>
    /// <param name="linkTransform">リンクするオブジェクトのTransform</param>
    /// <param name="audioTime">再生予定の音の長さ</param>
    public GameObject GetActiveFalseObj(Transform linkTransform, float audioTime)
    {
        // SetActiveがfalseなオブジェクトを取得
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

        // 無ければ生成
        GameObject generatedTimerObj = Instantiate(obj, gameObject.transform);
        timerObjPool.Add(generatedTimerObj);
        AudioTimer generatedAudioTimer = generatedTimerObj.GetComponent<AudioTimer>();
        generatedAudioTimer.SetImage(linkTransform, audioTime);

        return generatedTimerObj;
    }

    /// <summary>
    /// SetActiveがfalseなオブジェクトを取得
    /// </summary>
    /// <param name="linkTransform">リンクするオブジェクトのTransform</param>
    /// <param name="imageColor">スピーカー画像の色</param>
    /// <param name="audioTime">再生予定の音の長さ</param>
    public GameObject GetActiveFalseObj(Transform linkTransform, Color imageColor, float audioTime)
    {
        // SetActiveがfalseなオブジェクトを取得
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

        // 無ければ生成
        GameObject generatedTimerObj = Instantiate(obj, gameObject.transform);
        timerObjPool.Add(generatedTimerObj);
        AudioTimer generatedAudioTimer = generatedTimerObj.GetComponent<AudioTimer>();
        generatedAudioTimer.SetImage(linkTransform, audioTime);
        generatedAudioTimer.ChangeImageColor(imageColor);
        return generatedTimerObj;
    }
}
