using System;
using System.Collections.Generic;

namespace StageObject.Buff
{
    public class BuffDB : IBuffDB
    {
        private Dictionary<BuffID, Func<Buff>> db = new Dictionary<BuffID, Func<Buff>>();

        public BuffDB()
        {
        }

        private void Add<T>(BuffID id) where T : Buff, new()
        {
            db.Add(id, () => new T());
        }

        public Buff Create(BuffID id)
        {
            Buff b = db[id]();
            return b;
        }
    }
}