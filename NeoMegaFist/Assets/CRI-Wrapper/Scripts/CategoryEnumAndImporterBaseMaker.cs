using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using CriWare;
using System.Linq;

#if UNITY_EDITOR 

namespace Audio
{
    public class CategoryEnumAndImporterBaseMaker : MonoBehaviour
    {
        [SerializeField]
        private EnumAndBaseMakerSettingAsset settingAsset;

        private IGettableAcfEnumInfo acfEnumInfo;

        private static string br = "\n";
        private static string tab = "\t";

        void Start()
        {
            acfEnumInfo = new AcfEnumInfo();

            string exportPath = Application.dataPath + "/" + settingAsset.exportPath + "/";
            var searchPath = Application.streamingAssetsPath;
            var projInfo = new CriAtomWindowInfo();
            var acbInfoList = projInfo.GetAcbInfoList(true, searchPath);
            int categoryCount = acfEnumInfo.categoryKindNumProp;

            CreateEnumScript(categoryCount, exportPath, acbInfoList);
            CreateSceneAudioImporterBaseScript(categoryCount, exportPath);
            CreateAudioCategoryScript(categoryCount, exportPath);

            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
            Debug.Log("Convert Success");
        }

        /// <summary>
        /// カテゴリーごとに分けられたEnumのスクリプトを作成する
        /// </summary>
        /// <param name="categoryNum">カテゴリーの総数</param>
        /// <param name="exportPath">エクスポート先</param>
        /// <param name="acbInfoList">acbInfoのList</param>
        private void CreateEnumScript(int categoryNum, string exportPath, List<CriAtomWindowInfo.AcbInfo> acbInfoList)
        {
            if (!Directory.Exists(exportPath + "Enum"))
            {
                Directory.CreateDirectory(exportPath + "Enum");
            }

            for (int i = 0; i < categoryNum; i++)
            {
                string categoryName = acfEnumInfo.GetCategoryNameFromNum(i);
                File.WriteAllText(exportPath + "Enum/" + categoryName + ".cs", CreateEnumCode(categoryName, acbInfoList), Encoding.UTF8);
            }
        }

        /// <summary>
        /// カテゴリーごとに分けられたEnumを作成する
        /// </summary>
        /// <param name="categoryName">カテゴリー名</param>
        /// <param name="acbInfoList">acbInfoのList</param>
        private string CreateEnumCode(string categoryName, List<CriAtomWindowInfo.AcbInfo> acbInfoList)
        {
            string code = "";
            int elementCount = 0;
            HashSet<string> elementName = new HashSet<string>();

            code += "public enum " + categoryName + br;
            code += "{" + br;
            foreach (var acbInfo in acbInfoList)
            {
                foreach (var cueInfo in acbInfo.cueInfoList)
                {
                    if (cueInfo.categoryNames[0] == categoryName)
                    {
                        if (elementName.Contains(acbInfo.name))
                        {
                            continue;
                        }
                        code += tab + acbInfo.name + " = " + elementCount + "," + br;
                        elementCount++;
                        elementName.Add(acbInfo.name);
                    }
                }
            }
            code += "}" + br;

            return code;
        }

        /// <summary>
        /// SceneAudioImporterの基底クラスを自動生成する
        /// </summary>
        /// <param name="categoryNum">カテゴリーの総数</param>
        /// <param name="exportPath">エクスポート先</param>
        private void CreateSceneAudioImporterBaseScript(int categoryNum, string exportPath)
        {
            File.WriteAllText(exportPath + "SceneAudioImporterBase.cs", CreateSceneAudioImporterBaseCode(categoryNum), Encoding.UTF8);
        }

        /// <summary>
        /// SceneAudioImporterの基底クラスのコードを作成する
        /// </summary>
        /// <param name="categoryNum">カテゴリーの総数</param>
        private string CreateSceneAudioImporterBaseCode(int categoryNum)
        {
            string code = "";
            List<string> categoryNameList = new List<string>();
            for (int i = 0; i < categoryNum; i++)
            {
                string categoryName = acfEnumInfo.GetCategoryNameFromNum(i);
                categoryNameList.Add(categoryName);
            }

            code += "using System.Collections.Generic;" + br;
            code += "using UnityEngine;" + br;
            code += br;

            code += "namespace Audio" + br;
            code += "{" + br;
            code += "public abstract class SceneAudioImporterBase : MonoBehaviour" + br;
            code += "{" + br;
            for (int i = 0; i < categoryNum; i++)
            {
                code += tab + "[SerializeField]" + br;
                code += tab + "private List<" + categoryNameList[i] + "> " + categoryNameList[i].ToLower() + "List = new List<" + categoryNameList[i] + ">();" + br;
                code += br;
            }

            code += tab + "private int categoryKindNum = " + categoryNum.ToString() + ";" + br;
            code += br;

            code += tab + "protected List<string> acbNameList = new List<string>();" + br;
            code += br;
            code += tab + "protected void CreateAcbNameData()" + br;
            code += tab + "{" + br;
            for (int i = 0; i < categoryNum; i++)
            {
                string objectName = categoryNameList[i].ToLower() + "AcbData";
                code += tab + tab + "foreach (var " + objectName + " in " + categoryNameList[i].ToLower() + "List)" + br;
                code += tab + tab + "{" + br;
                code += tab + tab + tab + "acbNameList.Add(" + objectName + ".ToString());" + br;
                code += tab + tab + "}" + br;
            }
            code += tab + "}" + br;

            code += tab + "public string[][] GetSelectedCueSheetNames()" + br;
            code += tab + "{" + br;
            code += tab + tab + "string[][] cueSheetNames = new string[categoryKindNum][];" + br;
            for (int i = 0; i < categoryNum; i++)
            {
                code += tab + tab + "cueSheetNames[" + i.ToString() + "] = " + categoryNameList[i].ToLower() + "List.ConvertAll(e => e.ToString()).ToArray();" + br;
            }
            code += br;
            code += tab + tab + "return cueSheetNames;" + br;
            code += tab + "}" + br;

            code += "}" + br;
            code += "}" + br;

            return code;
        }

        /// <summary>
        /// カテゴリの種類のEnumのスクリプトを作成する
        /// </summary>
        /// <param name="categoryNum">カテゴリの総数</param>
        /// <param name="exportPath">エクスポート先</param>
        private void CreateAudioCategoryScript(int categoryNum, string exportPath)
        {
            if (!Directory.Exists(exportPath + "Enum"))
            {
                Directory.CreateDirectory(exportPath + "Enum");
            }

            File.WriteAllText(exportPath + "Enum/" + "AudioCategory" + ".cs", CreateAudioCategoryCode(categoryNum), Encoding.UTF8);
        }

        /// <summary>
        /// カテゴリの種類のEnumを作成する
        /// </summary>
        /// <param name="categoryNum">カテゴリの総数</param>
        private string CreateAudioCategoryCode(int categoryNum)
        {
            string code = "";

            code += "public enum AudioCategory" + br;
            code += "{" + br;

            for (int i = 0; i < categoryNum; i++)
            {
                code += tab + acfEnumInfo.GetCategoryNameFromNum(i) + " = " + i.ToString() + "," + br;
            }
            code += "}" + br;

            return code;
        }
    }

}


#endif
