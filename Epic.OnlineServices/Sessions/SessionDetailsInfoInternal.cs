using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012A RID: 298
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsInfoInternal : IGettable<SessionDetailsInfo>, ISettable<SessionDetailsInfo>, IDisposable
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0000CB38 File Offset: 0x0000AD38
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0000CB59 File Offset: 0x0000AD59
		public Utf8String SessionId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_SessionId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SessionId);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x0000CB8D File Offset: 0x0000AD8D
		public Utf8String HostAddress
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_HostAddress, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_HostAddress);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		public uint NumOpenPublicConnections
		{
			get
			{
				return this.m_NumOpenPublicConnections;
			}
			set
			{
				this.m_NumOpenPublicConnections = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0000CBE5 File Offset: 0x0000ADE5
		public SessionDetailsSettings? Settings
		{
			get
			{
				SessionDetailsSettings? result;
				Helper.Get<SessionDetailsSettingsInternal, SessionDetailsSettings>(this.m_Settings, out result);
				return result;
			}
			set
			{
				Helper.Set<SessionDetailsSettings, SessionDetailsSettingsInternal>(ref value, ref this.m_Settings);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0000CBF6 File Offset: 0x0000ADF6
		public void Set(ref SessionDetailsInfo other)
		{
			this.m_ApiVersion = 1;
			this.SessionId = other.SessionId;
			this.HostAddress = other.HostAddress;
			this.NumOpenPublicConnections = other.NumOpenPublicConnections;
			this.Settings = other.Settings;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0000CC34 File Offset: 0x0000AE34
		public void Set(ref SessionDetailsInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionId = other.Value.SessionId;
				this.HostAddress = other.Value.HostAddress;
				this.NumOpenPublicConnections = other.Value.NumOpenPublicConnections;
				this.Settings = other.Value.Settings;
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000CCA9 File Offset: 0x0000AEA9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionId);
			Helper.Dispose(ref this.m_HostAddress);
			Helper.Dispose(ref this.m_Settings);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public void Get(out SessionDetailsInfo output)
		{
			output = default(SessionDetailsInfo);
			output.Set(ref this);
		}

		// Token: 0x0400040B RID: 1035
		private int m_ApiVersion;

		// Token: 0x0400040C RID: 1036
		private IntPtr m_SessionId;

		// Token: 0x0400040D RID: 1037
		private IntPtr m_HostAddress;

		// Token: 0x0400040E RID: 1038
		private uint m_NumOpenPublicConnections;

		// Token: 0x0400040F RID: 1039
		private IntPtr m_Settings;
	}
}
