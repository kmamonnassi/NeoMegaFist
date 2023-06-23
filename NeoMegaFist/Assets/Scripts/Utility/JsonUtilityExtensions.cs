using UnityEngine;
using System;
using System.IO;

public static class JsonUtilityExtensions
{
    private const string JSON_DIRECTORY_PATH = "/JsonText";
    
    /// <summary>
    /// Jsonを保存するディレクトリがあるか調べ、無ければ生成する
    /// </summary>
    public static void CheckJsonDirectory()
    {
        string path = string.Empty;

#if UNITY_EDITOR
        path = Application.dataPath + JSON_DIRECTORY_PATH;
#else
        path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + JSON_DIRECTORY_PATH;
#endif

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// Jsonにデータを書き込む。無ければJsonを生成する
    /// </summary>
    /// <param name="data">jsonに書き込みたいデータ</param>
    /// <param name="jsonFileName">Jsonの名前。XXX.jsonと書く</param>
    public static void WriteJson<T>(T data, string jsonFileName)
    {
        string path = string.Empty;
        string jsonStr = JsonUtility.ToJson(data, true);

#if UNITY_EDITOR
        path = Application.dataPath + JSON_DIRECTORY_PATH;
#else
        path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + JSON_DIRECTORY_PATH;
#endif
        path = Path.Combine(path, jsonFileName);

        File.WriteAllText(path, jsonStr);
    }

    /// <summary>
    /// Jsonからデータを取得する
    /// </summary>
    /// <typeparam name="T">Jsonから変換したい型</typeparam>
    /// <param name="jsonFileName">Jsonの名前。XXX.jsonと書く</param>
    public static T ReadJson<T>(string jsonFileName)
    {
        string path = string.Empty;
        string dataStr = string.Empty;

#if UNITY_EDITOR
        path = Application.dataPath + JSON_DIRECTORY_PATH;
#else
        path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + JSON_DIRECTORY_PATH;
#endif
        path = Path.Combine(path, jsonFileName);

        if(File.Exists(path))
        {
            StreamReader streamReader = new StreamReader(path);
            dataStr = streamReader.ReadToEnd();
            streamReader.Close();

            return JsonUtility.FromJson<T>(dataStr);
        }
        else
        {
            return default(T);
        }
    }
}
