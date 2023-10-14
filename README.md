# go-dll
使用cgo编译golang代码，生成c的动态库(dll)，并使用c#调用的demo。本例使用c#调用go编译成的dll，完成对minio的文件上传下载功能。
并实现了c#传递回调函数到dll中，实时获取上传下载进度。
## 编写cgo代码 minioUploadAndDownload.go
注意go的导出类型。

### cgo导出类型转换
返回cgo字符串类型时一定要转换为*C.char。

### 调用传参时类型转换
转入字符串类型时一定要转换为*C.char。
传入回调函数类型时一定要转换为uintptr，并使用系统调用实现。

## cgo代码编译dll

**前提：**需先配置好gcc编译器mingw64。并配置好环境变量。
```shell
# 开启cgo
go env -w CGO_ENABLED=1
# 生成dll
go build -ldflags "-s -w" -buildmode=c-shared -o process.dll minioUploadAndDownload.go
# 还可以用upx对dll进行压缩
upx.exe process.dll
```

## 编写c#代码 test.cs

```csharp
 [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
 delegate void ProgressCallback(string value);
 [DllImport("process.dll", EntryPoint="uploadFile", CallingConvention = CallingConvention.Cdecl, ExactSpelling = false)]
 static extern IntPtr uploadFile(string  v, string  v1, string  v2, string  v3, string  v4, string  v5,[MarshalAs(UnmanagedType.FunctionPtr)] ProgressCallback p);
     
 [DllImport("process.dll", EntryPoint="downloadFile", CallingConvention = CallingConvention.Cdecl, ExactSpelling = false)]
 static extern IntPtr downloadFile(string  v, string  v1, string  v2, string  v3, string  v4, string  v5,[MarshalAs(UnmanagedType.FunctionPtr)] ProgressCallback p);
```

## 运行c#程序

```shell
dotnet run -r win-x86 --self-contained
```

## 效果图

