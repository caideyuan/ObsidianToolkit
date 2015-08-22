using Obsidian.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Model
{
    /// <summary>
    /// ObModel权限实体
    /// </summary>
    public class ObModelPermission : OModel
    {
        private BoolField canCreate;
        /// <summary>
        /// 是否可以创建
        /// </summary>
        public BoolField CanCreate
        {
            get { return canCreate; }
        }

        private BoolField canRead;
        /// <summary>
        /// 是否可以读取
        /// </summary>
        public BoolField CanRead
        {
            get { return canRead; }
        }

        private BoolField canUpdate;
        /// <summary>
        /// 是否可以更新
        /// </summary>
        public BoolField CanUpdate
        {
            get { return canUpdate; }
        }

        private BoolField canDelete;
        /// <summary>
        /// 是否可以删除
        /// </summary>
        public BoolField CanDelete
        {
            get { return canDelete; }
        }

        public ObModelPermission()
        {
            base.InitModel(new IModelField[] {
                canCreate = new BoolField(this,null,"canCreate"),
                canRead = new BoolField(this,null,"canRead"),
                canUpdate = new BoolField(this,null,"canUpdate"),
                canDelete = new BoolField(this,null,"canDelete"),
            });
        }
    }
}
