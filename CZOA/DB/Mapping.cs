/*
 * 设置数据模型与数据库的映射关系
 */

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CZOA.DB.Mapping
{
    public class M_TEST : EntityTypeConfiguration<TEST>
    {
        public M_TEST(string dbSchema = Config.DbSchema)
        {
            //设置键
            //HasKey(p => p.ID);
            //映射字段
            //Property(p => p.AGE).HasColumnName("AGE");
            //GUID类型的映射
            //Property(p => p.ID).HasPrecision(20, 0).HasColumnName("ID").HasColumnType("number");
            //映射到表与架构
            ToTable("TEST", dbSchema);
            //使用表字段的默认值 => [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            Property(p => p.TS)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_FILE_TYPE : EntityTypeConfiguration<C_FILE_TYPE>
    {
        public M_FILE_TYPE(string dbSchema = Config.DbSchema)
        {
            ToTable("C_FILE_TYPE", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
    public class M_RULES_FILE : EntityTypeConfiguration<C_RULES_FILE>
    {
        public M_RULES_FILE(string dbSchema = Config.DbSchema)
        {
            ToTable("C_RULES_FILE", dbSchema);

        }
    }
    public class M_ATTA : EntityTypeConfiguration<T_ATTA>
    {
        public M_ATTA(string dbSchema = Config.DbSchema)
        {
            ToTable("T_ATTA", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_ATTA_RECORD : EntityTypeConfiguration<T_ATTA_RECORD>
    {
        public M_ATTA_RECORD(string dbSchema = Config.DbSchema)
        {
            ToTable("T_ATTA_RECORD", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_COMMENT : EntityTypeConfiguration<T_COMMENT>
    {
        public M_COMMENT(string dbSchema = Config.DbSchema)
        {
            ToTable("T_COMMENT", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
    public class M_DEPT : EntityTypeConfiguration<T_DEPT>
    {
        public M_DEPT(string dbSchema = Config.DbSchema)
        {
            ToTable("T_DEPT", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_ENTITY : EntityTypeConfiguration<T_ENTITY>
    {
        public M_ENTITY(string dbSchema = Config.DbSchema)
        {
            ToTable("T_ENTITY", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_ENTITY_ITEM : EntityTypeConfiguration<T_ENTITY_ITEM>
    {
        public M_ENTITY_ITEM(string dbSchema = Config.DbSchema)
        {
            ToTable("T_ENTITY_ITEM", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_ENTITY_ITEM_SIGN : EntityTypeConfiguration<T_ENTITY_ITEM_SIGN>
    {
        public M_ENTITY_ITEM_SIGN(string dbSchema = Config.DbSchema)
        {
            ToTable("T_ENTITY_ITEM_SIGN", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_FLOW : EntityTypeConfiguration<T_FLOW>
    {
        public M_FLOW(string dbSchema = Config.DbSchema)
        {
            ToTable("T_FLOW", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_FLOW_AUTH : EntityTypeConfiguration<T_FLOW_AUTH>
    {
        public M_FLOW_AUTH(string dbSchema = Config.DbSchema)
        {
            ToTable("T_FLOW_AUTH", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_FLOW_TAG : EntityTypeConfiguration<T_FLOW_TAG>
    {
        public M_FLOW_TAG(string dbSchema = Config.DbSchema)
        {
            ToTable("T_FLOW_TAG", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_FORM : EntityTypeConfiguration<T_FORM>
    {
        public M_FORM(string dbSchema = Config.DbSchema)
        {
            ToTable("T_FORM", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_MODEL : EntityTypeConfiguration<T_MODEL>
    {
        public M_MODEL(string dbSchema = Config.DbSchema)
        {
            ToTable("T_MODEL", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_MODEL_ITEM : EntityTypeConfiguration<T_MODEL_ITEM>
    {
        public M_MODEL_ITEM(string dbSchema = Config.DbSchema)
        {
            ToTable("T_MODEL_ITEM", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_NODE : EntityTypeConfiguration<T_NODE>
    {
        public M_NODE(string dbSchema = Config.DbSchema)
        {
            ToTable("T_NODE", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_NODE_DOMAIN : EntityTypeConfiguration<T_NODE_DOMAIN>
    {
        public M_NODE_DOMAIN(string dbSchema = Config.DbSchema)
        {
            ToTable("T_NODE_DOMAIN", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_NODE_TAG : EntityTypeConfiguration<T_NODE_TAG>
    {
        public M_NODE_TAG(string dbSchema = Config.DbSchema)
        {
            ToTable("T_NODE_TAG", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_OBJECT_AUTH : EntityTypeConfiguration<T_OBJECT_AUTH>
    {
        public M_OBJECT_AUTH(string dbSchema = Config.DbSchema)
        {
            ToTable("T_OBJECT_AUTH", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_ORG : EntityTypeConfiguration<T_ORG>
    {
        public M_ORG(string dbSchema = Config.DbSchema)
        {
            ToTable("T_ORG", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_ROLE : EntityTypeConfiguration<T_ROLE>
    {
        public M_ROLE(string dbSchema = Config.DbSchema)
        {
            ToTable("T_ROLE", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_SIGN : EntityTypeConfiguration<T_SIGN>
    {
        public M_SIGN(string dbSchema = Config.DbSchema)
        {
            ToTable("T_SIGN", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_STAGE : EntityTypeConfiguration<T_STAGE>
    {
        public M_STAGE(string dbSchema = Config.DbSchema)
        {
            ToTable("T_STAGE", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_STEP : EntityTypeConfiguration<T_STEP>
    {
        public M_STEP(string dbSchema = Config.DbSchema)
        {
            ToTable("T_STEP", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_STEP_AUTH : EntityTypeConfiguration<T_STEP_AUTH>
    {
        public M_STEP_AUTH(string dbSchema = Config.DbSchema)
        {
            ToTable("T_STEP_AUTH", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_STEP_MODEL : EntityTypeConfiguration<T_STEP_MODEL>
    {
        public M_STEP_MODEL(string dbSchema = Config.DbSchema)
        {
            ToTable("T_STEP_MODEL", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_STEP_MODEL_ITEM : EntityTypeConfiguration<T_STEP_MODEL_ITEM>
    {
        public M_STEP_MODEL_ITEM(string dbSchema = Config.DbSchema)
        {
            ToTable("T_STEP_MODEL_ITEM", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_STEP_TAG : EntityTypeConfiguration<T_STEP_TAG>
    {
        public M_STEP_TAG(string dbSchema = Config.DbSchema)
        {
            ToTable("T_STEP_TAG", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_STEP_TO_NEXT : EntityTypeConfiguration<T_STEP_TO_NEXT>
    {
        public M_STEP_TO_NEXT(string dbSchema = Config.DbSchema)
        {
            ToTable("T_STEP_TO_NEXT", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_TAG : EntityTypeConfiguration<T_TAG>
    {
        public M_TAG(string dbSchema = Config.DbSchema)
        {
            ToTable("T_TAG", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_TAG_CATEGORY : EntityTypeConfiguration<T_TAG_CATEGORY>
    {
        public M_TAG_CATEGORY(string dbSchema = Config.DbSchema)
        {
            ToTable("T_TAG_CATEGORY", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_TAG_ITEM : EntityTypeConfiguration<T_TAG_ITEM>
    {
        public M_TAG_ITEM(string dbSchema = Config.DbSchema)
        {
            ToTable("T_TAG_ITEM", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_USER : EntityTypeConfiguration<T_USER>
    {
        public M_USER(string dbSchema = Config.DbSchema)
        {
            ToTable("T_USER", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_USER_DOMAIN : EntityTypeConfiguration<T_USER_DOMAIN>
    {
        public M_USER_DOMAIN(string dbSchema = Config.DbSchema)
        {
            ToTable("T_USER_DOMAIN", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_WORK : EntityTypeConfiguration<T_WORK>
    {
        public M_WORK(string dbSchema = Config.DbSchema)
        {
            ToTable("T_WORK", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

    public class M_WORK_TAG : EntityTypeConfiguration<T_WORK_TAG>
    {
        public M_WORK_TAG(string dbSchema = Config.DbSchema)
        {
            ToTable("T_WORK_TAG", dbSchema);
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }

}
