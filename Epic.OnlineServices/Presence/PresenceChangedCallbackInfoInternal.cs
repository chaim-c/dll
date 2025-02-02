using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000245 RID: 581
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceChangedCallbackInfoInternal : ICallbackInfoInternal, IGettable<PresenceChangedCallbackInfo>, ISettable<PresenceChangedCallbackInfo>, IDisposable
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00017AC4 File Offset: 0x00015CC4
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x00017AE5 File Offset: 0x00015CE5
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

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00017B10 File Offset: 0x00015D10
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x00017B31 File Offset: 0x00015D31
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00017B44 File Offset: 0x00015D44
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x00017B65 File Offset: 0x00015D65
		public EpicAccountId PresenceUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_PresenceUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_PresenceUserId);
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00017B75 File Offset: 0x00015D75
		public void Set(ref PresenceChangedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PresenceUserId = other.PresenceUserId;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00017BA0 File Offset: 0x00015DA0
		public void Set(ref PresenceChangedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PresenceUserId = other.Value.PresenceUserId;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00017BF9 File Offset: 0x00015DF9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_PresenceUserId);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00017C20 File Offset: 0x00015E20
		public void Get(out PresenceChangedCallbackInfo output)
		{
			output = default(PresenceChangedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000724 RID: 1828
		private IntPtr m_ClientData;

		// Token: 0x04000725 RID: 1829
		private IntPtr m_LocalUserId;

		// Token: 0x04000726 RID: 1830
		private IntPtr m_PresenceUserId;
	}
}
