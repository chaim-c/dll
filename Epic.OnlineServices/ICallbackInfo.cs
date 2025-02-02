using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000B RID: 11
	internal interface ICallbackInfo
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600007E RID: 126
		object ClientData { get; }

		// Token: 0x0600007F RID: 127
		Result? GetResultCode();
	}
}
