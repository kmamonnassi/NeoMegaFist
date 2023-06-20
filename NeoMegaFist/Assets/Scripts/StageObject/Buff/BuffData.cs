using UnityEngine;

namespace StageObject.Buff
{
    [System.Serializable]
    public class BuffData
    {
        [SerializeField] private BuffID id;
        [SerializeField] private float duration;
        [SerializeField] private int value;

        public BuffID ID => id;
        public float Duration => duration;
        public int Value => value;

        public BuffData(BuffID id, int stack, float duration, int value)
        {
            this.id = id;
            this.duration = duration;
            this.value = value;
        }
    }
}