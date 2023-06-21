using UnityEngine;
using UnityEngine.UI;

namespace StageObject.Buff
{
    public class BuffSlot : MonoBehaviour
    {
        [SerializeField] private Shader gaugeShader;
        [SerializeField] private Image back;
        [SerializeField] private Image icon;

        private BuffBase buff;
        private int shaderValuePropID;

        private void Start()
        {
            back.material = new Material(gaugeShader);
            shaderValuePropID = Shader.PropertyToID("_Value");
        }

        public void Initalize(BuffBase buff)
        {
            this.buff = buff;
            buff.OnSetDuration += OnSetDuration;
            buff.OnRemove += Inactive;
        }

        private void OnSetDuration(float duration)
        {
            back.material.SetFloat(shaderValuePropID, duration / buff.StartDuration);
        }

        private void Inactive()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Destroy(back.material);
        }
    }
}