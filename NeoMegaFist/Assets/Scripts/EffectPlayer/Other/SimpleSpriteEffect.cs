using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleSpriteEffect : MonoBehaviour
{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private float[] intervals;
	[SerializeField] bool isLoop;

	private SpriteRenderer rend;
	private float nowTime;
	private int nowSpriteIndex;

	private void Awake()
	{
		rend = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if (sprites.Length <= nowSpriteIndex) return;

		nowTime += Time.deltaTime;
		if(nowTime > intervals[nowSpriteIndex])
		{
			nowTime = 0;
			rend.sprite = sprites[nowSpriteIndex];
			nowSpriteIndex++;

			if(nowSpriteIndex == sprites.Length && isLoop)
			{
				nowSpriteIndex = 0;
			}
		}
	}
}
