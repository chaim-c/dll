using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000522 RID: 1314
	public struct CreateUserCallbackInfo : ICallbackInfo
	{
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000326E9 File Offset: 0x000308E9
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x000326F1 File Offset: 0x000308F1
		public Result ResultCode { get; set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x000326FA File Offset: 0x000308FA
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x00032702 File Offset: 0x00030902
		public object ClientData { get; set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0003270B File Offset: 0x0003090B
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x00032713 File Offset: 0x00030913
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x060021BF RID: 8639 RVA: 0x0003271C File Offset: 0x0003091C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00032739 File Offset: 0x00030939
		internal void Set(ref CreateUserCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
