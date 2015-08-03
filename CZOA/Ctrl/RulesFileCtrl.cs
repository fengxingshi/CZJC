using System;
using System.Data;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.Ctrl
{
    internal class RulesFileCtrl : IRulesFileCtrl
    {
        public decimal AddRulesFile(C_RULES_FILE rulesFile)
        {
            using (var ctx = new OAContext())
            {
                ctx.C_RULES_FILE.Add(rulesFile);
                return ctx.SaveChanges();
            }
        }

        public decimal UpdataRulesFile(C_RULES_FILE rulesFile)
        {
            using (var ctx = new OAContext())
            {
                ctx.C_RULES_FILE.Attach(rulesFile);
                ctx.Entry<C_RULES_FILE>(rulesFile).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public DataTable GetRulesFile(long departmentId)
        {
            using (var ctx = new OAContext())
            {
                var sqlStr = string.Format(@"select c.*,
t.item_name as accessscope_name,
t1.item_name as filestate_name,
t2.item_name as filelevel_name,
case c.data_state when 0 then '未发布' when 1 then '已发布' end datastate_name 
from C_RULES_FILE c
left join t_tag_item t
on c.accessscope = t.tag_item_id
left join t_tag_item t1
on c.file_state = t1.tag_item_id
left join t_tag_item t2
on c.file_level = t2.tag_item_id
where SOURCE_DEPT = {0}", departmentId);
                var re = ctx.SqlTable(sqlStr);
                return re;
            }
        }

        public DataTable GetTypeRulesFile(decimal orgId, decimal departmentId, decimal userId, decimal type, decimal sccope = 0)
        {
            using (var ctx = new OAContext())
            {
                string con;
                if (sccope != 0)
                {   
                    //仅编辑用户本人添加的文件,按文件访问范围分开查询
                    con = string.Format("f.type_id = {0} and c.accessscope = {1} and c.editor = {2}", type, sccope, userId);
                }
                else
                {
                    //查询仅能查看正常已发布的文件
                    con = string.Format("f.type_id = {0} and c.file_state = 200001101 and c.data_state = 1 and " +
                                        "(c.accessscope = 200001001 or (c.accessscope = 200001002 and c.source_org = {1}) or " +
                                        "(c.accessscope = 200001003 and c.source_dept = {2}))", type, orgId, departmentId);
                }

                var sqlStr = string.Format(@"select c.*,
t.item_name as accessscope_name,
t1.item_name as filestate_name,
t2.item_name as filelevel_name,
case c.data_state when 0 then '未发布' when 1 then '已发布' end datastate_name, 
f.type_name
from C_RULES_FILE c
left join t_tag_item t
on c.accessscope = t.tag_item_id
left join t_tag_item t1
on c.file_state = t1.tag_item_id
left join t_tag_item t2
on c.file_level = t2.tag_item_id
left join c_File_Type f
on c.file_type_id = f.type_id
where c.rs = 1 and {0} 
order by t.item_idx,c.file_idx,t2.item_idx,c.file_title", con);
                var re = ctx.SqlTable(sqlStr);
                return re;
            }
        }

        public decimal DeleteRulesFile(decimal rulesFileId)
        {
            using (var ctx = new OAContext())
            {
                var sqlStr = string.Format("update c_rules_file c set c.rs = 0 where c.file_id = {0}", rulesFileId);
                var re = ctx.Database.ExecuteSqlCommand(sqlStr);
                return re;
            }
        }

        public decimal UpdataFileState(decimal rulesFileId, int state, decimal userId)
        {
            using (var ctx = new OAContext())
            {
                string sqlStr;
                if (state == 1)
                {
                    sqlStr =
                        string.Format(
                            "update C_RULES_FILE c set c.data_state = {1},c.release_user = {2},c.release_date = sysdate where c.file_id = {0}",
                            rulesFileId, state, userId);
                }
                else
                {
                    sqlStr = string.Format("update C_RULES_FILE c set c.data_state = {1} where c.file_id = {0}",
                        rulesFileId, state);
                }

                return ctx.Database.ExecuteSqlCommand(sqlStr);
            }
        }
    }
}
