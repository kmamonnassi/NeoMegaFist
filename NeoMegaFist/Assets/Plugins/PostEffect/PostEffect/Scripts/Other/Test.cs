using UnityEngine;
using Utility.PostEffect;
using Zenject;

public class Test : MonoBehaviour
{
    [SerializeField] private PostEffectType postEffectType;
    [SerializeField] private float time;
    [SerializeField] private Color color;
    [SerializeField] private FadeType fadeType;

    [Inject] private IPostEffector postEffector;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            postEffector.Fade(postEffectType, time, color, fadeType, DG.Tweening.Ease.Linear);
        }
    }
}
