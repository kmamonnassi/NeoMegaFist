using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class AudioTimer : MonoBehaviour
{
    [SerializeField]
    private Image timerImage;

    /// <summary>
    /// リンクしたオブジェクトと画像が追従するようにする
    /// </summary>
    /// <param name="linkTransform">リンクするオブジェクトのTransform</param>
    /// <param name="time">再生予定の長さ</param>
    public void SetImage(Transform linkTransform, float time)
    {
        gameObject.SetActive(true);
        linkTransform.ObserveEveryValueChanged(_ => linkTransform.position)
            .Where(_ => this.gameObject.activeSelf && linkTransform != null)
            .Subscribe(_ => { this.transform.position = linkTransform.position;})
            .AddTo(this.gameObject);

        StartCoroutine(SetActiveFalseByTimer(time));
    }

    /// <summary>
    /// 再生予定の長さ分画像を表示する
    /// </summary>
    /// <param name="time">再生予定の長さ</param>
    IEnumerator SetActiveFalseByTimer(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// スピーカー画像の色を変更する
    /// </summary>
    /// <param name="imageColor">スピーカー画像の色</param>
    public void ChangeImageColor(Color imageColor)
    {
        timerImage.color = imageColor;
    }
}
