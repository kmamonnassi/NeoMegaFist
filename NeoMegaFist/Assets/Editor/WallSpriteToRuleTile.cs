#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WallSpriteToRuleTile : EditorWindow
{
    [SerializeField] private Sprite targetSprite;
    [SerializeField] private int tileSize;

    private string path = "";

    [MenuItem("Window/WallSpriteToRuleTile")]
    static void Create()
    {
        WallSpriteToRuleTile window = (WallSpriteToRuleTile)GetWindow(typeof(WallSpriteToRuleTile));
        window.Show();
    }

    private void OnGUI()
    {
        targetSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", targetSprite, typeof(Sprite), false);
        EditorGUILayout.Space();
        tileSize = EditorGUILayout.IntField("TileSize", tileSize);
        if (targetSprite != null)
        {
            if (GUILayout.Button("Create", GUILayout.Width(200), GUILayout.Height(30)))
            {
                SetReadable(targetSprite.texture);
                CreateTexture();
            }
		}
    }

    //Texture の Read/Write Enabled を スクリプトから変更する
    private bool SetReadable(Texture2D texture)
    {
        var path = AssetDatabase.GetAssetPath(texture);
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer.isReadable == true)
            return false;

        importer.isReadable = true;
        importer.SaveAndReimport();
        return true;
    }

    private void CreateTexture()
    {
        Texture2D texture = new Texture2D(tileSize * 12, tileSize * 10, TextureFormat.RGBA32, false);
    }

    private Color[,] GetPixels(int posX, int posY, int width, int height)
    {
        Color[] c = targetSprite.texture.GetPixels(posX, posY, width, height);
        Color[,] colors = new Color[width, height];
        int i = 0;
        for(int x = 0; x < width;x++)
        {
            for(int y = 0; y < height;y++)
            {
                colors[x, y] = c[i];
                i++;
			}
		}
        return colors;
    }
}
#endif