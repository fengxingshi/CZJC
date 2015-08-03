/*
 * ===数据模型环境===
 * 设置数据库连接
 * 初始化模型，加载配置信息与映射关系
 * 配置数据模型集DbSet
 * 与CZOA.DB项目配合，DB项目中是各模型的定义
 */

using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Dynamic;
using CZOA.DB.Mapping;

namespace CZOA.DB
{
    public partial class OAContext : DbContext
    {
        public OAContext()
            : base("OAContextConn")
        {
            /*
             * OAContextConn为App.config中的连接字符串
             */
        }

        public OAContext(string connNameOrConnStr)
            : base(connNameOrConnStr)
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Build(this.Database.Connection);
            //modelBuilder.Conventions.Add(new DecimalPropertyConvention(38, 18));//EF6支持
            //modelBuilder.Configurations.Add(new M_TAG(CZOA.Config.DbSchema));
            //modelBuilder.Configurations.Add(new M_TEST(CZOA.Config.DbSchema));
            //modelBuilder.Entity<TEST>().Property(p => p.ID).HasPrecision(19, 0);//手动设置某表某列decimal转换规则

            // DONE:关键，去掉EF5默认的decimal转为number(18,2)的转换器，否则decimal存不进19位
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();

            // DONE:加载Mapping映射设置
            modelBuilder.Configurations.Add(new M_TEST())
                .Add(new M_FILE_TYPE())
                .Add(new M_ATTA())
                .Add(new M_ATTA_RECORD())
                .Add(new M_COMMENT())
                .Add(new M_DEPT())
                .Add(new M_ENTITY())
                .Add(new M_ENTITY_ITEM())
                .Add(new M_ENTITY_ITEM_SIGN())
                .Add(new M_FLOW())
                .Add(new M_FLOW_AUTH())
                .Add(new M_FLOW_TAG())
                .Add(new M_FORM())
                .Add(new M_MODEL())
                .Add(new M_MODEL_ITEM())
                .Add(new M_NODE())
                .Add(new M_NODE_DOMAIN())
                .Add(new M_NODE_TAG())
                .Add(new M_OBJECT_AUTH())
                .Add(new M_ORG())
                .Add(new M_ROLE())
                .Add(new M_SIGN())
                .Add(new M_STAGE())
                .Add(new M_STEP())
                .Add(new M_STEP_AUTH())
                .Add(new M_STEP_MODEL())
                .Add(new M_STEP_MODEL_ITEM())
                .Add(new M_STEP_TAG())
                .Add(new M_STEP_TO_NEXT())
                .Add(new M_TAG())
                .Add(new M_TAG_CATEGORY())
                .Add(new M_TAG_ITEM())
                .Add(new M_USER())
                .Add(new M_USER_DOMAIN())
                .Add(new M_WORK())
                .Add(new M_WORK_TAG())
                .Add(new M_RULES_FILE());



            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<T_FLOW>().Ignore(p => p.FLOW_CATEGORY_TI_NAME);

        }

        public virtual DbSet<TEST> TEST { get; set; }

        public virtual DbSet<C_FILE_TYPE> C_FILE_TYPE { get; set; }
        public virtual DbSet<C_RULES_FILE> C_RULES_FILE { get; set; }

        public virtual DbSet<T_ATTA> T_ATTA { get; set; }
        public virtual DbSet<T_ATTA_RECORD> T_ATTA_RECORD { get; set; }
        public virtual DbSet<T_COMMENT> T_COMMENT { get; set; }
        public virtual DbSet<T_DEPT> T_DEPT { get; set; }
        public virtual DbSet<T_ENTITY> T_ENTITY { get; set; }
        public virtual DbSet<T_ENTITY_ITEM> T_ENTITY_ITEM { get; set; }
        public virtual DbSet<T_ENTITY_ITEM_SIGN> T_ENTITY_ITEM_SIGN { get; set; }
        public virtual DbSet<T_FLOW> T_FLOW { get; set; }
        public virtual DbSet<T_FLOW_AUTH> T_FLOW_AUTH { get; set; }
        public virtual DbSet<T_FLOW_TAG> T_FLOW_TAG { get; set; }
        public virtual DbSet<T_FORM> T_FORM { get; set; }
        public virtual DbSet<T_MODEL> T_MODEL { get; set; }
        public virtual DbSet<T_MODEL_ITEM> T_MODEL_ITEM { get; set; }
        public virtual DbSet<T_NODE> T_NODE { get; set; }
        public virtual DbSet<T_NODE_DOMAIN> T_NODE_DOMAIN { get; set; }
        public virtual DbSet<T_NODE_TAG> T_NODE_TAG { get; set; }
        public virtual DbSet<T_OBJECT_AUTH> T_OBJECT_AUTH { get; set; }
        public virtual DbSet<T_ORG> T_ORG { get; set; }
        public virtual DbSet<T_ROLE> T_ROLE { get; set; }
        public virtual DbSet<T_SIGN> T_SIGN { get; set; }
        public virtual DbSet<T_STAGE> T_STAGE { get; set; }
        public virtual DbSet<T_STEP> T_STEP { get; set; }
        public virtual DbSet<T_STEP_AUTH> T_STEP_AUTH { get; set; }
        public virtual DbSet<T_STEP_MODEL> T_STEP_MODEL { get; set; }
        public virtual DbSet<T_STEP_MODEL_ITEM> T_STEP_MODEL_ITEM { get; set; }
        public virtual DbSet<T_STEP_TAG> T_STEP_TAG { get; set; }
        public virtual DbSet<T_STEP_TO_NEXT> T_STEP_TO_NEXT { get; set; }
        public virtual DbSet<T_TAG> T_TAG { get; set; }
        public virtual DbSet<T_TAG_CATEGORY> T_TAG_CATEGORY { get; set; }
        public virtual DbSet<T_TAG_ITEM> T_TAG_ITEM { get; set; }
        public virtual DbSet<T_USER> T_USER { get; set; }
        public virtual DbSet<T_USER_DOMAIN> T_USER_DOMAIN { get; set; }
        public virtual DbSet<T_WORK> T_WORK { get; set; }
        public virtual DbSet<T_WORK_TAG> T_WORK_TAG { get; set; }
    }
}
