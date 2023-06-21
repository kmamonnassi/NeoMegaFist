using UnityEngine;
using UnityEditor;

public class CreateSceneAudioImporterPrefab
{
    private static string objDataPath = "Assets/CRI-Wrapper/Prefab/SceneAudioImporter.prefab";

    [MenuItem("GameObject/Audio/SceneAudioImporter", false, 0)]
    public static void CreatePrefab()
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(objDataPath);
        GameObject prefab = PrefabUtility.InstantiatePrefab(obj) as GameObject;
    }
}
