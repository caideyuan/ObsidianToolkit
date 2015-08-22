using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动生成代码类
    /// 1.Model,Query,Permit
    /// 2.DAL,BLL
    /// 3.ASPX,JS
    /// 4.SDK,Ctrl,View
    /// 5.SQL
    /// 6.Descri
    /// </summary>
    public class AutoCoder
    {
        private AutoObject ao;
        private List<FieldObject> fdos;

        public AutoCoder(AutoObject ao, DataTable fdDT)
        {
            this.ao = ao;
            fdos = DataTableToFieldObject(fdDT);
        }

        /// <summary>
        /// 1.Model
        /// </summary>
        /// <returns></returns>
        public string CreateModelCode()
        {
            string cs = "";
            AutoModel am = new AutoModel();
            cs = am.CreateCodeString(ao, fdos);
            return cs;
        }
        /// <summary>
        /// 1.Query
        /// </summary>
        /// <returns></returns>
        public string CreateQueryCode()
        {
            string cs = "";
            AutoQuery aq = new AutoQuery();
            cs = aq.CreateCodeString(ao, fdos);
            return cs;
        }
        /// <summary>
        /// 1.Permition
        /// </summary>
        /// <returns></returns>
        public string CreatePermitCode()
        {
            string cs = "";
            AutoPermit ap = new AutoPermit(ao, fdos);
            cs = ap.CreateCodeString();
            return cs;
        }
        /// <summary>
        /// 2.DAL
        /// </summary>
        /// <returns></returns>
        public string CreateDALCode()
        {
            string cs = "";
            AutoDA ad = new AutoDA();
            cs = ad.CreateCodeString(ao, fdos);
            return cs;
        }
        /// <summary>
        /// 2.BLL
        /// </summary>
        /// <returns></returns>
        public string CreateBLLCode()
        {
            string cs = "";
            AutoBL ab = new AutoBL(ao, fdos);
            cs = ab.CreateCodeString();
            return cs;
        }
        
        /// <summary>
        /// 3.ASPX
        /// </summary>
        /// <returns></returns>
        public string CreateASPXCode()
        {
            string cs = "";
            AutoAspx apx = new AutoAspx(ao, fdos);
            cs = apx.CreateCodeString();
            return cs;
        }
        /// <summary>
        /// 3.JS
        /// </summary>
        /// <returns></returns>
        public string CreateJSCode()
        {
            string cs = "";
            AutoJs aj = new AutoJs(ao,fdos);
            cs = aj.CreateCodeString();
            return cs;
        }
        /// <summary>
        /// 4.SDK
        /// </summary>
        /// <returns></returns>
        public string CreateSDKCode()
        {
            string cs = "";
            AutoSDK asdk = new AutoSDK(ao, fdos);
            cs = asdk.CreateCodeString();
            return cs;
        }
        /// <summary>
        /// 4.Ctrl
        /// </summary>
        /// <returns></returns>
        public string CreateCtrlCode()
        {
            string cs = "";
            AutoCtrl ac = new AutoCtrl(ao, fdos);
            cs = ac.CreateCodeString();
            return cs;
        }
        /// <summary>
        /// 4.View
        /// </summary>
        /// <returns></returns>
        public string CreateViewCode()
        {
            string cs = "";
            AutoView av = new AutoView(ao, fdos);
            cs = av.CreateCodeString();
            return cs;
        }
        /// <summary>
        /// 5.SQL
        /// </summary>
        /// <returns></returns>
        public string CreateSQLCode()
        {
            string cs = "";
            AutoSql asql = new AutoSql(ao, fdos);
            cs = asql.CreateCodeString();
            return cs;
        }
        /// <summary>
        /// 6.Description
        /// </summary>
        /// <returns></returns>
        public string CreateDescription()
        {
            string cs = "";
            AutoDescri ad = new AutoDescri(ao, fdos);
            cs = ad.CreateCodeString();
            return cs;
        }

        ///////////////////////////////////

        /// <summary>
        /// 表字段转换为字段实体类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<FieldObject> DataTableToFieldObject(DataTable dt)
        {
            List<FieldObject> list = new List<FieldObject>();
            foreach (DataRow dr in dt.Rows)
            {
                /*
                 * "USE {1};
                 * SELECT 
                 * [Sort]=a.colorder,
                 * [Name]=a.name,
                 * [IsPKey]=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (
                 *  SELECT name FROM sysindexes WHERE indid in(	
                 *      SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid	
                 * ))) then 1 else 0 end,
                 * [IsIdentity]=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then 1 else 0 end,
                 * [Type]=b.name,
                 * [Bytes]=a.length,
                 * [Length]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                 * [Digits]=ISNULL(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
                 * [Nullable]= a.isnullable ,
                 * [DefValue]=ISNULL(e.text,'') 
                 * FROM syscolumns a LEFT JOIN systypes b ON a.xusertype=b.xusertype INNER JOIN sysobjects d ON a.id=d.id 
                 * AND d.xtype='U' AND d.name !='dtproperties' LEFT JOIN syscomments e ON a.cdefault=e.id 
                 * WHERE d.name='{0}' ORDER BY a.id,a.colorder"
                 */
                int sort = 0;
                int.TryParse(dr["Sort"].ToString(), out sort);
                string name = dr["Name"].ToString();
                int isPKey = 0;
                int.TryParse(dr["IsPKey"].ToString(), out isPKey);
                int isIdentity = 0;
                int.TryParse(dr["IsIdentity"].ToString(), out isIdentity);
                string type = dr["Type"].ToString();
                int bytes = 0;
                int.TryParse(dr["Bytes"].ToString(), out bytes);
                int length = 0;
                int.TryParse(dr["Length"].ToString(), out length);
                int digits = 0;
                int.TryParse(dr["Digits"].ToString(), out digits);
                bool nullable = false;
                bool.TryParse(dr["Nullable"].ToString(), out nullable);
                string defValue = dr["DefValue"].ToString();

                FieldObject fo = new FieldObject();
                fo.Sort = sort;
                fo.Name = name;     //表字段名称：原样
                fo.PrimaryKey = (isPKey == 1);
                fo.Identity = (isIdentity == 1);
                fo.Type = DbTypeToObType(type);
                fo.Bytes = bytes;
                fo.Length = length;
                fo.Digits = digits;
                fo.Nullable = nullable;
                fo.DefaultValue = defValue;
                //
                string split = "_";
                fo.Alias = (name.Length>2) 
                    ? StringUtil.FristLower(name, split)
                    : name.ToLower();  //（别名：去掉"_"，，首字母小写；数据库字段命名最好采用驼峰法）
                fo.PrivateProp = (name.Length>2) 
                    ? StringUtil.FristLower(name, split)
                    : name.ToLower();    //（私有字段：去掉"_"，首字母小写；数据库字段命名最好采用驼峰法）
                fo.PublicProp = StringUtil.FirstUpper(name, '_'); //（公有属性：去掉'_'，首字母大写）
                fo.Descr = "";  //TODO，增加获取数据字段说明
                list.Add(fo);
            }
            return list;
        }

        /// <summary>
        /// 数据库类型转换为Obsidian自定义数据类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public string DbTypeToObType(string dbType)
        {
            string obType = "";
            switch (dbType.ToLower())
            {
                case "int":
                case "smallint":
                case "tinyint":
                    obType = "IntField";
                    break;
                case "bigint":
                    obType = "LongField";
                    break;
                case "float":
                //case "numeric":
                //case "real":
                    obType = "DoubleField";
                    break;
                case "decimal":
                case "money":
                case "smallmoney":
                    obType = "DecimalField";
                    break;
                case "datetime":
                    obType = "DateTimeField";
                    break;
                case "bit":
                    obType = "BoolField";
                    break;
                case "char":    //max 8000单字节
                case "nchar":   //max 4000双字节
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    obType = "StringField";
                    break;
                default:
                    obType = "StringField";
                    break;
            }
            return obType;
        }
    }
}
