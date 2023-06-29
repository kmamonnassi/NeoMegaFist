using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

// TODO:ï ÇÃèÍèäÇ…à⁄ìÆÇ≥ÇπÇÈ
public enum OptionKinds
{
    DisplaySetting = 0,
    AudioSetting = 1,
}

public class OptionTabGroup : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tabObjs;

    private Dictionary<OptionKinds, GameObject> tabDic = new Dictionary<OptionKinds, GameObject>();

    private GameObject makedTabObj = null;

    [Inject]
    private DiContainer container;

    private void Awake()
    {
        foreach (var tabObj in tabObjs)
        {
            OptionTab optionTab = tabObj.GetComponent<OptionTab>();
            if (!tabDic.ContainsKey(optionTab.optionKindProp))
            {
                tabDic.Add(optionTab.optionKindProp, tabObj);
            }
        }
    }

    private void Start()
    {
        // TODO:Ç±Ç±ÇíËêîÇ…Ç∑ÇÈ
        ShowTab(OptionKinds.AudioSetting);
    }

    public void ShowTab(OptionKinds optionKind)
    {
        if(makedTabObj != null)
        {
            HideTab();
        }
        makedTabObj = container.InstantiatePrefab(tabDic[optionKind], transform);
    }

    public void HideTab()
    {
        Destroy(makedTabObj);
    }
}
