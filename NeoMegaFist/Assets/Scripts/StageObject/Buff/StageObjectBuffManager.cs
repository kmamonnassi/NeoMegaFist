using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace StageObject.Buff
{
    public class StageObjectBuffManager : MonoBehaviour, IStageObjectBuffManager
    {
        [Inject] private IBuffDB buffDB;

        private List<Buff> buffs = new List<Buff>();
        private StageObjectBase target;

        private void Start()
        {
            target = GetComponent<StageObjectBase>();
        }

        /// <summary>バフ追加</summary>
        public void Add(BuffData data)
        {
            foreach(Buff buff in buffs)
            {
                if(buff.ID == data.ID)
                {
                    //既に同じバフを得ている場合、加算される
                    buff.Add(data);
                    return;
                }
            }
            //バフがない場合、新規に作成される
            Buff newBuff = buffDB.Create(data.ID);
            newBuff.Initalize(data, target);
            buffs.Add(newBuff);
        }

        /// <summary>バフの削除</summary>
        public void Remove(BuffID id)
        {
            List<Buff> copy = new List<Buff>(buffs);
            foreach (Buff buff in copy)
            {
                if(buff.ID == id)
                {
                    buffs.Remove(buff);
                    return;
                }
            }
        }

        private void Update()
        {
            List<Buff> copy = new List<Buff>(buffs);
            foreach(Buff buff in copy)
            {
                buff.Update();
                if(buff.Duration <= 0)
                {
                    buffs.Remove(buff);
                    buff.CallRemove();
                }
            }
        }
    }
}