using UnityEngine;

namespace Audio
{
    public class EnumAndBaseMakerSettingAsset : ScriptableObject
    {
        [SerializeField, Header("Asset/以下を記述")]
        public string exportPath;
    }
}
