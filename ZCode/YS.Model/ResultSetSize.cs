using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Model
{
    /// <summary>
    /// �������С����������ʵ��
    /// һ������Query��EnumField<ResultSetSize>
    /// </summary>
    public enum ResultSetSize
    {
        /// <summary>
        /// ����
        /// </summary>
        TINY,
        /// <summary>
        /// ��
        /// </summary>
        SIMPLE,
        /// <summary>
        /// ��׼����ͨ
        /// </summary>
        NORMAL,
        /// <summary>
        /// ȫ��
        /// </summary>
        ALL,
		/// <summary>
		/// �Զ��壬����������ֶ��������Ӧ����ֶΣ��޶�Ӧ�ĺ��Ե�
		/// </summary>
		CUSTOM,
        /// <summary>
        /// Ĭ�ϣ���DA�㷵�ؽ���ֶ�
        /// </summary>
        DEFAULT
    }
}
