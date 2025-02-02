using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200029C RID: 668
	public struct WriteFileDataCallbackInfo : ICallbackInfo
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0001AFAF File Offset: 0x000191AF
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x0001AFB7 File Offset: 0x000191B7
		public object ClientData { get; set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0001AFC0 File Offset: 0x000191C0
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x0001AFC8 File Offset: 0x000191C8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0001AFD1 File Offset: 0x000191D1
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x0001AFD9 File Offset: 0x000191D9
		public Utf8String Filename { get; set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0001AFE2 File Offset: 0x000191E2
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x0001AFEA File Offset: 0x000191EA
		public uint DataBufferLengthBytes { get; set; }

		// Token: 0x06001248 RID: 4680 RVA: 0x0001AFF4 File Offset: 0x000191F4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0001B00F File Offset: 0x0001920F
		internal void Set(ref WriteFileDataCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.DataBufferLengthBytes = other.DataBufferLengthBytes;
		}
	}
}
