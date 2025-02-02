using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200068A RID: 1674
	public struct OnQueryDefinitionsCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x00040567 File Offset: 0x0003E767
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x0004056F File Offset: 0x0003E76F
		public Result ResultCode { get; set; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x00040578 File Offset: 0x0003E778
		// (set) Token: 0x06002AF2 RID: 10994 RVA: 0x00040580 File Offset: 0x0003E780
		public object ClientData { get; set; }

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0004058C File Offset: 0x0003E78C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000405A9 File Offset: 0x0003E7A9
		internal void Set(ref OnQueryDefinitionsCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
