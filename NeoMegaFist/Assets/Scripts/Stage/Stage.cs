using UnityEngine;
using Utility.PostEffect;
using Zenject;

namespace Stage
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private Color wallColor;
        [SerializeField] private Room[] rooms;

        [Inject] private IPostEffectCamera cam;

        private void Start()
        {
            cam.SetColor(wallColor);
        }
    }
}