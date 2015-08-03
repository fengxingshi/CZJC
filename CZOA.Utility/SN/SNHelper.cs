using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace SN.Utility
{
    public class SNHelper
    {
        public class Checker
        {
            public static bool IsNullOrEmpty(params object[] values)
            {
                int t=0;
                Func<object, bool> f = (p) =>
                {
                    if (int.TryParse(p.ToString(), out t))
                    {
                        if (t != 0)
                        {
                            return true;
                        }
                    }
                    return false;
                };
                foreach (var v in values)
                {
                    if (v == null
                        || v.ToString().Equals(string.Empty)
                        || f(v))
                    {
                        return true;
                    }

                }

                return false;
            }
        }
        #region 唯一序列
        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>
        public static long GuidToLongID(Guid id)
        {
            byte[] buffer = id.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        /// <summary>
        /// 直接获取19位唯一数字序列
        /// </summary>
        private static long GetLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        /// <summary>
        /// 检查id，如果为0，返回一个新longID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static long BuildID(long id = 0)
        {
            id = id == 0 ? GetLongID() : id;
            return id;
        }
        #endregion

        #region 进程、线程管理
        /// <summary>
        /// 杀死进程
        /// </summary>
        /// <param name="processName"></param>
        public static void KillProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(processName);

                foreach (System.Diagnostics.Process p in ps)
                {
                    if (!p.CloseMainWindow())
                    {
                        p.Kill();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public static int GetProcessCount(string processName)
        {
            try
            {
                return System.Diagnostics.Process.GetProcessesByName(processName).Length;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void KillProcessByPID(string processName, int pid)
        {
            try
            {
                System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(processName);

                foreach (System.Diagnostics.Process p in ps)
                {
                    if (p.Id == pid)
                    {
                        if (!p.CloseMainWindow())
                        {
                            p.Kill();
                        }
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static void KillWord(int pid = 0)
        {
            if (pid == 0)
            {
                KillProcess("WINWORD");
            }
            else
            {
                KillProcessByPID("WINWORD", pid);
            }
        }

        public static void KillExcel(int pid = 0)
        {
            if (pid == 0)
            {
                KillProcess("EXCEL");
            }
            else
            {
                KillProcessByPID("EXCEL", pid);
            }
        }

        public static void Sleep(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
        #endregion

        #region 获取和设置默认打印机
        /// <summary>
        /// 设置默认打印机
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        [DllImport("Winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string printerName);
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int pcchBuffer);
        /// <summary>
        /// 获取默认打印机
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultPrinter()
        {
            const int ERROR_FILE_NOT_FOUND = 2;
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            int pcchBuffer = 0;
            if (GetDefaultPrinter(null, ref pcchBuffer))
            {
                return null;
            }
            int lastWin32Error = Marshal.GetLastWin32Error();
            if (lastWin32Error == ERROR_INSUFFICIENT_BUFFER)
            {
                StringBuilder pszBuffer = new StringBuilder(pcchBuffer);
                if (GetDefaultPrinter(pszBuffer, ref pcchBuffer))
                {
                    return pszBuffer.ToString();
                }
                lastWin32Error = Marshal.GetLastWin32Error();
            }
            if (lastWin32Error == ERROR_FILE_NOT_FOUND)
            {
                return null;
            }
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }
        #endregion

        #region 压缩解压
        //压缩Gzip
        public static string GzipCompress(string uncompressedString)
        {
            byte[] bytData = System.Text.Encoding.Unicode.GetBytes(uncompressedString);
            MemoryStream ms = new MemoryStream();
            Stream s = new GZipStream(ms, CompressionMode.Compress);
            s.Write(bytData, 0, bytData.Length);
            s.Close();
            byte[] compressedData = (byte[])ms.ToArray();
            return System.Convert.ToBase64String(compressedData, 0, compressedData.Length);
        }
        //解压Gzip
        public static string GzipDeCompress(string compressedString)
        {
            System.Text.StringBuilder uncompressedString = new System.Text.StringBuilder();
            int totalLength = 0;
            byte[] bytInput = System.Convert.FromBase64String(compressedString); ;
            byte[] writeData = new byte[4096];
            Stream s2 = new GZipStream(new MemoryStream(bytInput), CompressionMode.Decompress);
            while (true)
            {
                int size = s2.Read(writeData, 0, writeData.Length);
                if (size > 0)
                {
                    totalLength += size;
                    uncompressedString.Append(System.Text.Encoding.Unicode.GetString(writeData, 0, size));
                }
                else
                {
                    break;
                }
            }
            s2.Close();
            return uncompressedString.ToString();
        }
        #endregion
    }
}
