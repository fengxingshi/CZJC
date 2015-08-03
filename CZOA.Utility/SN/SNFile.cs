using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SN.Utility
{
    public class SNFile
    {
        #region 二进制操作

        //public static byte[] MergeOnSrc(ref byte[] srcBytes, byte[] newBytes)
        //{
        //    if (srcBytes == null || newBytes == null)
        //    {
        //        return null;
        //    }

        //    var sl = srcBytes.Length;
        //    var nl = newBytes.Length;

        //    sl = sl + nl;//新长度
        //    Array.Resize<byte>(ref srcBytes, sl);
        //    Array.ConstrainedCopy()

        //    return null;
        //}
        /// <summary>
        /// 向二进制尾添加数据
        /// </summary>
        public static byte[] BytesAppendToFoot(ref byte[] srcBytes, byte[] newBytes)
        {
            if (srcBytes == null || newBytes == null)
            {
                return null;
            }

            var sl = srcBytes.Length;
            var nl = newBytes.Length;

            sl = sl + nl;//新长度
            Array.Resize<byte>(ref srcBytes, sl);
            Array.ConstrainedCopy(newBytes, 0, srcBytes, sl - nl, nl);

            return srcBytes;
        }
        /// <summary>
        /// 向二进制头添加数据
        /// </summary>
        public static byte[] BytesAppendToHead(ref byte[] srcBytes, byte[] newBytes)
        {
            if (srcBytes == null || newBytes == null)
            {
                return null;
            }

            Array.Reverse(srcBytes);//反转源数组
            Array.Reverse(newBytes);//反转新数组

            BytesAppendToFoot(ref srcBytes, newBytes);//将反转的新数组添加到反转的源数组尾

            Array.Reverse(srcBytes);//再反转回来，添加尾后的原数组，尾变头

            return srcBytes;
        }
        /// <summary>
        /// 从文件尾倒着查找特定字符串的索引
        /// </summary>
        public static int BytesLastIndexOf(Byte[] buffer, int length, string Search)
        {
            if (buffer == null)
                return -1;
            if (buffer.Length <= 0)
                return -1;
            byte[] SearchBytes = Encoding.Default.GetBytes(Search.ToUpper());
            for (int i = length - SearchBytes.Length; i >= 0; i--)
            {
                bool bFound = true;
                for (int j = 0; j < SearchBytes.Length; j++)
                {
                    if (ByteUpper(buffer[i + j]) != SearchBytes[j])
                    {
                        bFound = false;
                        break;
                    }
                }
                if (bFound)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 从文件头正着查找特定字符串的索引
        /// </summary>
        public static int BytesFirstIndexOf(Byte[] buffer, int length, string Search)
        {
            if (buffer == null)
                return -1;
            if (buffer.Length <= 0)
                return -1;
            byte[] SearchBytes = Encoding.Default.GetBytes(Search.ToUpper());

            for (int i = 0; i <= length - SearchBytes.Length; i++)
            {
                bool bFound = true;
                for (int j = 0; j < SearchBytes.Length; j++)
                {
                    if (ByteUpper(buffer[i + j]) != SearchBytes[j])
                    {
                        bFound = false;
                        break;
                    }
                }
                if (bFound)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 置为大写字符二进制
        /// </summary>
        private static byte ByteUpper(byte byteValue)
        {
            char charValue = Convert.ToChar(byteValue);
            if (charValue < 'a' || charValue > 'z')
                return byteValue;
            else
                return Convert.ToByte(byteValue - 32);
        }
        #endregion

        #region 文件读写
        /// <summary>
        /// 在指定位置创建文件
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <param name="strFileEx">扩展名（不带"."）</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns></returns>
        public static string CreateFileToPath(string strFileAllName, string strFileEx, byte[] fileContent)
        {
            try
            {
                //string strFilePath = string.Format(@"{0}\{1}.{2}", BusinessParameters.WorkingPath, strFileName + DateTime.Now.ToString("yyyyMMddHHmmssfffffff"), strFileEx);
                //判断是否已经有扩展名
                if (strFileAllName.IndexOf('.') == -1)
                {
                    strFileAllName += string.Format(".{0}", strFileEx);
                }
                FileInfo fileInfo = new FileInfo(strFileAllName);
                if (!Directory.Exists(fileInfo.DirectoryName))
                {
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }
                if (File.Exists(strFileAllName))
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
                FileStream fs2 = new FileStream(strFileAllName, FileMode.Create, FileAccess.Write, FileShare.None);
                byte[] content = fileContent;
                BinaryWriter bw = new BinaryWriter(fs2);
                bw.Write(content);
                bw.Flush();
                bw.Close();
                //fs2.Close();
                return strFileAllName;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 读取指定文件（只读模式），返回byte[]
        /// </summary>
        /// <param name="fileFullName">文件全名，带路径</param>
        /// <returns></returns>
        public static byte[] GetFileBytes(string fileFullName)
        {
            try
            {
                if (File.Exists(fileFullName))
                {
                    FileStream fsMyfile = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
                    BinaryReader brMyfile = new BinaryReader(fsMyfile);
                    byte[] bytes = brMyfile.ReadBytes(Convert.ToInt32(fsMyfile.Length));
                    brMyfile.Close();
                    //fsMyfile.Close();
                    return bytes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取指定文件出现异常", ex);
            }
        }
        #endregion

        #region 检查文件是否被占用
        #region API声明
        [DllImport("kernel32.dll")]
        private static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);
        private const int OF_READWRITE = 2;
        private const int OF_SHARE_DENY_NONE = 0x40;
        private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        #endregion
        /// <summary>
        /// 检测文件状态 - 文件是否被占用(API版) : 0不存在，1被占用，2没被占用
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>0不存在，1被占用，2没被占用</returns>
        public static int GetFileStatus(string fileName)
        {
            string vFileName = fileName;
            if (!File.Exists(vFileName))
            {
                //文件不存在
                return 0;
            }

            IntPtr vHandle = _lopen(vFileName, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                //文件被占用
                return 1;
            }
            CloseHandle(vHandle);
            //没有被占用
            return 2;
        }
        /// <summary>
        /// 文件是否被占用(.NET版)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileUsed(string fileName)
        {
            bool inUse = true;
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                fs.Close();
                inUse = false;
            }
            catch (Exception)
            {
                return inUse;
            }
            return inUse;//true表示正在使用,false没有使用
        }
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }
        #endregion
    }
}
