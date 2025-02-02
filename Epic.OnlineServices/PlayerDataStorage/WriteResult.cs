using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020002A0 RID: 672
	public enum WriteResult
	{
		// Token: 0x04000827 RID: 2087
		ContinueWriting = 1,
		// Token: 0x04000828 RID: 2088
		CompleteRequest,
		// Token: 0x04000829 RID: 2089
		FailRequest,
		// Token: 0x0400082A RID: 2090
		CancelRequest
	}
}
