using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UniRx;
using System.Linq;
using Audio;


public class AudioReserveEditorWindow : EditorWindow
{
    private string searchPath = "";
    private string[] jsonDataNames;
    private ReactiveProperty<int> selectedJsonDataIndex = new ReactiveProperty<int>(0);
    private Dictionary<string, string> audioReserveDic;

    private Vector2 scrollPosition = Vector2.zero;
    private Texture reloadIconTex;
    private Texture copyIconTex;
    private bool isDeveloperFoldoutOpen = false;


    [MenuItem("Window/Audio/AudioReserve Browser", false, 0)]
    static void OpenWindow()
    {
        EditorWindow.GetWindow<AudioReserveEditorWindow>(false, "AudioReserve Browser");
    }

    private void OnEnable()
    {
        searchPath = Application.dataPath + AudioSettingStaticData.JSON_DIRECTORY_PATH + "/AudioLogData";
        DirectoryInfo dir = new DirectoryInfo(searchPath);
        FileInfo[] info = dir.GetFiles("*.json");

        if(info.Length == 0)
        {
            return;
        }

        jsonDataNames = new string[info.Length];
        for (int i = 0; i < info.Length; i++)
        {
            jsonDataNames[i] = info[i].Name;
        }

        selectedJsonDataIndex.Subscribe(i => AudioReserveDicFromJson(i));

        reloadIconTex = AssetDatabase.LoadAssetAtPath<Texture>("Packages/com.unity.collab-proxy/Editor/PlasticSCM/Assets/Images/d_refresh.png");
        copyIconTex = AssetDatabase.LoadAssetAtPath<Texture>("Packages/com.unity.2d.animation/Editor/Assets/EditorIcons/Dark/d_Copy.png");
    }

    private void OnGUI()
    {
        Information();

        GUIPartition();

        isDeveloperFoldoutOpen = EditorGUILayout.Foldout(isDeveloperFoldoutOpen, "開発者オプション", true);
        if(isDeveloperFoldoutOpen)
        {
            EditorGUI.indentLevel++;

            // StreamingAssetsを開く
            if (GUILayout.Button("StreamingAssetsを開く"))
            {
                System.Diagnostics.Process.Start(Application.streamingAssetsPath);
            }
            
            EditorGUI.indentLevel--;
        }

        GUIPartition();

        if (jsonDataNames == null)
        {
            return;
        }

        float boxWidth = position.size.x * 0.5f;

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Label("読み込むデータを選択する");
            using (new GUILayout.HorizontalScope())
            {
                selectedJsonDataIndex.Value = EditorGUILayout.Popup(selectedJsonDataIndex.Value, jsonDataNames);
                
                if (GUILayout.Button(reloadIconTex, GUILayout.Width(24f)))
                {
                    AudioReserveDicFromJson(selectedJsonDataIndex.Value);
                }
            }
        }

        using (new GUILayout.HorizontalScope())
        {

            EditorGUILayout.Space();
            GUILayout.Label("オブジェクト名", GUI.skin.box, GUILayout.Width(boxWidth - 10f));
            GUILayout.Label("内容", GUI.skin.box, GUILayout.Width(boxWidth - 10f));
            EditorGUILayout.Space();
        }

        GUIPartition();
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope())
            {
                foreach (var audioReserve in audioReserveDic)
                {
                    using (new GUILayout.HorizontalScope("box"))
                    {
                        string speakerName = audioReserve.Value;
                        GUILayout.Label(speakerName);
                        if (GUILayout.Button(copyIconTex, GUILayout.Width(24f)))
                        {
                            EditorGUIUtility.systemCopyBuffer = speakerName;
                        }
                    }
                }
            }

            using (new GUILayout.VerticalScope())
            {
                foreach (var audioReserve in audioReserveDic)
                {
                    using (new GUILayout.HorizontalScope("box"))
                    {
                        string containText = audioReserve.Key;
                        GUILayout.Label(containText);
                        if (GUILayout.Button(copyIconTex, GUILayout.Width(24f)))
                        {
                            EditorGUIUtility.systemCopyBuffer = containText;
                        }
                    }
                }
            }
        }
        EditorGUILayout.EndScrollView();
    }

    private void AudioReserveDicFromJson(int index)
    {
        string jsonName = jsonDataNames[index];
        string jsonPath = searchPath + "/" + jsonName;
        string dataStr = string.Empty;
        StreamReader streamReader = new StreamReader(jsonPath);
        dataStr = streamReader.ReadToEnd();
        streamReader.Close();

        AudioReserveLogData audioLogData = JsonUtility.FromJson<AudioReserveLogData>(dataStr);
        audioReserveDic = audioLogData.containTextArray.Select((k, i) => new { k, v = audioLogData.speakerArray[i] }).ToDictionary(a => a.k, a => a.v);

        audioReserveDic = audioReserveDic.OrderBy((x) => x.Value).ToDictionary(a => a.Key, a => a.Value);
    }

    /// <summary>
    /// 横線を描画する
    /// </summary>
    public void GUIPartition()
    {
        GUI.color = Color.gray;
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        GUI.color = Color.white;
    }

    /// <summary>
    /// 基本情報を表示する
    /// </summary>
    public void Information()
    {
        using (new EditorGUILayout.VerticalScope(GUI.skin.box))
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Version");
                EditorGUILayout.LabelField("Version " + CriWrapperInfo.GetVersion());
            }

            //using (new EditorGUILayout.HorizontalScope())
            //{
            //    EditorGUILayout.LabelField("How to use (Japanese)");
            //    if (GUILayout.Button("How to use (Japanese)"))
            //    {
            //        System.Diagnostics.Process.Start(CriWrapperInfo.GetRepositoryLink());
            //    }
            //}
        }
    }
}
