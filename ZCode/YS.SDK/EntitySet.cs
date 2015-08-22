
using System;
using System.Collections.Generic;
using System.Text;

namespace YS.SDK
{
    public class EntitySet
    {

        private const string NULL_VALUE_KEY = "__null_key__";
        private const string NULL_VALUE = "__null_val__";

        private List<Entity> listEntity = new List<Entity>();
        private Dictionary<string, Dictionary<object, List<Entity>>> dictEntity = null;


        public EntitySet()
        {
            this.listEntity = new List<Entity>();
        }

        public EntitySet(List<Entity> list)
        {
            this.listEntity = list;
        }

        public Entity this[int index]
        {
            get { return listEntity[index]; }
        }

        public List<Entity> GetList()
        {
            return listEntity;
        }

        public List<Entity> GetList(string name, object val)
        {
            if (val == null)
                val = NULL_VALUE;
            
            if (dictEntity == null)
            {
                dictEntity = new Dictionary<string, Dictionary<object, List<Entity>>>();
                foreach (Entity entity in listEntity)
                    this.SetEntityDict(entity);
            }

            Dictionary<object, List<Entity>> dict;
            if (!dictEntity.TryGetValue(name, out dict))
                return null;

            object key = val == null ? NULL_VALUE_KEY : val;
            List<Entity> list;
            if (!dict.TryGetValue(key, out list))
                return new List<Entity>();

            return list;
        }

        public Entity Get(string name, object val, int index)
        {
            if (val == null)
                val = NULL_VALUE;

            List<Entity> list = this.GetList(name, val);
            return index > list.Count - 1 ? null : list[index];
        }


        public Entity First
        {
            get
            {
                return listEntity.Count > 0 ? listEntity[0] : null;
            }
        }

        public Entity GetFirst(string name, object val)
        {
            if (val == null)
                val = NULL_VALUE;

            Entity o = this.Get(name, val, 0);
            return o;
        }

        public EntitySet GetEntitySet(string name, object val)
        {
            if (val == null)
                val = NULL_VALUE;

            List<Entity> entityList = this.GetList(name, val);
            EntitySet es = new EntitySet(entityList);
            return es;
        }

        /// <summary>
        /// 实体关联
        /// </summary>
        /// <param name="filedName">新建属性名称</param>
        /// <param name="entitySet">目标实体集</param>
        /// <param name="relateFiledName">关联属性名称</param>
        /// <param name="targetFiledName">关联目标实体集属性名称</param>
        /// <param name="relateType">关联类型</param>
        public void Relate(string filedName, EntitySet entitySet, string relateFiledName, string targetFiledName, EntityRelateType relateType)
        {
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                Entity o = this[i];
                object val = o.Get(relateFiledName);
                if (relateType == EntityRelateType.OneToOne)
                {
                    o.Set(filedName, entitySet.GetFirst(targetFiledName, val));
                }
                else if (relateType == EntityRelateType.OneToMany)
                {
                    o.Set(filedName, entitySet.GetEntitySet(targetFiledName, val));
                }
            }
        }

        public bool TryGet(string name, object val, int index, Entity entity)
        {
            if (val == null)
                val = NULL_VALUE;

            List<Entity> list = this.GetList(name, val);
            if (list == null || index > list.Count - 1)
            {
                entity = null;
                return false;
            }
            entity = list[index];
            return true;
        }

        public void Add(Entity entity)
        {
            if (dictEntity != null)
            {
                this.SetEntityDict(entity);
            }
            listEntity.Add(entity);
        }

        public void Remove(Entity entity)
        {
            listEntity.Remove(entity);
            if (dictEntity == null)
                return;
            Dictionary<string, object>.KeyCollection keys = entity.Keys;
            Dictionary<object, List<Entity>> dict;
            List<Entity> list;
            foreach (string key in keys)
            {
                if (!dictEntity.TryGetValue(key, out dict))
                    continue;
                object val = entity.Get(key);
                if (!dict.TryGetValue(val, out list))
                    continue;
                list.Remove(entity);
            }
        }

        public void RemoveAt(int index)
        {
            Entity entity = listEntity[index];
            this.Remove(entity);
        }

        public int Count
        {
            get { return listEntity.Count; }
        }

        private void SetEntityDict(Entity entity)
        {
            Dictionary<string, object>.KeyCollection keys = entity.Keys;
            Dictionary<object, List<Entity>> dict;
            List<Entity> list;
            foreach (string key in keys)
            {
                if (!dictEntity.TryGetValue(key, out dict))
                {
                    dict = new Dictionary<object, List<Entity>>();
                    dictEntity.Add(key, dict);
                }
                object val = entity.Get(key);
                if (val == null)
                    val = NULL_VALUE;
                if (!dict.TryGetValue(val, out list))
                {
                    list = new List<Entity>();
                    dict.Add(val, list);
                }
                list.Add(entity);
            }
        }

        public string ToJsonString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            bool first = true;
            foreach (Entity o in this.listEntity)
            {
                if (first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(o.ToJsonString());
            }
            sb.Append("]");
            return sb.ToString();
        }

    }


    public enum EntityRelateType
    {
        /// <summary>
        /// 一对一
        /// </summary>
        OneToOne,
        /// <summary>
        /// 一对多
        /// </summary>
        OneToMany
    }
}
