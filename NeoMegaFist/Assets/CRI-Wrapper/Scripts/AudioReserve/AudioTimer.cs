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
    /// �����N�����I�u�W�F�N�g�Ɖ摜���Ǐ]����悤�ɂ���
    /// </summary>
    /// <param name="linkTransform">�����N����I�u�W�F�N�g��Transform</param>
    /// <param name="time">�Đ��\��̒���</param>
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
    /// �Đ��\��̒������摜��\������
    /// </summary>
    /// <param name="time">�Đ��\��̒���</param>
    IEnumerator SetActiveFalseByTimer(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// �X�s�[�J�[�摜�̐F��ύX����
    /// </summary>
    /// <param name="imageColor">�X�s�[�J�[�摜�̐F</param>
    public void ChangeImageColor(Color imageColor)
    {
        timerImage.color = imageColor;
    }
}
