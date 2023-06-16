using UnityEngine;
using DG.Tweening;
using System;
using Zenject;

namespace Utility.PostEffect
{
    public class PostEffector : IPostEffector
    {
        private IPostEffectMaterialDB materialDB;

        [Inject]
        public PostEffector(IPostEffectMaterialDB materialDB)
        {
            this.materialDB = materialDB;
        }

        public void Play(PostEffectType postEffectType)
        {
            Material mat = null;
            materialDB.SetShader(postEffectType, ref mat);
        }

        public void Fade(PostEffectType postEffectType, float time, Color color, FadeType fadeType, Ease ease = Ease.Linear)
        {
            Material mat = null;
            materialDB.SetShader(postEffectType, ref mat);

            mat.SetColor("_Color", color);
            int fadeId = Shader.PropertyToID("_Fade");
            int startValue = 1;
            int endValue = 0;
            switch (fadeType)
            {
                case FadeType.Out:
                    startValue = 0;
                    endValue = 1;
                    break;
            }

            DOVirtual.Float(startValue, endValue, time, value =>
            {
                mat.SetFloat(fadeId, value);
            }).SetEase(ease);

        }
    }

    public enum FadeType
    {
        In,
        Out,
    }

    public interface IPostEffector
    {
        void Fade(PostEffectType postEffectType, float time, Color color, FadeType fadeType, Ease ease = Ease.Linear);
    }
}