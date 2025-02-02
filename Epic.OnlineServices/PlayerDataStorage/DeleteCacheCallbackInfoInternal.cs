using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000264 RID: 612
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheCallbackInfoInternal : ICallbackInfoInternal, IGettable<DeleteCacheCallbackInfo>, ISettable<DeleteCacheCallbackInfo>, IDisposable
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00018C48 File Offset: 0x00016E48
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00018C60 File Offset: 0x00016E60
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00018C6C File Offset: 0x00016E6C
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x00018C8D File Offset: 0x00016E8D
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00018CA0 File Offset: 0x00016EA0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00018CB8 File Offset: 0x00016EB8
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x00018CD9 File Offset: 0x00016ED9
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

		// Token: 0x060010BD RID: 4285 RVA: 0x00018CE9 File Offset: 0x00016EE9
		public void Set(ref DeleteCacheCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00018D14 File Offset: 0x00016F14
		public void Set(ref DeleteCacheCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00018D6D File Offset: 0x00016F6D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00018D88 File Offset: 0x00016F88
		public void Get(out DeleteCacheCallbackInfo output)
		{
			output = default(DeleteCacheCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000780 RID: 1920
		private Result m_ResultCode;

		// Token: 0x04000781 RID: 1921
		private IntPtr m_ClientData;

		// Token: 0x04000782 RID: 1922
		private IntPtr m_LocalUserId;
	}
}
