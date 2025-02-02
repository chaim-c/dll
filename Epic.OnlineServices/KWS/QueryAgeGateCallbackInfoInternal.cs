using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000444 RID: 1092
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryAgeGateCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryAgeGateCallbackInfo>, ISettable<QueryAgeGateCallbackInfo>, IDisposable
	{
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0002958C File Offset: 0x0002778C
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x000295A4 File Offset: 0x000277A4
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x000295B0 File Offset: 0x000277B0
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x000295D1 File Offset: 0x000277D1
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000295E4 File Offset: 0x000277E4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x000295FC File Offset: 0x000277FC
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x0002961D File Offset: 0x0002781D
		public Utf8String CountryCode
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_CountryCode, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_CountryCode);
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00029630 File Offset: 0x00027830
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x00029648 File Offset: 0x00027848
		public uint AgeOfConsent
		{
			get
			{
				return this.m_AgeOfConsent;
			}
			set
			{
				this.m_AgeOfConsent = value;
			}
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00029652 File Offset: 0x00027852
		public void Set(ref QueryAgeGateCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.CountryCode = other.CountryCode;
			this.AgeOfConsent = other.AgeOfConsent;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0002968C File Offset: 0x0002788C
		public void Set(ref QueryAgeGateCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.CountryCode = other.Value.CountryCode;
				this.AgeOfConsent = other.Value.AgeOfConsent;
			}
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000296FA File Offset: 0x000278FA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_CountryCode);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00029715 File Offset: 0x00027915
		public void Get(out QueryAgeGateCallbackInfo output)
		{
			output = default(QueryAgeGateCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C65 RID: 3173
		private Result m_ResultCode;

		// Token: 0x04000C66 RID: 3174
		private IntPtr m_ClientData;

		// Token: 0x04000C67 RID: 3175
		private IntPtr m_CountryCode;

		// Token: 0x04000C68 RID: 3176
		private uint m_AgeOfConsent;
	}
}
