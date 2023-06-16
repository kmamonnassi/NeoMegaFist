using UnityEngine;
using Utility.PostEffect;
using Zenject;

public class NonPostEffectEditorCamera : MonoBehaviour
{
    [Inject] private IPostEffectCamera cam;

    private void Start()
    {
        cam.SetPosition(transform.position);
        Destroy(gameObject);
    }
}
