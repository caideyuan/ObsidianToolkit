using Obsidian.Edm;  //引入Obsidian框架
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YS.Model
{
    /// <summary>
    /// [Adv 实体类描述]
    /// 1.继承 Obsidian.Edm.OModel
    /// 2.命名规范：以Info结尾
    /// </summary>
    public class AdvInfo : OModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public AdvInfo()
        {
            base.InitModel(DbcsName, TableName, new IModelField[] {
				id = new IntField(this, "Id" , "id"),
				class1Id = new IntField(this, "Class1Id" , "class1Id"),
				class2Id = new IntField(this, "Class2Id" , "class2Id"),
				advUrl = new StringField(this, "Adv_Url" , "advUrl"),
				advName = new StringField(this, "Adv_Name" , "advName"),
				advImg = new StringField(this, "Adv_Img" , "advImg"),
				advdesc = new StringField(this, "Adv_desc" , "advdesc"),
				advType = new StringField(this, "Adv_Type" , "advType"),
				courseId = new LongField(this, "CourseId" , "courseId"),
				price = new DecimalField(this, "Price" , "price"),
				userid = new LongField(this, "Userid" , "userid"),
				instructorName = new StringField(this, "InstructorName" , "instructorName"),
				width = new StringField(this, "Width" , "width"),
				height = new StringField(this, "Height" , "height"),
				startTime = new DateTimeField(this, "StartTime" , "startTime"),
				endTime = new DateTimeField(this, "EndTime" , "endTime")
			});
        }

        private string DbcsName = "YingSheng";  //bin目录下，配置文件AppConfig.xml中的数据库连接字符串别名
        private string TableName = "Adv"; //数据库中的表名
        private IntField id;
        /// <summary>
        /// 
        /// </summary>
        public IntField Id { get { return id; } }
        private IntField class1Id;
        /// <summary>
        /// 
        /// </summary>
        public IntField Class1Id { get { return class1Id; } }
        private IntField class2Id;
        /// <summary>
        /// 
        /// </summary>
        public IntField Class2Id { get { return class2Id; } }
        private StringField advUrl;
        /// <summary>
        /// 
        /// </summary>
        public StringField AdvUrl { get { return advUrl; } }
        private StringField advName;
        /// <summary>
        /// 
        /// </summary>
        public StringField AdvName { get { return advName; } }
        private StringField advImg;
        /// <summary>
        /// 
        /// </summary>
        public StringField AdvImg { get { return advImg; } }
        private StringField advdesc;
        /// <summary>
        /// 
        /// </summary>
        public StringField Advdesc { get { return advdesc; } }
        private StringField advType;
        /// <summary>
        /// 
        /// </summary>
        public StringField AdvType { get { return advType; } }
        private LongField courseId;
        /// <summary>
        /// 
        /// </summary>
        public LongField CourseId { get { return courseId; } }
        private DecimalField price;
        /// <summary>
        /// 
        /// </summary>
        public DecimalField Price { get { return price; } }
        private LongField userid;
        /// <summary>
        /// 
        /// </summary>
        public LongField Userid { get { return userid; } }
        private StringField instructorName;
        /// <summary>
        /// 
        /// </summary>
        public StringField InstructorName { get { return instructorName; } }
        private StringField width;
        /// <summary>
        /// 
        /// </summary>
        public StringField Width { get { return width; } }
        private StringField height;
        /// <summary>
        /// 
        /// </summary>
        public StringField Height { get { return height; } }
        private DateTimeField startTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTimeField StartTime { get { return startTime; } }
        private DateTimeField endTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTimeField EndTime { get { return endTime; } }
    }
}
