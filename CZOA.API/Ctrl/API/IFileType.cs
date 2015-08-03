using System.Collections;
using System.Collections.Generic;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface IFileType
    {
        decimal AddFileType(C_FILE_TYPE fileType);

        decimal UpdataFileType(C_FILE_TYPE fileType);

        IList<C_FILE_TYPE> GetFileType();
    }
}
