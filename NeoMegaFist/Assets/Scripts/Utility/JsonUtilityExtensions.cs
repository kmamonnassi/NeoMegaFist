using UnityEngine;
using System;
using System.IO;

public static class JsonUtilityExtensions
{
    private const string JSON_DIRECTORY_PATH = "/JsonText";
    
    /// <summary>
    /// Json��ۑ�����f�B���N�g�������邩���ׁA������ΐ�������
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
    /// Json�Ƀf�[�^���������ށB�������Json�𐶐�����
    /// </summary>
    /// <param name="data">json�ɏ������݂����f�[�^</param>
    /// <param name="jsonFileName">Json�̖��O�BXXX.json�Ə���</param>
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
    /// Json����f�[�^���擾����
    /// </summary>
    /// <typeparam name="T">Json����ϊ��������^</typeparam>
    /// <param name="jsonFileName">Json�̖��O�BXXX.json�Ə���</param>
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
