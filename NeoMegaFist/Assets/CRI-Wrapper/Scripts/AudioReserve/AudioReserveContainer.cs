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
    /// 再生予定の情報を登録する
    /// </summary>
    /// <param name="speaker">音を出すオブジェクト名</param>
    /// <param name="containText">内容</param>
    public void RegisterAudioReserve(string speaker, string containText)
    {
        if(!audioReserveDic.ContainsKey(containText))
        {
            audioReserveDic.Add(containText, speaker);
        }
    }

    /// <summary>
    /// ログデータを保存する
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
