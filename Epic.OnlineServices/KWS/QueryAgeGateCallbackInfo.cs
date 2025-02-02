using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000443 RID: 1091
	public struct QueryAgeGateCallbackInfo : ICallbackInfo
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000294F1 File Offset: 0x000276F1
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x000294F9 File Offset: 0x000276F9
		public Result ResultCode { get; set; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00029502 File Offset: 0x00027702
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x0002950A File Offset: 0x0002770A
		public object ClientData { get; set; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00029513 File Offset: 0x00027713
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0002951B File Offset: 0x0002771B
		public Utf8String CountryCode { get; set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00029524 File Offset: 0x00027724
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x0002952C File Offset: 0x0002772C
		public uint AgeOfConsent { get; set; }

		// Token: 0x06001BFF RID: 7167 RVA: 0x00029538 File Offset: 0x00027738
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00029555 File Offset: 0x00027755
		internal void Set(ref QueryAgeGateCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.CountryCode = other.CountryCode;
			this.AgeOfConsent = other.AgeOfConsent;
		}
	}
}
