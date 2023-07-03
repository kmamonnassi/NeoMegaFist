#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class WallSpriteToRuleTile : EditorWindow
{
    [SerializeField] private Sprite targetSprite;
    [SerializeField] private int tileSize = 48;

    private int[] spriteIndex = new int[] { 12, 33, 34, 35, 36, 27, 28, 26, 25, 17, 18, 19, 20, 21, 22, 23, 24, 29, 30, 31, 32, 37, 38, 39, 40, 7, 4, 5, 6, 1, 0, 3, 2, 13, 14, 16, 15 };

    private int[][] tilingRuleNeighbors = new int[][]
    {
        new[] { 1, 1, 1, 1, 1, 1, 1, 1 },
        new[] { 1, 1, 1, 2, 2, 1, 1 },
        new[] { 2, 1, 1, 1, 1, 2, 1 },
        new[] { 2, 2, 1, 1, 1, 1, 1 },
        new[] { 1, 1, 2, 1, 1, 2, 1 },
        new[] { 1, 1, 1, 1, 2, 2, 1 },
        new[] { 1, 2, 1, 1, 2, 1, 1 },
        new[] { 2, 1, 1, 1, 1, 1, 2 },
        new[] { 2, 1, 1, 2, 1, 1, 1 },
        new[] { 2, 1, 1, 1, 2, 2 },
        new[] { 2, 1, 1, 1, 2, 2 },
        new[] { 2, 1, 1, 1, 2, 2 },
        new[] { 1, 2, 1, 1, 2, 2 },
        new[] { 1, 1, 2, 2, 2 },
        new[] { 1, 1, 2, 2, 2 },
        new[] { 1, 1, 2, 2, 2 },
        new[] { 1, 1, 2, 2, 2 },
        new[] { 1, 2, 2, 1, 2 },
        new[] { 1, 2, 2, 1, 2 },
        new[] { 1, 1, 2, 2, 2 },
        new[] { 2, 1, 2, 1, 2 },
        new[] { 1, 1, 1, 1, 1, 2 },
        new[] { 1, 1, 2, 1, 1, 1 },
        new[] { 1, 1, 1, 1, 2, 1 },
        new[] { 1, 1, 1, 1, 2, 1 },
        new[] { 1, 1, 2 },
        new[] { 1, 1, 2 },
        new[] { 1, 1, 2 },
        new[] { 1, 1, 2 },
        new[] { 1, 1, 2, 2 },
        new[] { 1, 1, 2, 2 },
        new[] { 1, 1, 2, 2 },
        new[] { 1, 1, 2, 2 },
        new[] { 1, 1, 2, 1, 1, 1, 1, 1 },
        new[] { 1, 1, 1, 1, 1, 1, 1, 2 },
        new[] { 1, 1, 1, 1, 1, 1, 1, 2 },
        new[] { 1, 1, 1, 1, 1, 1, 2, 1 },
    };
    private Vector3Int[][] tilingRuleNeighborPositions = new Vector3Int[][]
    {
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0) },
        new[] { new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(0, 1, 0) },
        new[] { new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(-1, -1, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 1, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 1, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 1, 0) },
        new[] { new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0) },
        new[] { new Vector3Int(1, 1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0) },
        new[] { new Vector3Int(-1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) },
        new[] { new Vector3Int(-1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0) },
        new[] { new Vector3Int(1, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 0, 0) },
        new[] { new Vector3Int(-1, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-1, 0, 0) },
        new[] { new Vector3Int(1, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(-1, -1, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(1, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0) },
        new[] { new Vector3Int(1, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0) },
        new[] { new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0) },
        new[] { new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 0, 0) },
        new[] { new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0) },
        new[] { new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(1, 1, 0), new Vector3Int(-1, -1, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, 1, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0) },
        new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(-1, -1, 0), new Vector3Int(1, -1, 0) },
    };

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
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.SaveAndReimport();
        importer.isReadable = true;
        importer.spritePixelsPerUnit = 1;
        importer.SaveAndReimport();
        return true;
    }

    private void CreateTexture()
    {
        Texture2D texture = new Texture2D(tileSize * 12, tileSize * 10, TextureFormat.RGBA32, false);

        Color[,] tile_1 = GetPixels(0, 0, tileSize, tileSize, targetSprite.texture);
        Color[,] tile_2 = GetPixels(tileSize, 0, tileSize, tileSize, targetSprite.texture);
        Color[,] tile_3 = GetPixels(tileSize * 2, 0, tileSize, tileSize, targetSprite.texture);
        Color[,] tile_4 = GetPixels(tileSize * 3, 0, tileSize, tileSize, targetSprite.texture);
        Color[,] diagonalTiles = GetPixels(0, tileSize, tileSize * 2, tileSize * 2, targetSprite.texture);
        Color[,] parts_1 = GetPixels(tileSize * 2, tileSize, tileSize / 3 * 2, tileSize * 2, targetSprite.texture);
        Color[,] parts_2 = GetPixels(tileSize * 2 + tileSize / 3 * 2, tileSize, tileSize / 3 * 2, tileSize * 2, targetSprite.texture);
        Color[,] parts_3 = GetPixels(tileSize * 2 + tileSize / 3 * 4, tileSize, tileSize, tileSize * 2, targetSprite.texture);
        Color[,] parts_4 = GetPixels(tileSize * 4 + tileSize / 3, 0, tileSize / 3, tileSize * 2, targetSprite.texture);

        //パーツからタイルを作成
        Color[,] combined_1 = ResizePixels(CombineX(parts_1, parts_2), tileSize * 2, tileSize * 2, tileSize / 6, -tileSize / 6);
        Color[,] combined_2 = InvertPixels(ResizePixels(CombineX(parts_2, parts_3),tileSize * 2, tileSize * 2, tileSize / 2, tileSize / 6), true, false);
        Color[,] combined_3 = RotatePixels(combined_1, 3);
        Color[,] combined_4 = RotatePixels(combined_2, 3);

        Color[,] c5_parts_1 = CombineX(RotatePixels(parts_3, 2), ResizePixels(parts_4, tileSize / 3, tileSize * 2, 0, tileSize / 3));
        Color[,] c5_parts_2 = InvertPixels(RotatePixels(parts_3, 2), true, false);
        Color[,] combined_5 = ResizePixels(CombineX(c5_parts_1, c5_parts_2), tileSize * 2, tileSize * 2, -tileSize / 6, -tileSize / 6 - 1);


        //tile_1の差分を4つ配置
        SetPixels(tileSize * 0, tileSize * 0, tile_1, texture);
        SetPixels(tileSize * 1, tileSize * 0, RotatePixels(tile_1, 1), texture);
        SetPixels(tileSize * 1, tileSize * 1, RotatePixels(tile_1, 2), texture);
        SetPixels(tileSize * 0, tileSize * 1, RotatePixels(tile_1, 3), texture);

        //tile_2の差分を4つ配置
        SetPixels(tileSize * 2, tileSize * 0, tile_2, texture);
        SetPixels(tileSize * 3, tileSize * 0, RotatePixels(tile_2, 1), texture);
        SetPixels(tileSize * 3, tileSize * 1, RotatePixels(tile_2, 2), texture);
        SetPixels(tileSize * 2, tileSize * 1, RotatePixels(tile_2, 3), texture);

        //tile_3の差分を4つ配置
        SetPixels(tileSize * 4, tileSize * 0, tile_3, texture);
        SetPixels(tileSize * 5, tileSize * 0, RotatePixels(tile_3, 1), texture);
        SetPixels(tileSize * 5, tileSize * 1, RotatePixels(tile_3, 2), texture);
        SetPixels(tileSize * 4, tileSize * 1, RotatePixels(tile_3, 3), texture);

        //tile_4を配置
        SetPixels(tileSize * 6, tileSize * 0, tile_4, texture);

        //tile_4を三角に削ってその差分を4つ配置
        Color[,] triangle = InvertPixels(TrimTriangle(tile_4), true, false);
        SetPixels(tileSize * 7, tileSize * 0, triangle, texture);
        SetPixels(tileSize * 8, tileSize * 0, RotatePixels(triangle, 1), texture);
        SetPixels(tileSize * 8, tileSize * 1, RotatePixels(triangle, 2), texture);
        SetPixels(tileSize * 7, tileSize * 1, RotatePixels(triangle, 3), texture);


        //diagonalTiles(斜めタイルの差分を4つ配置)
        SetPixels(tileSize * 0, tileSize * 2, diagonalTiles, texture);
        SetPixels(tileSize * 2, tileSize * 2, InvertPixels(diagonalTiles, true, false), texture);
        SetPixels(tileSize * 0, tileSize * 4, InvertPixels(diagonalTiles, false, true), texture);
        SetPixels(tileSize * 2, tileSize * 4, InvertPixels(diagonalTiles, true, true), texture);

        //combined_1の差分を4つ配置
        SetPixels(tileSize * 4, tileSize * 2, combined_1, texture);
        SetPixels(tileSize * 6, tileSize * 2, InvertPixels(combined_1, true, false), texture);
        SetPixels(tileSize * 4, tileSize * 4, InvertPixels(combined_1, false, true), texture);
        SetPixels(tileSize * 6, tileSize * 4, InvertPixels(combined_1, true, true), texture);

        //combined_2の差分を4つ配置
        SetPixels(tileSize * 8, tileSize * 2, combined_2, texture);
        SetPixels(tileSize * 10, tileSize * 2, InvertPixels(combined_2, true, false), texture);
        SetPixels(tileSize * 8, tileSize * 4, InvertPixels(combined_2, false, true), texture);
        SetPixels(tileSize * 10, tileSize * 4, InvertPixels(combined_2, true, true), texture);

        //combined_3の差分を4つ配置
        SetPixels(tileSize * 0, tileSize * 6, combined_3, texture);
        SetPixels(tileSize * 2, tileSize * 6, InvertPixels(combined_3, true, false), texture);
        SetPixels(tileSize * 0, tileSize * 8, InvertPixels(combined_3, false, true), texture);
        SetPixels(tileSize * 2, tileSize * 8, InvertPixels(combined_3, true, true), texture);

        //combined_4の差分を4つ配置
        SetPixels(tileSize * 4, tileSize * 6, combined_4, texture);
        SetPixels(tileSize * 6, tileSize * 6, InvertPixels(combined_4, true, false), texture);
        SetPixels(tileSize * 4, tileSize * 8, InvertPixels(combined_4, false, true), texture);
        SetPixels(tileSize * 6, tileSize * 8, InvertPixels(combined_4, true, true), texture);

        //combined_5の差分を4つ配置
        SetPixels(tileSize * 8, tileSize * 6, combined_5, texture);
        SetPixels(tileSize * 10, tileSize * 6, InvertPixels(combined_5, false, true), texture);
        SetPixels(tileSize * 8, tileSize * 8, RotatePixels(combined_5, 3), texture);
        SetPixels(tileSize * 10, tileSize * 8, RotatePixels(combined_5, 1), texture);

        // 保存先のファイルパスを取得する
        var filePath = EditorUtility.SaveFilePanel("Save", "Assets", targetSprite.name + "_sliced", "png");

        if (!string.IsNullOrEmpty(filePath))
        {
            //テクスチャをスプライトとして保存
            texture.Apply();
            byte[] b = texture.EncodeToPNG();
            File.WriteAllBytes(filePath, b);

            string assetsFilePath = filePath.Replace("\\", "/").Replace(Application.dataPath, "Assets");
            //再インポート
            AssetDatabase.ImportAsset(assetsFilePath, ImportAssetOptions.ForceUpdate);

            //生成したテクスチャのパスからインポーターを生成
            Texture2D savedTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetsFilePath);
            SetReadable(savedTexture);
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetsFilePath) as TextureImporter;
            textureImporter.spriteImportMode = SpriteImportMode.Multiple;

            var factory = new SpriteDataProviderFactories();
            factory.Init();
            var dataProvider = factory.GetSpriteEditorDataProviderFromObject(textureImporter);
            dataProvider.InitSpriteEditorDataProvider();
            dataProvider.SetSpriteRects(new SpriteRect[0]);
            var spriteNameFileIdDataProvider = dataProvider.GetDataProvider<ISpriteNameFileIdDataProvider>();
            var nameFileIdPairs = spriteNameFileIdDataProvider.GetNameFileIdPairs().ToList();

            //スライス
            for (int i = 0; i < 3;i++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        CreateSlicedSpriteMetaData(tileSize * x + tileSize * 2 * i, tileSize * y, tileSize, tileSize, savedTexture, dataProvider, nameFileIdPairs);
                    }
                }
            }
            CreateSlicedSpriteMetaData(tileSize * 6, 0, tileSize, tileSize, savedTexture, dataProvider, nameFileIdPairs);
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    CreateSlicedSpriteMetaData(tileSize * x + tileSize * 7, tileSize * y, tileSize, tileSize, savedTexture, dataProvider, nameFileIdPairs);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        CreateSlicedSpriteMetaData(tileSize * 2 * x + tileSize * 4 * i, tileSize * 2 + tileSize * 2 * y, tileSize * 2, tileSize * 2, savedTexture, dataProvider, nameFileIdPairs);
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        CreateSlicedSpriteMetaData(tileSize * 2 * x + tileSize * 4 * i, tileSize * 6 + tileSize * 2 * y, tileSize * 2, tileSize * 2, savedTexture, dataProvider, nameFileIdPairs);
                    }
                }
            }
            dataProvider.Apply();
            spriteNameFileIdDataProvider.SetNameFileIdPairs(nameFileIdPairs);
            AssetDatabase.ImportAsset(assetsFilePath, ImportAssetOptions.ForceUpdate);

            var sprites = AssetDatabase.LoadAllAssetsAtPath(assetsFilePath).OfType<Sprite>().ToArray();
            RuleTile ruleTile = CreateInstance<RuleTile>();
            ruleTile.m_DefaultSprite = sprites[4];
            List<RuleTile.TilingRule> rules = new List<RuleTile.TilingRule>();
            for(int i = 0; i < spriteIndex.Length;i++)
            {
                rules.Add(CreateTilingRule(i, sprites));
			}
            ruleTile.m_TilingRules = rules;
            string fileName = targetSprite.name + ".asset";
            string tilePath = EditorUtility.SaveFilePanel("Save", "Assets", fileName, "asset").Replace("\\", "/").Replace(Application.dataPath, "Assets");
            if (!string.IsNullOrEmpty(tilePath))
            {
                Debug.Log(tilePath);
                AssetDatabase.CreateAsset(ruleTile, tilePath);
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(ruleTile);
            }
            AssetDatabase.ImportAsset(tilePath, ImportAssetOptions.ForceUpdate);
        }
    }

    private RuleTile.TilingRule CreateTilingRule(int idx, Sprite[] sprites)
    {
        RuleTile.TilingRule tilingRule = new RuleTile.TilingRule();
        tilingRule.m_NeighborPositions = tilingRuleNeighborPositions[idx].ToList();
        tilingRule.m_Neighbors = tilingRuleNeighbors[idx].ToList();
        tilingRule.m_Sprites[0] = sprites[spriteIndex[idx]];
        return tilingRule;
    }

    private void CreateSlicedSpriteMetaData(int posX, int posY, int width, int height, Texture2D texture, ISpriteEditorDataProvider dataProvider, List<SpriteNameFileIdPair> nameFileIdPairs)
    {
        SpriteRect newSprite = new SpriteRect()
        {
            name = texture.name + "_" + dataProvider.GetSpriteRects().Length.ToString(),
            spriteID = GUID.Generate(),
            rect = new Rect(posX, texture.height - posY - height, width, height),
            alignment = SpriteAlignment.Custom,
            pivot = new Vector2(0.5f, 0.5f),
        };
        
        var spriteRects = dataProvider.GetSpriteRects().ToList();
        spriteRects.Add(newSprite);
        dataProvider.SetSpriteRects(spriteRects.ToArray());

        nameFileIdPairs.Add(new SpriteNameFileIdPair(newSprite.name, newSprite.spriteID));
    }

    private Color[,] GetPixels(int posX, int posY, int width, int height, Texture2D texture)
    {
        Color[,] colors = new Color[width, height];
        for(int x = 0; x < width;x++)
        {
            for(int y = height - 1; y >= 0;y--)
            {
                colors[x, y] = texture.GetPixel(posX + x, y + texture.height - posY - height);
            }
		}
        return colors;
    }

    private Color[,] RotatePixels(Color[,] pixels, int rotate90Multiply)
    {
        int w = pixels.GetLength(0);
        int h = pixels.GetLength(1);
        int loopX = 0;
        int loopY = 0;

        Color[,] newPixels = null;

        switch(rotate90Multiply)
        {
            case 1 or 3:
                newPixels = new Color[h, w];
                loopX = h;
                loopY = w;
                break;
            case 2:
                newPixels = new Color[w, h];
                loopX = w;
                loopY = h;
                break;
        }

        for (int x = 0; x < loopX; x++)
        {
            for (int y = 0; y < loopY; y++)
            {
                switch(rotate90Multiply)
                {
                    case 1:
                        newPixels[x, y] = pixels[w - 1 - y, x];
                        break;
                    case 2:
                        newPixels[x, y] = pixels[w - 1 - x, h - 1 - y];
                        break;
                    case 3:
                        newPixels[x, y] = pixels[y, h - 1 - x];
                        break;
                }
            }
        }
        return newPixels;
    }

    private Color[,] InvertPixels(Color[,] pixels, bool invertX, bool invertY)
    {
        int w = pixels.GetLength(0);
        int h = pixels.GetLength(1);

        Color[,] newPixels = new Color[w, h];
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                int _x = invertX ? w - 1 - x : x;
                int _y = invertY ? h - 1 - y : y;
                newPixels[x, y] = pixels[_x, _y];
            }
        }
        return newPixels;
    }

    private Color[,] CombineX(Color[,] c1, Color[,] c2)
    {
        int w1 = c1.GetLength(0);
        int w2 = c2.GetLength(0);
        int h = Mathf.Max(c1.GetLength(1), c2.GetLength(1));
        Color[,] newPixels = new Color[w1 + w2, h];

        for (int x = 0; x < w1; x++)
        {
            for (int y = 0; y < h; y++)
            {
                newPixels[x, y] = c1[x, y];
            }
        }

        for (int x = w1; x < w1 + w2; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (y >= h) continue;
                if (y < 0) continue;
                newPixels[x, y] = c2[x - w1, y];
            }
        }

        return newPixels;
    }

    private Color[,] TrimPixels(Color[,] pixels, int startX, int startY, int endX, int endY)
    {
        int w = pixels.GetLength(0) - startX - endX;
        int h = pixels.GetLength(1) - startY - endY;
        Color[,] newPixels = new Color[w, h];
        
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                newPixels[x, y] = pixels[x + startX, y + startY];
            }
        }
        return newPixels;
	}

    private Color[,] ResizePixels(Color[,] pixels, int sizeX, int sizeY, int paddingLeft, int paddingTop)
    {
        int w = pixels.GetLength(0);
        int h = pixels.GetLength(1);
        Color[,] newPixels = new Color[sizeX, sizeY];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (x + paddingLeft >= sizeX) continue;
                if (y + paddingTop >= sizeY) continue;
                if (x + paddingLeft < 0) continue;
                if (y + paddingTop < 0) continue;

                newPixels[x + paddingLeft, y + paddingTop] = pixels[x, y];
            }
        }
        return newPixels;
    }

    private Color[,] TrimTriangle(Color[,] pixels)
    {
        int w = pixels.GetLength(0);
        int h = pixels.GetLength(1);
        Color[,] newPixels = new Color[w, h];

        for (int y = 0; y < h; y++)
        {
            w--;
            for (int x = 0; x < w; x++)
            {
                newPixels[x, y] = pixels[x, y];
            }
        }
        return newPixels;
	}

    private void SetPixels(int posX, int posY, Color[,] pixels, Texture2D texture)
    {
        int w = pixels.GetLength(0);
        int h = pixels.GetLength(1);

        for (int x = 0; x < w;x++)
        {
            for(int y = 0; y < h; y++)
            {
                texture.SetPixel(x + posX, y + texture.height - h - posY, pixels[x, y]);
            }
		}
	}
}

public class TilingRuleElement
{
    public Sprite sprite;
    public List<int> neighbors;
}

public class RuleTileDebugger
{
    [MenuItem("CONTEXT/RuleTile/Debug Neighbors")]
    static void DebugNeighbors(MenuCommand command)
    {
        RuleTile tile = (RuleTile)command.context;
        string text = null;
        for(int i = 0; i < tile.m_TilingRules.Count;i++)
        {
            text += "new[] { ";
            for (int j = 0; j < tile.m_TilingRules[i].m_Neighbors.Count; j++)
            {
                text += tile.m_TilingRules[i].m_Neighbors[j];
                if(j != tile.m_TilingRules[i].m_Neighbors.Count - 1)
                {
                    text += ", ";
                }
            }
            text += " },\n";
        }
        Debug.Log(text);
    }

    [MenuItem("CONTEXT/RuleTile/Debug NeighborPositions")]
    static void DebugNeighborPositions(MenuCommand command)
    {
        RuleTile tile = (RuleTile)command.context;
        string text = null;
        for (int i = 0; i < tile.m_TilingRules.Count; i++)
        {
            text += "new[] { ";
            for (int j = 0; j < tile.m_TilingRules[i].m_NeighborPositions.Count; j++)
            {
                text += "new Vector3";
                text += tile.m_TilingRules[i].m_NeighborPositions[j];
                if (j != tile.m_TilingRules[i].m_NeighborPositions.Count - 1)
                {
                    text += ", ";
                }
            }
            text += " },\n";
        }
        Debug.Log(text);
    }
}
#endif