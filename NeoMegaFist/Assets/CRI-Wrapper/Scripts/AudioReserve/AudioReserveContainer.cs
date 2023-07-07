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
        logData.speakerArray = audioReserveDic.Values.ToArray();
        logData.containTextArray = audioReserveDic.Keys.ToArray();

        string jsonStr = JsonUtility.ToJson(logData, true);
        string sceneName = SceneManager.GetActiveScene().name;
        string jsonName = AudioReserveStaticData.LOG_JSON_NAME;
        string dataPath = Application.dataPath + AudioSettingStaticData.JSON_DIRECTORY_PATH + "/AudioLogData/" + sceneName + "_" + jsonName;

        // 以前のデータと見比べる
        if (File.Exists(dataPath))
        {
            AudioReserveLogData beforeLogData = new AudioReserveLogData();
            StreamReader streamReader = new StreamReader(dataPath);
            string beforeDataStr = streamReader.ReadToEnd();
            streamReader.Close();
            beforeLogData = JsonUtility.FromJson<AudioReserveLogData>(beforeDataStr);

            bool isNotDefauleBeforeData = beforeLogData.containTextArray.Length != 0 && beforeLogData.speakerArray.Length != 0;
            if (isNotDefauleBeforeData)
            {
                HashSet<string> containTextHashSet = new HashSet<string>();
                List<string> speakerList = new List<string>();
                containTextHashSet = logData.containTextArray.ToHashSet();
                speakerList = logData.speakerArray.ToList();

                int beforeDataCount = beforeLogData.containTextArray.Length;
                for (int i = 0; i < beforeDataCount; i++)
                {
                    if (!containTextHashSet.Contains(beforeLogData.containTextArray[i]))
                    {
                        containTextHashSet.Add(beforeLogData.containTextArray[i]);
                        speakerList.Add(beforeLogData.speakerArray[i]);
                    }
                }

                AudioReserveLogData comparedLogData = new AudioReserveLogData();
                comparedLogData.speakerArray = speakerList.ToArray();
                comparedLogData.containTextArray = containTextHashSet.ToArray();
                jsonStr = JsonUtility.ToJson(comparedLogData, true);
            }
        }

        File.WriteAllText(dataPath, jsonStr);
    }
}
