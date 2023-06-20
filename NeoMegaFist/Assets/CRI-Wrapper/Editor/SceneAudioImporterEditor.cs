using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Audio;

[CustomEditor(typeof(SceneAudioImporter))]
public class SceneAudioImporterEditor : Editor
{
    private string[] selectedBgmNames;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SceneAudioImporter importer = target as SceneAudioImporter;

        importer.isSceneLoadToPlayBgm = EditorGUILayout.Toggle("IsSceneLoadToPlayBGM", importer.isSceneLoadToPlayBgm);

        if(importer.isSceneLoadToPlayBgm)
        {
            selectedBgmNames = importer.GetSelectedCueSheetNames()[(int)NeoMegaFist_acf.Category.CategoryGroup.BGM];
            importer.sceneLoadToPlayBgmNum = EditorGUILayout.Popup(new GUIContent("SceneLoadToPlayBGM"), importer.sceneLoadToPlayBgmNum, selectedBgmNames);
            importer.sceneLoadToPlayBgmName = selectedBgmNames[importer.sceneLoadToPlayBgmNum];
        }
    }
}
