#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

using UnityEngine;

public class RPGMakerToRuleTile_ver2 : EditorWindow
{
    [SerializeField] Sprite _targetSprite;
    [SerializeField] int _size = 16;
    [SerializeField] int _countX = 3;
    [SerializeField] int _countY = 3;
    [SerializeField] int _animCount = 3;
    [SerializeField] int _animSpeed = 1;
    [SerializeField] int _offsetX = 0;
    [SerializeField] int _offsetY = 0;
    [SerializeField] Sprite _slicedSprite;
    [SerializeField] RuleTile.TilingRule.OutputSprite _output;

    public Sprite targetSprite { get { return _targetSprite; } set { _targetSprite = value; } }
    public int size { get { return _size; } set { _size = value; } }
    public int countX { get { return _countX; } set { _countX = value; } }
    public int countY { get { return _countY; } set { _countY = value; } }
    public int offsetX { get { return _offsetX; } set { _offsetX = value; } }
    public int offsetY { get { return _offsetY; } set { _offsetY = value; } }
    public int animCount { get { return _animCount; } set { _animCount = value; } }
    public int animSpeed { get { return _animSpeed; } set { _animSpeed = value; } }
    public Sprite slicedSprite { get { return _slicedSprite; } set { _slicedSprite = value; } }
    public RuleTile.TilingRule.OutputSprite output { get { return _output; } set { _output = value; } }

    private string path = "";

    private void OnEnable()
    {
        //dataPath = Application.dataPath + "/GameMaterial/Texture";
    }

    private List<RuleAndSprite> ruleAndSprite = new List<RuleAndSprite>() {
            new RuleAndSprite(0,0,0,0,2,1,2,1,1,2,1,2),
            new RuleAndSprite(0,0,0,1,2,1,2,1,1,2,1,1),
            new RuleAndSprite(0,0,2,0,2,1,2,1,1,1,1,2),
            new RuleAndSprite(0,0,2,1,2,1,2,1,1,0,1,0),
            new RuleAndSprite(0,0,4,3,2,1,2,1,1,0,2,0),//5
            new RuleAndSprite(0,3,0,0,2,1,1,1,1,2,1,2),
            new RuleAndSprite(0,3,0,1,2,1,1,1,1,2,1,1),
            new RuleAndSprite(0,3,2,0,2,1,1,1,1,1,1,2),
            new RuleAndSprite(0,3,2,1,2,1,1,1,1,1,1,1),
            new RuleAndSprite(0,3,4,3,2,1,1,1,1,0,2,0),//10
            new RuleAndSprite(0,4,0,2,2,1,0,1,2,2,1,0),
            new RuleAndSprite(0,4,2,2,2,1,0,1,2,1,1,0),
            new RuleAndSprite(0,4,4,4,2,1,0,1,2,0,2,0),
            new RuleAndSprite(1,2,3,4,0,2,0,2,2,0,2,0),
            //new RuleAndSprite(1,2,3,4,2,2,2,2,2,2,2,2),//15
            new RuleAndSprite(1,2,1,2,0,2,0,2,2,0,1,0),
            new RuleAndSprite(1,1,3,3,0,2,0,2,1,0,2,0),
            new RuleAndSprite(1,1,1,0,0,2,0,2,1,0,1,2),
            new RuleAndSprite(1,1,1,1,0,2,0,2,1,0,1,1),
            new RuleAndSprite(4,0,0,0,1,1,2,1,1,2,1,2),//20
            new RuleAndSprite(4,0,0,1,1,1,2,1,1,2,1,1),
            new RuleAndSprite(4,0,2,0,1,1,2,1,1,1,1,2),
            new RuleAndSprite(4,0,2,1,1,1,2,1,1,1,1,1),
            new RuleAndSprite(4,0,4,3,1,1,2,1,1,0,2,0),
            new RuleAndSprite(4,3,0,0,1,1,1,1,1,2,1,2),//25
            new RuleAndSprite(4,3,0,1,1,1,1,1,1,2,1,1),
            new RuleAndSprite(4,3,2,0,1,1,1,1,1,1,1,2),
            new RuleAndSprite(4,3,2,1,1,1,1,1,1,1,1,1),
            new RuleAndSprite(4,3,4,3,1,1,1,1,1,0,2,0),
            new RuleAndSprite(4,4,0,2,1,1,0,1,2,2,1,0),//30
            new RuleAndSprite(4,4,2,2,1,1,0,1,2,1,1,0),
            new RuleAndSprite(4,4,4,4,0,1,0,1,2,0,2,0),
            new RuleAndSprite(2,2,0,2,0,2,0,1,2,2,1,0),
            new RuleAndSprite(2,2,2,2,0,2,0,1,2,1,1,0),
            new RuleAndSprite(2,2,4,4,0,2,0,1,2,0,2,0),//35
            new RuleAndSprite(2,1,0,0,0,2,0,1,1,2,1,2),
            new RuleAndSprite(2,1,0,1,0,2,0,1,1,2,1,1),
            new RuleAndSprite(2,1,2,0,0,2,0,1,1,1,1,2),
            new RuleAndSprite(2,1,2,1,0,2,0,1,1,1,1,1),
            new RuleAndSprite(2,1,4,3,0,2,0,1,1,0,2,0),//40
            new RuleAndSprite(3,0,3,3,0,1,2,2,1,0,2,0),
            new RuleAndSprite(3,0,1,0,0,1,2,2,1,0,1,2),
            new RuleAndSprite(3,0,1,1,0,1,2,2,1,0,1,1),
            new RuleAndSprite(3,3,3,3,0,1,1,2,1,0,2,0),
            new RuleAndSprite(3,3,1,0,0,1,1,2,1,0,1,2),//45
            new RuleAndSprite(3,3,1,1,0,1,1,2,1,0,1,1),
            new RuleAndSprite(3,4,3,4,0,1,0,2,2,0,2,0),
            new RuleAndSprite(3,4,1,2,0,1,0,2,2,0,1,0),//48
        };

    [MenuItem("Window/RPGMakerToRuleTile_ver2")]
    static void Create()
    {
        RPGMakerToRuleTile_ver2 window = (RPGMakerToRuleTile_ver2)GetWindow(typeof(RPGMakerToRuleTile_ver2));
        window.Show();
    }

    private void OnGUI()
    {
        targetSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", targetSprite, typeof(Sprite), false);
        EditorGUILayout.Space();
        size = EditorGUILayout.IntSlider("TileSize", size, 1, 128);
        EditorGUILayout.Space();
        countX = EditorGUILayout.IntSlider("CountX", countX, 1, 32);
        EditorGUILayout.Space();
        countY = EditorGUILayout.IntSlider("CountY", countY, 1, 32);
        EditorGUILayout.Space();
        offsetX = EditorGUILayout.IntField("OffsetX", offsetX);
        offsetY = EditorGUILayout.IntField("OffsetY", offsetY);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        bool b = GUILayout.Button("CreateSlicedSprite", GUILayout.Width(200), GUILayout.Height(30));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        if (targetSprite != null)
            if (b)
            {
                CreateSlicedSprites();
            }

        slicedSprite = (Sprite)EditorGUILayout.ObjectField("SlicedSprite", slicedSprite, typeof(Sprite), false);
        output = (RuleTile.TilingRule.OutputSprite)EditorGUILayout.EnumPopup("Output", output);
        if (output == RuleTile.TilingRule.OutputSprite.Animation)
        {
            animCount = EditorGUILayout.IntSlider("AnimationCount", animCount, 1, 6);
            animSpeed = EditorGUILayout.IntSlider("AnimationSpeed", animSpeed, 1, 20);
        }
        if (slicedSprite != null)
        {
            if(GUILayout.Button("CreateSlicedSprite", GUILayout.Width(200), GUILayout.Height(30)))
            {
                CreateRuleTile();
            }
        }
    }

    void CreateSlicedSprites()
    {
        int ctn = countY;
        for (int y = targetSprite.texture.height - (size * 3); ctn > 0; y -= (size * 3))
        {
            for (int x = 0; x < countX; x++)
            {
                string name = x.ToString() + y.ToString();
                CreateSlicedSprite(CreateLinkingTextures(x * (size * 2) + offsetX, y - offsetY), name);
            }
            ctn--;
        }
        
    }

    void CreateRuleTile()
    {
        int ctn = countY;
        switch (output)
        {
            case RuleTile.TilingRule.OutputSprite.Single:
                CreateRuleTile(CreateRules(GetLinkingSprites()));
                break;

            case RuleTile.TilingRule.OutputSprite.Random:
                Debug.Log("知らね");
                break;

            case RuleTile.TilingRule.OutputSprite.Animation:

                //List<List<Sprite>> spl = new List<List<Sprite>>();
                //for (int y = targetSprite.texture.height - (size * 3); ctn > 0; y -= (size * 3))
                //{
                //    for (int x = 0; x < countX; x++)
                //    {
                //        spl.Add(CreateLinkingSprites(CreateSliceTextures(x * (size * 2), y)));
                //    }
                //    ctn--;
                //}
                //int tileCtn = countX * countY;
                //List<List<RuleTile.TilingRule>> rtl = new List<List<RuleTile.TilingRule>>();

                //for (int i = 0; i < tileCtn / animCount ;i++)
                //{
                //    List<RuleTile.TilingRule> l = CreateRules(spl[i * animCount]);
                //    foreach (RuleTile.TilingRule r in l)
                //    {
                //        r.m_Output = RuleTile.TilingRule.OutputSprite.Animation;
                //        r.m_AnimationSpeed = animSpeed;
                //        Sprite s = r.m_Sprites[0];
                //        r.m_Sprites = new Sprite[animCount];
                //        r.m_Sprites[0] = s;
                //    }
                //    for (int s = 1; s < animCount ;s++)
                //    {
                //        for(int r = 0; r < ruleAndSprite.Count;r++)
                //        {
                //            l[r].m_Sprites[s] = spl[s + i * animCount][r];
                //        }
                //    }
                //    rtl.Add(l);
                //}
                //List<Sprite> def_sp = new List<Sprite>();
                //ctn = countY;
                //for (int y = targetSprite.texture.height - (size * 3); ctn > 0; y -= (size * 3))
                //{
                //    for (int x = 0; x < countX / animCount; x++)
                //    {
                //        Texture2D deftex = new Texture2D(size, size);
                //        deftex.filterMode = FilterMode.Point;
                //        deftex.SetPixels(targetSprite.texture.GetPixels(x * animCount * size * 2, y + size * 2, size, size));
                //        deftex.Apply();
                //        Sprite sp = Sprite.Create(deftex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
                //        def_sp.Add(sp);
                //    }
                //    ctn--;
                //}
                //for (int i = 0; i < rtl.Count ;i++)
                //{
                //    CreateRuleTile(def_sp[i], rtl[i]);
                //}

                break;
        }
    }

    Texture2D[,] CreateLinkingTextures(int cx, int cy)
    {
        int resize = size / 2;
        Texture2D[,] sliceTex = new Texture2D[5, 4];
        int x = size + cx;
        int y = size * 2 + cy;
        for (int i = 0; i < 5; i++)
        {
            int _x = 0;
            int _y = resize;
            for (int r = 0; r < 4; r++)
            {
                sliceTex[i, r] = new Texture2D(resize, resize);
                sliceTex[i, r].SetPixels(targetSprite.texture.GetPixels(_x + x, _y + y, resize, resize));
                sliceTex[i, r].name = "[" + (x + _x) + "][" + (y + _y) + "]";
                _x += resize;
                if (_x >= size)
                {
                    _y -= resize;
                    _x = 0;
                }
            }
            x += size;
            if (x > size + cx) { x = cx; y -= size; }
        }
        return sliceTex;
    }

    void CreateSlicedSprite(Texture2D[,] slicedTex, string partOfName)
    {
        int resultTextureSize = 7;
        int resize = size / 2;
        int x = 0;
        int y = resultTextureSize - 1;
        Texture2D texture = new Texture2D(size * resultTextureSize, size * resultTextureSize, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;
        foreach (RuleAndSprite rs in ruleAndSprite)
        {
            if (x >= resultTextureSize)
            {
                x = 0;
                y--;
            }
            int posX = x * size;
            int posY = y * size;
            texture.SetPixels(posX, resize + posY, resize, resize, slicedTex[rs.texs[0], 0].GetPixels());
            texture.SetPixels(resize + posX, resize + posY, resize, resize, slicedTex[rs.texs[1], 1].GetPixels());
            texture.SetPixels(posX, posY, resize, resize, slicedTex[rs.texs[2], 2].GetPixels());
            texture.SetPixels(resize + posX, posY, resize, resize, slicedTex[rs.texs[3], 3].GetPixels());
            texture.Apply();
            x++;
        }
        string name = _targetSprite.name + partOfName + ".png";

        byte[] b = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/" + name, b);
    }

    public Sprite[] GetLinkingSprites()
    {
        string path = AssetDatabase.GetAssetPath(slicedSprite.texture);
        var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray(); ;
        return sprites;
    }

    List<RuleTile.TilingRule> CreateRules(Sprite[] linkingSprites)
    {
        List<RuleTile.TilingRule> list = new List<RuleTile.TilingRule>();
        for (int i = 0; i < linkingSprites.Length; i++)
        {
            RuleTile.TilingRule r = new RuleTile.TilingRule();
            r.m_Sprites[0] = linkingSprites[i];
            r.m_ColliderType = UnityEngine.Tilemaps.Tile.ColliderType.Grid;
            r.m_Neighbors = ruleAndSprite[i].neighbors;
            list.Add(r);
        }
        return list;
    }

    void CreateRuleTile(List<RuleTile.TilingRule> list)
    {
        RuleTile ruleTile = CreateInstance<RuleTile>();
        ruleTile.m_TilingRules = list;
        var fileName = "ruleTile.asset";
        fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, fileName)));
        path = EditorUtility.SaveFilePanelInProject("Save", fileName, "asset", "asset", path);
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(ruleTile, path);
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(ruleTile);
        }
    }

    void CreateRuleTile(Sprite sp, List<RuleTile.TilingRule> list)
    {
        RuleTile ruleTile = CreateInstance<RuleTile>();
        ruleTile.m_TilingRules = list;
        ruleTile.m_DefaultSprite = sp;

        var fileName = "ruleTile.asset";
        fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, fileName)));
        path = EditorUtility.SaveFilePanelInProject("Save", fileName, "asset", "asset", path);
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(ruleTile, path);
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(ruleTile);
        }
    }

    public class RuleAndSprite
    {
        public int[] texs = new int[4];
        public List<int> neighbors = new List<int>();
        //public int[] neighbors = new int[8];

        public RuleAndSprite(int tl, int tr, int bl, int br, int _1, int _2, int _3, int _4, int _5, int _6, int _7, int _8)
        {
            texs[0] = tl;
            texs[1] = tr;
            texs[2] = bl;
            texs[3] = br;
            //neighbors[0] = _1;
            //neighbors[1] = _2;
            //neighbors[2] = _3;
            //neighbors[3] = _4;
            //neighbors[4] = _5;
            //neighbors[5] = _6;
            //neighbors[6] = _7;
            //neighbors[7] = _8;
            neighbors.Add(_1);
            neighbors.Add(_2);
            neighbors.Add(_3);
            neighbors.Add(_4);
            neighbors.Add(_5);
            neighbors.Add(_6);
            neighbors.Add(_7);
            neighbors.Add(_8);
        }
    }
}
#endif