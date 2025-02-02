using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000091 RID: 145
	// (Invoke) Token: 0x06000586 RID: 1414
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnReadFileCompleteCallbackInternal(ref ReadFileCallbackInfoInternal data);
}
