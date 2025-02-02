using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000512 RID: 1298
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AuthExpirationCallbackInfoInternal : ICallbackInfoInternal, IGettable<AuthExpirationCallbackInfo>, ISettable<AuthExpirationCallbackInfo>, IDisposable
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x000316FC File Offset: 0x0002F8FC
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x0003171D File Offset: 0x0002F91D
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

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00031730 File Offset: 0x0002F930
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00031748 File Offset: 0x0002F948
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x00031769 File Offset: 0x0002F969
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

		// Token: 0x06002152 RID: 8530 RVA: 0x00031779 File Offset: 0x0002F979
		public void Set(ref AuthExpirationCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00031798 File Offset: 0x0002F998
		public void Set(ref AuthExpirationCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000317DC File Offset: 0x0002F9DC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000317F7 File Offset: 0x0002F9F7
		public void Get(out AuthExpirationCallbackInfo output)
		{
			output = default(AuthExpirationCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000EC8 RID: 3784
		private IntPtr m_ClientData;

		// Token: 0x04000EC9 RID: 3785
		private IntPtr m_LocalUserId;
	}
}
