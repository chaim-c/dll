using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026C RID: 620
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DuplicateFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<DuplicateFileCallbackInfo>, ISettable<DuplicateFileCallbackInfo>, IDisposable
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00019130 File Offset: 0x00017330
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x00019148 File Offset: 0x00017348
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

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00019154 File Offset: 0x00017354
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x00019175 File Offset: 0x00017375
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

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00019188 File Offset: 0x00017388
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x000191A0 File Offset: 0x000173A0
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x000191C1 File Offset: 0x000173C1
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

		// Token: 0x060010F2 RID: 4338 RVA: 0x000191D1 File Offset: 0x000173D1
		public void Set(ref DuplicateFileCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000191FC File Offset: 0x000173FC
		public void Set(ref DuplicateFileCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00019255 File Offset: 0x00017455
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00019270 File Offset: 0x00017470
		public void Get(out DuplicateFileCallbackInfo output)
		{
			output = default(DuplicateFileCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000794 RID: 1940
		private Result m_ResultCode;

		// Token: 0x04000795 RID: 1941
		private IntPtr m_ClientData;

		// Token: 0x04000796 RID: 1942
		private IntPtr m_LocalUserId;
	}
}
