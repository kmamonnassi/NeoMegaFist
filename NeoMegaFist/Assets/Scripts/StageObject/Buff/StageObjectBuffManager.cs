using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Zenject;

namespace StageObject.Buff
{
    public class StageObjectBuffManager : MonoBehaviour, IStageObjectBuffManager, IUpdate
    {
        [Inject] private IBuffDB buffDB;

        public event Action<BuffBase> OnAdd; 
        public event Action<BuffBase> OnRemove;

        private List<BuffBase> buffs = new List<BuffBase>();
        private StageObjectBase target;

        private void Start()
        {
            target = GetComponent<StageObjectBase>();
        }

        /// <summary>バフ追加</summary>
        public void Add(BuffData data)
        {
            BuffBase newBuff = buffDB.Create(data.ID);
            newBuff.Initalize(data, target);
            buffs.Add(newBuff);
            OnAdd?.Invoke(newBuff);
        }

        /// <summary>バフの削除</summary>
        public void Remove(BuffID id)
        {
            List<BuffBase> copy = new List<BuffBase>(buffs);
            foreach (BuffBase buff in copy)
            {
                if(buff.ID == id)
                {
                    buffs.Remove(buff);
                    OnRemove?.Invoke(buff);
                    return;
                }
            }
        }

        public void ManagedUpdate()
        {
            List<BuffBase> copy = new List<BuffBase>(buffs);
            foreach (BuffBase buff in copy)
            {
                buff.Update();
                if (buff.Duration <= 0)
                {
                    buffs.Remove(buff);
                    buff.CallRemove();
                }
            }
        }
    }
}