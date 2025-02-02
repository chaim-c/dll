using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012C RID: 300
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsSettingsInternal : IGettable<SessionDetailsSettings>, ISettable<SessionDetailsSettings>, IDisposable
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0000CDC5 File Offset: 0x0000AFC5
		public Utf8String BucketId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_BucketId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_BucketId);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
		public uint NumPublicConnections
		{
			get
			{
				return this.m_NumPublicConnections;
			}
			set
			{
				this.m_NumPublicConnections = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0000CE1D File Offset: 0x0000B01D
		public bool AllowJoinInProgress
		{
			get
			{
				bool result;
				Helper.Get(this.m_AllowJoinInProgress, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AllowJoinInProgress);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0000CE30 File Offset: 0x0000B030
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0000CE48 File Offset: 0x0000B048
		public OnlineSessionPermissionLevel PermissionLevel
		{
			get
			{
				return this.m_PermissionLevel;
			}
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0000CE54 File Offset: 0x0000B054
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0000CE75 File Offset: 0x0000B075
		public bool InvitesAllowed
		{
			get
			{
				bool result;
				Helper.Get(this.m_InvitesAllowed, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_InvitesAllowed);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0000CE88 File Offset: 0x0000B088
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0000CEA9 File Offset: 0x0000B0A9
		public bool SanctionsEnabled
		{
			get
			{
				bool result;
				Helper.Get(this.m_SanctionsEnabled, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SanctionsEnabled);
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		public void Set(ref SessionDetailsSettings other)
		{
			this.m_ApiVersion = 3;
			this.BucketId = other.BucketId;
			this.NumPublicConnections = other.NumPublicConnections;
			this.AllowJoinInProgress = other.AllowJoinInProgress;
			this.PermissionLevel = other.PermissionLevel;
			this.InvitesAllowed = other.InvitesAllowed;
			this.SanctionsEnabled = other.SanctionsEnabled;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0000CF20 File Offset: 0x0000B120
		public void Set(ref SessionDetailsSettings? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.BucketId = other.Value.BucketId;
				this.NumPublicConnections = other.Value.NumPublicConnections;
				this.AllowJoinInProgress = other.Value.AllowJoinInProgress;
				this.PermissionLevel = other.Value.PermissionLevel;
				this.InvitesAllowed = other.Value.InvitesAllowed;
				this.SanctionsEnabled = other.Value.SanctionsEnabled;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000CFC2 File Offset: 0x0000B1C2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_BucketId);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000CFD1 File Offset: 0x0000B1D1
		public void Get(out SessionDetailsSettings output)
		{
			output = default(SessionDetailsSettings);
			output.Set(ref this);
		}

		// Token: 0x04000416 RID: 1046
		private int m_ApiVersion;

		// Token: 0x04000417 RID: 1047
		private IntPtr m_BucketId;

		// Token: 0x04000418 RID: 1048
		private uint m_NumPublicConnections;

		// Token: 0x04000419 RID: 1049
		private int m_AllowJoinInProgress;

		// Token: 0x0400041A RID: 1050
		private OnlineSessionPermissionLevel m_PermissionLevel;

		// Token: 0x0400041B RID: 1051
		private int m_InvitesAllowed;

		// Token: 0x0400041C RID: 1052
		private int m_SanctionsEnabled;
	}
}
