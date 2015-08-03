using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace CZOA.DB
{
    public partial class OAContext
    {
        /// <summary>
        /// 执行sql返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SqlTable(string sql)
        {
            var cmd = Database.Connection.CreateCommand();
            cmd.CommandText = sql;

            cmd.Connection.Open();

            var reader = cmd.ExecuteReader();

            var table = new DataTable();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }
            while (reader.Read())
            {
                var vs = new List<object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    vs.Add(reader[i]);
                }
                table.Rows.Add(vs.ToArray());
            }
            reader.Close();
            //cmd.Connection.Close();

            return table;
        }
        /// <summary>
        /// 执行sql返回List Dynamic
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IList<dynamic> SqlDynamic(string sql)
        {
            var cmd = Database.Connection.CreateCommand();
            cmd.CommandText = sql;

            cmd.Connection.Open();

            var reader = cmd.ExecuteReader();

            IList<dynamic> table = new List<dynamic>();
            while (reader.Read())
            {
                var dy = new ExpandoObject();
                var dycol = (IDictionary<string, object>)dy;
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    dycol.Add(reader.GetName(i), reader[i]);
                }
                table.Add(dy);
            }
            reader.Close();

            return table;
        }
    }
}
