using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C4 RID: 196
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionInfoInternal : IGettable<ActiveSessionInfo>, ISettable<ActiveSessionInfo>, IDisposable
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0000A5C8 File Offset: 0x000087C8
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0000A5E9 File Offset: 0x000087E9
		public Utf8String SessionName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_SessionName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000A5FC File Offset: 0x000087FC
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0000A61D File Offset: 0x0000881D
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000A630 File Offset: 0x00008830
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0000A648 File Offset: 0x00008848
		public OnlineSessionState State
		{
			get
			{
				return this.m_State;
			}
			set
			{
				this.m_State = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000A654 File Offset: 0x00008854
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0000A675 File Offset: 0x00008875
		public SessionDetailsInfo? SessionDetails
		{
			get
			{
				SessionDetailsInfo? result;
				Helper.Get<SessionDetailsInfoInternal, SessionDetailsInfo>(this.m_SessionDetails, out result);
				return result;
			}
			set
			{
				Helper.Set<SessionDetailsInfo, SessionDetailsInfoInternal>(ref value, ref this.m_SessionDetails);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000A686 File Offset: 0x00008886
		public void Set(ref ActiveSessionInfo other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
			this.LocalUserId = other.LocalUserId;
			this.State = other.State;
			this.SessionDetails = other.SessionDetails;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000A6C4 File Offset: 0x000088C4
		public void Set(ref ActiveSessionInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
				this.LocalUserId = other.Value.LocalUserId;
				this.State = other.Value.State;
				this.SessionDetails = other.Value.SessionDetails;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000A739 File Offset: 0x00008939
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SessionDetails);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000A760 File Offset: 0x00008960
		public void Get(out ActiveSessionInfo output)
		{
			output = default(ActiveSessionInfo);
			output.Set(ref this);
		}

		// Token: 0x04000350 RID: 848
		private int m_ApiVersion;

		// Token: 0x04000351 RID: 849
		private IntPtr m_SessionName;

		// Token: 0x04000352 RID: 850
		private IntPtr m_LocalUserId;

		// Token: 0x04000353 RID: 851
		private OnlineSessionState m_State;

		// Token: 0x04000354 RID: 852
		private IntPtr m_SessionDetails;
	}
}
