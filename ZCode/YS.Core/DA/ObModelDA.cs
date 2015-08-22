using Obsidian.Data.Sql;
using Obsidian.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Model;

namespace YS.Core.DAL
{
    /// <summary>
    /// ObModel数据访问类
    /// 1.需要引入Model层
    /// 2.需要引入Obsidian.Data.Sql
    /// 3.需要引入Obsidian.Edm
    /// 4.方法统一使用internal修饰符（只能在同一程序集内访问）
    /// 5.命名规范：以DA结尾
    /// </summary>
    public class ObModelDA
    {
        #region SELECT
        internal ObModelInfo SelectById(long obId)
        {
            //初始化
            ObModelInfo om = new ObModelInfo();
            IDbHandler dbh = om.CreateHandler();    //创建数据库操作句柄接口
            //设置返回的实体字段，相当于EF的Select字段
            dbh.SetFields(om.Fields);   //om.Fields为所有字段
            //dbh.SetFields(om.ObId, om.ObName, om.ObEnabled);    //自定义返回的实体字段

            //组装Where条件语句
            dbh.Where(om.ObId.Equals(obId));

            //执行查询
            DataReader dr = dbh.Select();

            //获取结果集方法一（自动关闭读取器）
            om = dr.ReadFirst<ObModelInfo>();

            //获取结果集方法二（手动关闭读取器，更灵活）
            /*
            if (dr.Read())
            {
                dr.ReadTo(om);
            }
            else
            {
                om = null;
            }
            dr.Close();
            */

            return om;
        }

        internal ObModelInfo SelectByName(string obName)
        {
            ObModelInfo om = new ObModelInfo();
            IDbHandler dbh = om.CreateHandler();
            dbh.SetFields(om.Fields);

            dbh.Where(om.ObName.Like(obName));

            DataReader dr = dbh.Select();
            om = dr.ReadFirst<ObModelInfo>();

            return om;
        }

        internal ObModelInfo SelectByUserId(long obId,long userId)
        {
            ObModelInfo om = new ObModelInfo();
            IDbHandler dbh = om.CreateHandler();
            dbh.SetFields(om.Fields);

            dbh.Where(om.ObId.Equals(obId));
            dbh.Where(om.UserId.Equals(userId));

            DataReader dr = dbh.Select();
            om = dr.ReadFirst<ObModelInfo>();

            return om;
        }

        internal List<ObModelInfo> SelectList(ObModelQuery query, out ListAttrInfo listAttr)
        {
            //初始化
            ObModelInfo om = new ObModelInfo();
            IDbHandler dbh = om.CreateHandler();    //创建数据库操作句柄接口
            dbh.SetFields(om.Fields);   //设置返回的实体字段

            //[BEGIN]::组装Where条件语句
            //组装日期时间条件
            string startTime;   //开始时间
            string endTime;     //结束时间
            string dtFormat = "yyyy-MM-dd HH:mm:ss";
            if (!query.StartTime.IsNull && (!query.StartTime.IsMinValue))
            {
                startTime = query.StartTime.Value.ToString(dtFormat);
                dbh.Where(om.ObCreated.Compare(">=", startTime));
            }
            if (!query.EndTime.IsNull && (!query.EndTime.IsMinValue))
            {
                endTime = query.EndTime.Value.ToString(dtFormat);
                dbh.Where(om.ObCreated.Compare("<=", endTime));
            }
            //组装记录ID串条件
            if(!query.Ids.IsNull && query.Ids.Count > 0)
            {
                dbh.Where(om.ObId.In(query.Ids.Value));
            }
            //组装关键字条件
            if(!query.Keyword.IsNullOrWhiteSpace)
            {
                dbh.Where(om.ObName.Like(query.Keyword.Value));
            }
            //组装复合条件（如果需要）
            /*
            if (!query.Keyword.IsNullOrWhiteSpace)
            {
                string keyword = query.Keyword.Value;
                CompoundCondition ccd = new CompoundCondition();    //复合条件类
                ccd.And(om.ObName.Like(keyword))
                    .Or(om.ObDescri.Like(keyword));
                dbh.Where(ccd);
            }
            */

            //
            if(!query.Level.IsNull)
            {
                dbh.Where(om.ObLevel.Equals(query.Level));
            }
            //组装记录状态条件
            if(!query.Enabled.IsNull)
            {
                dbh.Where(om.ObEnabled.Equals(query.Enabled.Value));
            }

            //组装JOIN条件
            if (!query.GetRelation.IsTrue())
            {
                ObRelationInfo ori = new ObRelationInfo();
                dbh.SetFields(ori.Fields);  //设置返回的关联实体字段
                dbh.Join(ori).On(om.ObId.Equals(ori.ObId)); //关联条件
            }

            //组装排序条件（放在Where子句最后，分页时，必须有OrderBy）
            if(!query.OrderBy.IsNull && query.OrderBy.Count>0)
            {
                OrderInfo[] oiArray = query.OrderBy.GetArray();
                //拼装的order by条件，case条件字段值需要和ObModelQuery查询实体的OrderByFields属性一致
                foreach (OrderInfo oi in oiArray)
                {
                    switch (oi.FieldAlias)
                    {
                        case "obLevel":
                            dbh.OrderBy(om.ObLevel, oi.OrderType);
                            break;
                        case "obName":
                            dbh.OrderBy(om.ObName, oi.OrderType);
                            break;
                        case "obEnabled":
                            dbh.OrderBy(om.ObEnabled, oi.OrderType);
                            break;
                        case "obMoney":
                            dbh.OrderBy(om.ObMoney, oi.OrderType);
                            break;
                        case "obScore":
                            dbh.OrderBy(om.ObScore, oi.OrderType);
                            break;
                        case "obCreated":
                            dbh.OrderBy(om.ObCreated, oi.OrderType);
                            break;
                        case "obId":
                        default:
                            dbh.OrderBy(om.ObId, oi.OrderType);
                            break;
                    }
                }
            }
            else
            {
                //默认排序条件
                dbh.OrderBy(om.ObId, OrderType.DESC);
            }

            //[END]::组装Where条件语句

            List<ObModelInfo> list = new List<ObModelInfo>();
            DataReader dr = dbh.Select(query, out listAttr);
            //获取结果集方法一（自动关闭读取器）
            list = dr.ReadList<ObModelInfo>();

            //获取结果集方法二（手动关闭读取器，更灵活）
            
            while (dr.Read())
            {
                ObModelInfo ei = new ObModelInfo();
                dr.ReadTo(ei);
                list.Add(ei);
            }
            dr.Close();

            return list;
        }
        #endregion
    }
}
