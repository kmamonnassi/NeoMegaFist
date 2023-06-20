using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
public abstract class SceneAudioImporterBase : MonoBehaviour
{
	[SerializeField]
	private List<SE> seList = new List<SE>();

	[SerializeField]
	private List<BGM> bgmList = new List<BGM>();

	private int categoryKindNum = 2;

	protected List<string> acbNameList = new List<string>();

	protected void CreateAcbNameData()
	{
		foreach (var seAcbData in seList)
		{
			acbNameList.Add(seAcbData.ToString());
		}
		foreach (var bgmAcbData in bgmList)
		{
			acbNameList.Add(bgmAcbData.ToString());
		}
	}
	public string[][] GetSelectedCueSheetNames()
	{
		string[][] cueSheetNames = new string[categoryKindNum][];
		cueSheetNames[0] = seList.ConvertAll(e => e.ToString()).ToArray();
		cueSheetNames[1] = bgmList.ConvertAll(e => e.ToString()).ToArray();

		return cueSheetNames;
	}
}
}
