using UnityEngine;

namespace Utility.PostEffect
{
    public interface IPostEffectMaterialDB
    {
        void SetShader(PostEffectType type, ref Material mat);
    }
}
