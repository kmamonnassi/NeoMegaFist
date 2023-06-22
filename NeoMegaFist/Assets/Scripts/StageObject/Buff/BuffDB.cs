using System;
using System.Collections.Generic;

namespace StageObject.Buff
{
    public class BuffDB : IBuffDB
    {
        private Dictionary<BuffID, Func<BuffBase>> db = new Dictionary<BuffID, Func<BuffBase>>();

        public BuffDB()
        {
            Add<Burn>(BuffID.Burn);
            Add<Poison>(BuffID.Poison);
            Add<Freeze>(BuffID.Freeze);
        }

        private void Add<T>(BuffID id) where T : BuffBase, new()
        {
            db.Add(id, () => new T());
        }

        public BuffBase Create(BuffID id)
        {
            BuffBase b = db[id]();
            return b;
        }
    }
}