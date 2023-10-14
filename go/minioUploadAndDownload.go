package main

import "C"
import (
	"syscall"
	"unsafe"
)

var notifyCallBackUpload uintptr
var notifyCallBackDownload uintptr

// 系统调用
func notifyUpload(s *C.char) {
	_, _, _ = syscall.SyscallN(notifyCallBackUpload, uintptr(unsafe.Pointer(s)))
}

// 系统调用
func notifyDownload(s *C.char) {
	_, _, _ = syscall.SyscallN(notifyCallBackDownload, uintptr(unsafe.Pointer(s)))
}
func main() {
	// Need a main function to make CGO compile package as C shared library
}

//export uploadFile
func uploadFile(url, accessID, accessKey, bucket, object, path *C.char, callBackFunc uintptr) *C.char {
	return uploadFileWithRetry(url, accessID, accessKey, bucket, object, path, callBackFunc, 1)
}

func uploadFileWithRetry(url, accessID, accessKey, bucket, object, path *C.char, callBackFunc uintptr, retry int) *C.char {
	// 具体的文件上传步骤
}

//export downloadFile
func downloadFile(url, accessID, accessKey, bucket, object, localFile *C.char, callBackFunc uintptr) *C.char {
	return downloadFileWithRetry(url, accessID, accessKey, bucket, object, localFile, callBackFunc, 1)
}

func downloadFileWithRetry(url, accessID, accessKey, bucket, object, localFile *C.char, callBackFunc uintptr, retry int) *C.char {
	// 具体的文件下载步骤
}
