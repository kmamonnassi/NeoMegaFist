using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using Audio;

public class AudioReserveContainer
{
    private Dictionary<string, string> audioReserveDic = new Dictionary<string, string>();

    /// <summary>
    /// �Đ��\��̏���o�^����
    /// </summary>
    /// <param name="speaker">�����o���I�u�W�F�N�g��</param>
    /// <param name="containText">���e</param>
    public void RegisterAudioReserve(string speaker, string containText)
    {
        if(!audioReserveDic.ContainsKey(containText))
        {
            audioReserveDic.Add(containText, speaker);
        }
    }

    /// <summary>
    /// ���O�f�[�^��ۑ�����
    /// </summary>
    public void SaveLog()
    {
        AudioReserveLogData logData = new AudioReserveLogData();
        logData.speakerArray = audioReserveDic.Keys.ToArray();
        logData.containTextArray = audioReserveDic.Values.ToArray();

        string jsonStr = JsonUtility.ToJson(logData, true);
        string sceneName = SceneManager.GetActiveScene().name;
        string jsonName = AudioReserveStaticData.LOG_JSON_NAME;
        string dataPath = Application.dataPath + AudioSettingStaticData.JSON_DIRECTORY_PATH + "/AudioLogData/" + sceneName + "_" + jsonName;
        File.WriteAllText(dataPath, jsonStr);
    }
}
