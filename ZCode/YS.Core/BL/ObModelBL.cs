using Obsidian.Action;
using Obsidian.Edm;
using Obsidian.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Core.DAL;
using YS.Model;

namespace YS.Core.BLL
{
    /// <summary>
    /// ObModel业务逻辑类（接口方法类）
    /// 1.继承YS.Core.BLL.YingShengBL （基类YingShengBL封装了用户和权限）
    /// 2.需要引入Model层和对应的DA类
    /// 3.需要引入Obsidian.Action （接口基类）, Obsidian.Edm （实体）
    /// 4.引入Obsidian.Runtime （Logger写日志）
    /// 5.需要实现构造方法
    /// 6.命名规范：以BL结尾
    /// </summary>
    public class ObModelBL : YingShengBL
    {
        #region 字段属性（非必须，只为方便接口请求和返回参数的名称配置）
        private const string RESULT = "result";          //结果集参数名称（主要用于删除接口）
        private const string RESULT_FIELDS = "code,msg"; //结果集参数对应字段
        private const string QUERY = "qry";          //请求列表主参数名称（主要用于查询列表接口）
        private const string LISTATTR = "listAttr";  //返回分页参数名称
        private const string LISTATTR_FIELDS = "itemsCount,pageNo,pageSize,pagesCount";  //分页结果集字段
        private const int MIN_PAGESIZE = 10;     //默认分页大小
        private const int MAX_PAGESIZE = 100;    //最大分页大小
        private const string VERIFY_ERROR = "用户非法，无法进行此操作"; //用户非法操作时返回给请求端信息
        /********************/
        private const string ENTITY_REQ = "obModel";  //[请求]主参数名称（主要用于新增，修改，查询单实体，删除单实体等接口）
        private const string ENTITY_RES = "obModels"; //[返回]主参数名称（主要用于新增，修改，查询单实体，查询列表等接口；一般为“请求主参数名称+s”）
        //[返回]主参数名称对应字段名称（需要和ObModel实体ObModelInfo的属性别名一致；可按需控制返回给请求端那些字段）
        private const string ENTITY_FIELDS = "obId,obLevel,obName,obDescir,obEnabled,obMoney,obScore,obCreated";
        private const string ENTITY_STR = "[ObModel实体记录]";    //自定义日志记录的实体名称
        /********************/
        private const string RELATION_RES = "obRealtion";   //[返回]关联参数名称
        //[返回]关联参数名称对应字段名称（需要和ObRelation实体ObRelationInfo的属性别名一致；可按需控制返回给请求端那些字段）
        private const string RELATION_FIELDS = "rId,rName,enabled,created,obId";
        #endregion

        #region 构造方法（必须）
        public ObModelBL() : base() { }
        public ObModelBL(YsSession session) : base(session) { }
        #endregion

        #region 接口方法
        /**
         * 1.接口命名方式 [程序集别名.接口类别名.接口方法别名]
         *  除首字母外，接口命名是区分大小写的！
         *  程序集别名：bin目录下，配置文件AppConfig.xml中的apiAssemblies节点下的assembly节点的name属性
         *  接口类别名：业务逻辑类名称去掉BL后缀（ObModelBL => obModel）
         *  接口方法别名：如 add,update,get,list,delete等
         *  示例：ys.obModel.add; ys.obModel.get
         * 2.接口调用权限：配置文件AppConfig.xml中的apiServes节点下的serv节点
         * 3.接口执行权限：通过继承基类，使用YsSession进行用户角色判断
         */

        #region [新增单实体]
        public void Add(ActionRequest req, ActionResponse res)
        {
            string msg = "";
            try
            {
                //权限判断一，需要登录（用户或管理员）
                if (!Session.IsLogin)
                {
                    res.Error(VERIFY_ERROR);
                    return;
                }
                //权限判断二，必须是管理员
                /*
                if (!Session.IsAdmin)
                {
                    res.Error(VERIFY_ERROR);
                    return;
                }
                */

                ObModelInfo ei = null;
                //解析请求参数，转换为实体类（针对单一实体结构）
                ei = req.GetModelByNameOrFirst<ObModelInfo>(ENTITY_REQ);

                //解析请求参数（针对有规律的实体结构（同前缀的同构表））
                /****
                ActReqParam param = req.GetParamByNameOrFirst(ENTITY_REQ);
                if (param == null)
                {
                    res.Error("参数" + ENTITY_REQ + "错误");
                    return;
                }
                //定义参数（对于DateTime类型，以"yyyy-MM-dd HH:mm:ss"格式的字符串处理）
                long obId = 0;
                int obLevel = 0;
                string obName = "";
                string obDescri = "";
                bool obEnabled = false;
                decimal obMoney = 0.0m;
                double obScore = 0.0d;
                long userId = 0;
                //验证参数（参数名必须和ObModel实体ObModelInfo的属性别名一致）
                if (!param.TryGetFirstLong("obId", out obId))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性obId错误");
                    return;
                }
                if (!param.TryGetFirstInt("obLevel", out obLevel))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性obLevel错误");
                    return;
                }
                if (!param.TryGetFirstString("obName", out obName))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性obName错误");
                    return;
                }
                if (!param.TryGetFirstString("obDescri", out obDescri))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性obDescri错误");
                    return;
                }
                if (!param.TryGetFirstBool("obEnabled", out obEnabled))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性obEnabled错误");
                    return;
                }
                if (!param.TryGetFirstDecimal("obMoney", out obMoney))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性obMoney错误");
                    return;
                }
                if (!param.TryGetFirstDouble("obScore", out obScore))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性obScore错误");
                    return;
                }
                if (!param.TryGetFirstLong("userId", out userId))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性userId错误");
                    return;
                }
                //设置实体参数值
                long uid = userId % 1024;
                string tableNameFormat = @"TbObModel_{0}"; //同前缀的同构表名格式定义
                string tableName = String.Format(tableNameFormat, uid);
                ei = new ObModelInfo(tableName);
                ei.ObLevel.Set(obLevel);
                ei.ObName.Set(obName);
                ei.ObDescri.Set(obDescri);
                ei.ObEnabled.Set(obEnabled);
                ei.ObMoney.Set(obMoney);
                ei.ObScore.Set(obScore);
                ei.UserId.Set(userId);
                ****/

                //调用业务处理方法
                ei = AddEntity(ei, out msg);
                if (ei == null)
                {
                    res.Error(msg);
                    return;
                }
                //返回结果集
                ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS); //定义返回结果集名称和字段名
                ar.AddModel(ei);    //添加结果集到ActionResult
            }
            catch (Exception ex)
            {
                msg = "ys.ObModel.add 接口调用异常";
                Logger.Error(ex, msg);
                res.Error(msg);
            }
        }
        /// <summary>
        /// 添加实体方法
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ObModelInfo AddEntity(ObModelInfo entity, out string msg)
        {
            ObModelInfo ei = null;
            try
            {
                //验证必要的属性值
                if (!Verify(entity, out msg))
                {
                    return null;
                }
                //判断是否已存在（如同名）
                /* 对于同一userId只能有一条记录的情况
                 * 在DA类增加 SelectByUserId(entity.UserId.Value) 方法，然后进行判断即可
                 * 
                */
                ObModelDA da = new ObModelDA();
                ei = da.SelectByName(entity.ObName.Value);
                if (ei != null)
                {
                    msg = "obName已存在";
                    return null;
                }

                //设置必要的空值属性初始值（对于数据库表字段要求 NOT NULL 的都必须设置）
                ei = entity;
                if (entity.ObDescri.IsNullOrWhiteSpace)
                    ei.ObDescri.Set("");
                if (entity.ObEnabled.IsNull)
                    ei.ObEnabled.Set(false);
                if (entity.ObLevel.IsNull || entity.ObLevel.Value < 0)
                    ei.ObLevel.Set(0);
                if (entity.ObMoney.IsNull || entity.ObMoney.Value < 0.0m)
                    ei.ObMoney.Set(0.0m);
                if (entity.ObScore.IsNull || entity.ObScore.Value < 0.0d)
                    ei.ObScore.Set(60.0d);
                ei.ObCreated.Now(); //设置当前日期时间
                ei.Save();  //保存实体（新增记录）
            }
            catch (Exception ex)
            {
                msg = "添加" + ENTITY_STR + "异常";
                Logger.Error(ex, msg);
                return null;
            }
            msg = "";
            return ei;
        }
        #endregion

        #region [更新单实体]
        public void Update(ActionRequest req, ActionResponse res)
        {
            string msg = "";
            try
            {
                //权限判断一，需要登录（用户或管理员）
                if (!Session.IsLogin)
                {
                    res.Error(VERIFY_ERROR);
                    return;
                }
                YsMemberInfo user = Session.User;   //获取登录用户信息
                //权限判断二，必须是管理员
                /*
                if (!Session.IsAdmin)
                {
                    res.Error(VERIFY_ERROR);
                    return;
                }
                */

                ObModelInfo ei = null;
                //解析请求参数，转换为实体类
                ei = req.GetModelByNameOrFirst<ObModelInfo>(ENTITY_REQ);

                //解析请求参数（针对有规律的实体结构（同前缀的同构表））
                //参照 ： public void Add(ActionRequest req, ActionResponse res) 方法

                //调用业务处理方法一（用户无关）
                ei = UpdateEntity(ei, out msg);

                /*
                //调用业务处理方法二（用户相关，操作权限判断）
                ei = UpdateEntity(ei, user, out msg);
                */

                if (ei == null)
                {
                    res.Error(msg);
                    return;
                }
                //返回结果集
                ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS); //定义返回结果集名称和字段名
                ar.AddModel(ei);    //添加结果集到ActionResult
            }
            catch (Exception ex)
            {
                msg = "ys.ObModel.update 接口调用异常";
                Logger.Error(ex, msg);
                res.Error(msg);
            }
        }
        /// <summary>
        /// 更新实体方法（用户无关）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ObModelInfo UpdateEntity(ObModelInfo entity, out string msg)
        {
            ObModelInfo ei = null;
            try
            {
                //判断参数中是否有记录ID
                if (entity.ObId.IsNull)
                {
                    msg = "obId不能为空";
                    return null;
                }
                //判断是否存在ID对应的记录
                ObModelDA da = new ObModelDA();
                ei = da.SelectById(entity.ObId.Value);  //根据ID获取记录
                //ei = Obsidian.Edm.OModel.GetByPk<ObModelInfo>(entity.ObId.Value); //或者：根据主键获取记录
                if (ei != null)
                {
                    msg = "记录不存在";
                    return null;
                }
                //设置需要更新的属性值（注意与添加的代码进行区分）
                ei.ResetAssigned();

                if (!entity.ObDescri.IsNullOrWhiteSpace)
                    ei.ObDescri.Set(entity.ObDescri.Value);
                if (!entity.ObEnabled.IsNull)
                    ei.ObEnabled.Set(entity.ObEnabled.Value);
                if (!entity.ObLevel.IsNull)
                    ei.ObLevel.Set(entity.ObLevel.Value);
                if (!entity.ObMoney.IsNull)
                    ei.ObMoney.Set(entity.ObMoney.Value);
                if (!entity.ObScore.IsNull)
                    ei.ObScore.Set(entity.ObScore.Value);

                if (!ei.Update())  //保存实体（更新记录）
                {
                    msg = "更新" + ENTITY_STR + "失败";
                    return null;
                }
            }
            catch (Exception ex)
            {
                msg = "更新" + ENTITY_STR + "异常";
                Logger.Error(ex, msg);
                return null;
            }
            msg = "";
            return ei;
        }
        /// <summary>
        /// 更新实体方法（用户相关）
        /// 1.判断是否管理员
        /// 2.判断记录是否属于当前用户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ObModelInfo UpdateEntity(ObModelInfo entity, YsMemberInfo user, out string msg)
        {
            ObModelInfo ei = null;
            try
            {
                //判断参数中是否有记录ID
                if (entity.ObId.IsNull)
                {
                    msg = "obId不能为空";
                    return null;
                }
                //判断是否存在ID对应的记录
                ObModelDA da = new ObModelDA();
                ei = da.SelectById(entity.ObId.Value);  //根据ID获取记录
                //ei = OModel.GetByPk<ObModelInfo>(entity.ObId.Value); //或者：根据主键获取记录
                if (ei != null)
                {
                    msg = "记录不存在";
                    return null;
                }

                //判断是否有操作权限（管理员或用户本人）
                if (user != null
                    && (!user.UserId.IsNull)
                    && (!ei.UserId.IsNull))
                {
                    if (user.AccountType.Value != Oak.Model.AccountType.Admin)  //非管理员
                    {
                        if (user.UserId.Value != ei.UserId.Value)
                        {
                            msg = VERIFY_ERROR;
                            return null;
                        }
                    }
                }
                else
                {
                    msg = VERIFY_ERROR;
                    return null;
                }

                //设置需要更新的属性值（注意与添加的代码进行区分）
                ei.ResetAssigned();

                if (!entity.ObDescri.IsNullOrWhiteSpace)
                    ei.ObDescri.Set(entity.ObDescri.Value);
                if (!entity.ObEnabled.IsNull)
                    ei.ObEnabled.Set(entity.ObEnabled.Value);
                if (!entity.ObLevel.IsNull)
                    ei.ObLevel.Set(entity.ObLevel.Value);
                if (!entity.ObMoney.IsNull)
                    ei.ObMoney.Set(entity.ObMoney.Value);
                if (!entity.ObScore.IsNull)
                    ei.ObScore.Set(entity.ObScore.Value);

                if (!ei.Update())  //保存实体（更新记录）
                {
                    msg = "更新" + ENTITY_STR + "失败";
                    return null;
                }
            }
            catch (Exception ex)
            {
                msg = "更新" + ENTITY_STR + "异常";
                Logger.Error(ex, msg);
                return null;
            }
            msg = "";
            return ei;
        }
        #endregion

        #region [获取单实体]
        public void Get(ActionRequest req, ActionResponse res)
        {
            string msg = "";
            try
            {
                //权限判断，是否有登录（根据业务确定是否需要此判断）
                /*
                if (!Session.IsLogin) // (!Session.IsAdmin)
                {
                    res.Error(VERIFY_ERROR);
                    return;
                }
                */

                ActReqParam param;
                if (!req.TryGetParam(ENTITY_REQ, out param))
                {
                    res.Error("参数" + ENTITY_REQ + "错误");
                    return;
                }
                long obId = 0;
                if (!param.TryGetFirstLong("obId", out obId))
                {
                    res.Error("参数" + ENTITY_REQ + "属性obId错误");
                    return;
                }

                //用户相关
                /*
                long userId = 0;
                if (!param.TryGetFirstLong("userId", out userId))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性userId错误");
                }
                 */

                ObModelInfo ei = null;
                ei = GetEntity(obId, out msg);
                //ei = GetEntity(obId, userId, out msg); //用户相关

                if (ei == null)
                {
                    res.Error(msg);
                    return;
                }
                ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS);
                ar.AddModel(ei);
            }
            catch (Exception ex)
            {
                msg = "ext.ObModel.get 接口调用异常";
                Logger.Error(ex, msg);
                res.Error(msg);
            }
        }
        /// <summary>
        /// 获取记录（根据记录ID）
        /// </summary>
        /// <param name="obId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ObModelInfo GetEntity(long obId, out string msg)
        {
            ObModelInfo ei = null;
            try
            {
                ObModelDA da = new ObModelDA();
                ei = da.SelectById(obId);
                //ei = OModel.GetByPk<ObModelInfo>(entity.ObId.Value); //或者：根据主键获取记录
                if (ei == null)
                {
                    msg = "获取" + ENTITY_STR + "记录为空";
                    return null;
                }
            }
            catch (Exception ex)
            {
                msg = "获取" + ENTITY_STR + "记录异常";
                Logger.Error(ex, msg);
                return null;
            }
            msg = "";
            return ei;
        }
        /// <summary>
        /// 获取记录（根据记录ID和用户ID）
        /// </summary>
        /// <param name="obId"></param>
        /// <param name="userId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ObModelInfo GetEntity(long obId, long userId, out string msg)
        {
            ObModelInfo ei = null;
            try
            {
                ObModelDA da = new ObModelDA();
                ei = da.SelectByUserId(obId, userId);
                if (ei == null)
                {
                    msg = "获取" + ENTITY_STR + "记录为空";
                    return null;
                }
            }
            catch (Exception ex)
            {
                msg = "获取" + ENTITY_STR + "记录异常";
                Logger.Error(ex, msg);
                return null;
            }
            msg = "";
            return ei;
        }
        #endregion

        #region [获取实体列表]
        public void List(ActionRequest req, ActionResponse res)
        {
            string msg = "";
            ListAttrInfo la;
            try
            {
                if (!Session.IsAdmin)   //权限判断，是管理员
                {
                    res.Error(VERIFY_ERROR);
                    return;
                }

                ObModelQuery qry = req.GetModelByNameOrFirst<ObModelQuery>(QUERY);
                List<ObModelInfo> list = GetList(qry, out la, out msg);
                if (list == null)
                {
                    res.Error(msg);
                    return;
                }

                ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS);
                ar.AddModels<ObModelInfo>(list);

                ActionResult arAttr = res.AddResult(LISTATTR, LISTATTR_FIELDS);
                arAttr.AddModel(la);

                //获取关联结果集
                if (qry.GetRelation.IsTrue())
                {
                    ActionResult arRe = res.AddResult(RELATION_RES, RELATION_FIELDS);
                    if (list.Count > 0)
                    {
                        //获取ObModel的obId串
                        List<long> obIds = new List<long>();
                        for(int i=0,j=list.Count;i<j;i++)
                        {
                            obIds.Add(list[i].ObId.Value);
                        }
                        //调用ObRelation实体对应的BL类中的获取列表方法
                        //  ObRelationInfo 中属性 ObId 对应 ObModelInfo 的 ObId
                        //  RelationQuery 中属性 ObIds 对应 ObModelInfo 的 ObId （多个）
                        /*
                        RelationBL rbl = new RelationBL();
                        RelationQuery rqry = new RelationQuery();
                        ListAttrInfo rla = new ListAttrInfo();
                        rqry.PageNo = 1;
                        rqry.PageSize = list.Count; //
                        rqry.ObIds.Set(obIds);
                        List<ObRelationInfo> rList = rbl.GetList(rqry, out rla, out msg);
                        arRe.AddModels<ObRelationInfo>(rList);
                        */
                    }
                    else
                    {
                        arRe.AddModels<ObRelationInfo>(new List<ObRelationInfo>());
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "ys.obModel.list 接口调用异常";
                Logger.Error(ex, msg);
                res.Error(msg);
            }
        }
        public List<ObModelInfo> GetList(ObModelQuery query, out ListAttrInfo listAttr, out string msg)
        {
            List<ObModelInfo> list = null;
            try
            {
                if (!query.CheckPagingAttrs(MIN_PAGESIZE, MAX_PAGESIZE, out msg))
                {
                    listAttr = null;
                    return null;
                }

                ObModelDA da = new ObModelDA();
                list = da.SelectList(query, out listAttr);
                if (list == null)
                {
                    msg = "获取" + ENTITY_STR + "列表为空";
                    listAttr = null;
                    return null;
                }
            }
            catch (Exception ex)
            {
                msg = "获取" + ENTITY_STR + "列表异常";
                Logger.Error(ex, msg);
                listAttr = null;
                return null;
            }
            return list;
        }
        #endregion

        #region [删除单实体]
        public void Delete(ActionRequest req, ActionResponse res)
        {
            string msg = "";
            try
            {
                //权限判断，是否有登录（根据业务确定是否 限制为管理员）
                if (!Session.IsLogin) // (!Session.IsAdmin)
                {
                    res.Error(VERIFY_ERROR);
                    return;
                }

                ActReqParam param;
                if (!req.TryGetParam(ENTITY_REQ, out param))
                {
                    res.Error("参数" + ENTITY_REQ + "错误");
                    return;
                }
                int obId = 0;
                if (!param.TryGetInt(0, "obId", out obId))
                {
                    res.Error("参数" + ENTITY_REQ + "属性obId错误");
                    return;
                }

                //用户相关
                /*
                long userId = 0;
                if (!param.TryGetLong(0, "userId", out userId))
                {
                    res.Error("参数" + ENTITY_REQ + "的属性userId错误");
                }
                */

                int code = 0;
                code = DeleteEntity(obId, out msg);
                //code = DeleteEntity(obId, userId, out msg);   //用户相关

                ActionResult ar = res.AddResult(RESULT, RESULT_FIELDS);
                ar.AddValues(code, msg);
            }
            catch (Exception ex)
            {
                msg = "ext.obModel.delete 接口调用异常";
                Logger.Error(ex, msg);
                res.Error(msg);
            }
        }

        public int DeleteEntity(long obId, out string msg)
        {
            int code = 0;
            try
            {
                ObModelInfo ei = GetEntity(obId, out msg);
                if(ei == null)
                {
                    return 0;
                }
                if (ei.Delete())
                {
                    code = 1;
                }
                else
                {
                    msg = "删除" + ENTITY_STR + "失败";
                    return code;
                }
            }
            catch (Exception ex)
            {
                msg = "删除" + ENTITY_STR + "异常";
                Logger.Error(ex, msg);
                return -1;
            }
            msg = "";
            return code;
        }
        public int DeleteEntity(long obId, long userId, out string msg)
        {
            int code = 0;
            try
            {
                ObModelInfo ei = GetEntity(obId, userId, out msg);
                if (ei == null)
                {
                    return 0;
                }
                if (ei.Delete())
                {
                    code = 1;
                }
                else
                {
                    msg = "删除" + ENTITY_STR + "失败";
                    return code;
                }
            }
            catch (Exception ex)
            {
                msg = "删除" + ENTITY_STR + "异常";
                Logger.Error(ex, msg);
                return -1;
            }
            msg = "";
            return code;
        }
        #endregion

        #endregion

        #region 辅助方法
        /// <summary>
        /// 验证请求实体参数
        /// 一般用于新增实体
        /// </summary>
        /// <param name="om"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool Verify(ObModelInfo om, out string msg)
        {
            if (om.ObName.IsNullOrWhiteSpace)
            {
                msg = "ObName不能为空";
                return false;
            }

            msg = "";
            return true;
        }
        #endregion
    }
}
