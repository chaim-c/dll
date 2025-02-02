using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000093 RID: 147
	// (Invoke) Token: 0x0600058E RID: 1422
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate ReadResult OnReadFileDataCallbackInternal(ref ReadFileDataCallbackInfoInternal data);
}
