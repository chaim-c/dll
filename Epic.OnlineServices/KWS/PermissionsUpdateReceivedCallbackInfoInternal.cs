using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000442 RID: 1090
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PermissionsUpdateReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<PermissionsUpdateReceivedCallbackInfo>, ISettable<PermissionsUpdateReceivedCallbackInfo>, IDisposable
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000293E4 File Offset: 0x000275E4
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x00029405 File Offset: 0x00027605
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00029418 File Offset: 0x00027618
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00029430 File Offset: 0x00027630
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x00029451 File Offset: 0x00027651
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

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00029461 File Offset: 0x00027661
		public void Set(ref PermissionsUpdateReceivedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00029480 File Offset: 0x00027680
		public void Set(ref PermissionsUpdateReceivedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000294C4 File Offset: 0x000276C4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000294DF File Offset: 0x000276DF
		public void Get(out PermissionsUpdateReceivedCallbackInfo output)
		{
			output = default(PermissionsUpdateReceivedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C5F RID: 3167
		private IntPtr m_ClientData;

		// Token: 0x04000C60 RID: 3168
		private IntPtr m_LocalUserId;
	}
}
