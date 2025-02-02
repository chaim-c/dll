using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000565 RID: 1381
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlinkAccountCallbackInfoInternal : ICallbackInfoInternal, IGettable<UnlinkAccountCallbackInfo>, ISettable<UnlinkAccountCallbackInfo>, IDisposable
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x00034384 File Offset: 0x00032584
		// (set) Token: 0x0600234E RID: 9038 RVA: 0x0003439C File Offset: 0x0003259C
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

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x000343A8 File Offset: 0x000325A8
		// (set) Token: 0x06002350 RID: 9040 RVA: 0x000343C9 File Offset: 0x000325C9
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

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000343DC File Offset: 0x000325DC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000343F4 File Offset: 0x000325F4
		// (set) Token: 0x06002353 RID: 9043 RVA: 0x00034415 File Offset: 0x00032615
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

		// Token: 0x06002354 RID: 9044 RVA: 0x00034425 File Offset: 0x00032625
		public void Set(ref UnlinkAccountCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00034450 File Offset: 0x00032650
		public void Set(ref UnlinkAccountCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000344A9 File Offset: 0x000326A9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000344C4 File Offset: 0x000326C4
		public void Get(out UnlinkAccountCallbackInfo output)
		{
			output = default(UnlinkAccountCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F85 RID: 3973
		private Result m_ResultCode;

		// Token: 0x04000F86 RID: 3974
		private IntPtr m_ClientData;

		// Token: 0x04000F87 RID: 3975
		private IntPtr m_LocalUserId;
	}
}
