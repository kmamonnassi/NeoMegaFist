using UnityEngine;
using UnityEngine.UI;

namespace Utility.PostEffect
{
    public class PostEffectMaterialDB : MonoBehaviour, IPostEffectMaterialDB
    {
        [SerializeField] private PostEffectShaderData[] datas = null;
        [SerializeField] RawImage rawImage = null;

        void IPostEffectMaterialDB.SetShader(PostEffectType type, ref Material mat)
        {
            foreach (PostEffectShaderData data in datas)
            {
                if (type == data.Type)
                {
                    rawImage.material.shader = data.Shader;
                    mat = rawImage.material;
                    return;
                }
            }
        }

        private void OnApplicationQuit()
        {
            Material mat = null;
            ((IPostEffectMaterialDB)this).SetShader(PostEffectType.Default, ref mat);
        }
    }

    [System.Serializable]
    public class PostEffectShaderData
    {
        [SerializeField] PostEffectType type = PostEffectType.SimpleFade;
        [SerializeField] Shader shader = null;

        public PostEffectType Type => type;
        public Shader Shader => shader;
    }
}
