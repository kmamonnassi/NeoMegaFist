using UnityEngine;
using UnityEditor;
using Audio;

[CustomEditor(typeof(SceneAudioImporter))]
public class SceneAudioImporterEditor : Editor
{
    private string[] selectedBgmNames;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SceneAudioImporter importer = target as SceneAudioImporter;

        if (importer.isSceneLoadToPlayBgm)
        {
            selectedBgmNames = importer.GetSelectedCueSheetNames()[(int)NeoMegaFist_acf.Category.CategoryGroup.BGM];
            if(selectedBgmNames.Length == 0)
            {
                EditorGUILayout.HelpBox("ロードするBGMデータが未設定です", MessageType.Error);
                return;
            }
            importer.sceneLoadToPlayBgmNum = EditorGUILayout.Popup(new GUIContent("SceneLoadToPlayBGM"), importer.sceneLoadToPlayBgmNum, selectedBgmNames);
            importer.sceneLoadToPlayBgmName = selectedBgmNames[importer.sceneLoadToPlayBgmNum];
        }
    }
}
