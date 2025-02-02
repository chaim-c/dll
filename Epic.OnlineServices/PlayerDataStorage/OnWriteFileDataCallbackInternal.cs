using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000288 RID: 648
	// (Invoke) Token: 0x06001189 RID: 4489
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate WriteResult OnWriteFileDataCallbackInternal(ref WriteFileDataCallbackInfoInternal data, IntPtr outDataBuffer, ref uint outDataWritten);
}
