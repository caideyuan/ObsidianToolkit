using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Model
{
    /// <summary>
    /// ״̬ö�٣���������ʵ��
    /// һ������Query��EnumField<StatusType>
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// ����
        /// </summary>
        NORMAL,
        /// <summary>
        /// ɾ��
        /// </summary>
        DELETE,
        /// <summary>
        /// ��ɾ��
        /// </summary>
        NOTDEL,
		/// <summary>
        /// ȱʡĬ��
        /// </summary>
		DEFAULT,
        /// <summary>
        /// ȫ��
        /// </summary>
        ALL	
    }
}
