using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dotnetWorks
{
    class File
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void ProgressCallback(string value);
        [DllImport("process.dll", EntryPoint="uploadFile", CallingConvention = CallingConvention.Cdecl, ExactSpelling = false)]
        static extern IntPtr uploadFile(string  v, string  v1, string  v2, string  v3, string  v4, string  v5,[MarshalAs(UnmanagedType.FunctionPtr)] ProgressCallback p);

        [DllImport("process.dll", EntryPoint="downloadFile", CallingConvention = CallingConvention.Cdecl, ExactSpelling = false)]
        static extern IntPtr downloadFile(string  v, string  v1, string  v2, string  v3, string  v4, string  v5,[MarshalAs(UnmanagedType.FunctionPtr)] ProgressCallback p);

        static ProgressCallback callback =
            (value) =>
            {
                Console.WriteLine("progress: {0}",value);
            };
        static void Main()
        {
            Environment.SetEnvironmentVariable("GODEBUG", "cgocheck=0");
            Console.WriteLine("process begin...");

            // demo1 uploadFile
             IntPtr p;
             p = uploadFile("minio的各种配置信息", callback);
             string res = Marshal.PtrToStringAnsi(p);
             Console.WriteLine("res: {0}",res);

            // demo2 downloadFile
            IntPtr d;
            d = downloadFile("minio的各种配置信息", callback);
            string resD = Marshal.PtrToStringAnsi(d);
            Console.WriteLine("resD: {0}",resD);
        }
    }
}