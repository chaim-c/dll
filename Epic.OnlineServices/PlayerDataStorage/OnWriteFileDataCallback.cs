using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000287 RID: 647
	// (Invoke) Token: 0x06001185 RID: 4485
	public delegate WriteResult OnWriteFileDataCallback(ref WriteFileDataCallbackInfo data, out ArraySegment<byte> outDataBuffer);
}
