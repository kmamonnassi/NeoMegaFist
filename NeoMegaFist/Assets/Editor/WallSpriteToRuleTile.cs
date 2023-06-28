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
    [MenuItem("Window/WallSpriteToRuleTile")]
    static void Create()
    {
        RPGMakerToRuleTile_ver2 window = (RPGMakerToRuleTile_ver2)GetWindow(typeof(RPGMakerToRuleTile_ver2));
        window.Show();
    }
}
#endif