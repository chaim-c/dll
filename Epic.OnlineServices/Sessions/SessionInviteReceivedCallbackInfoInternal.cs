using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000130 RID: 304
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionInviteReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<SessionInviteReceivedCallbackInfo>, ISettable<SessionInviteReceivedCallbackInfo>, IDisposable
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0000D37C File Offset: 0x0000B57C
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0000D39D File Offset: 0x0000B59D
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0000D3E9 File Offset: 0x0000B5E9
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

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0000D41D File Offset: 0x0000B61D
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0000D430 File Offset: 0x0000B630
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0000D451 File Offset: 0x0000B651
		public Utf8String InviteId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_InviteId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000D461 File Offset: 0x0000B661
		public void Set(ref SessionInviteReceivedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.InviteId = other.InviteId;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000D498 File Offset: 0x0000B698
		public void Set(ref SessionInviteReceivedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0000D506 File Offset: 0x0000B706
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_InviteId);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0000D539 File Offset: 0x0000B739
		public void Get(out SessionInviteReceivedCallbackInfo output)
		{
			output = default(SessionInviteReceivedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400042B RID: 1067
		private IntPtr m_ClientData;

		// Token: 0x0400042C RID: 1068
		private IntPtr m_LocalUserId;

		// Token: 0x0400042D RID: 1069
		private IntPtr m_TargetUserId;

		// Token: 0x0400042E RID: 1070
		private IntPtr m_InviteId;
	}
}
