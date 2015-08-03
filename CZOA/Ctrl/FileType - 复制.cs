using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using CZOA.DB;
using SN.Utility;

namespace CZOA.Ctrl
{
    internal class FileType : IFileType
    {
        public IList<C_FILE_TYPE> GetFileType()
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.C_FILE_TYPE;
                return re.ToList();
            }
        }


        public decimal AddFileType(C_FILE_TYPE fileType)
        {
            using (var ctx = new OAContext())
            {
                ctx.C_FILE_TYPE.Add(fileType);
                return ctx.SaveChanges();
            }
        }

        public decimal UpdataFileType(C_FILE_TYPE fileType)
        {
            using (var ctx = new OAContext())
            {
                ctx.C_FILE_TYPE.Attach(fileType);
                ctx.Entry<C_FILE_TYPE>(fileType).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

    }
}
