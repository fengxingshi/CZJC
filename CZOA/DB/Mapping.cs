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
            Property(p => p.TS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.RS).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
    

}
