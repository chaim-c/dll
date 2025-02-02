using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000268 RID: 616
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<DeleteFileCallbackInfo>, ISettable<DeleteFileCallbackInfo>, IDisposable
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00018E94 File Offset: 0x00017094
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00018EAC File Offset: 0x000170AC
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

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00018EB8 File Offset: 0x000170B8
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00018ED9 File Offset: 0x000170D9
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00018EEC File Offset: 0x000170EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00018F04 File Offset: 0x00017104
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00018F25 File Offset: 0x00017125
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

		// Token: 0x060010D6 RID: 4310 RVA: 0x00018F35 File Offset: 0x00017135
		public void Set(ref DeleteFileCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00018F60 File Offset: 0x00017160
		public void Set(ref DeleteFileCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00018FB9 File Offset: 0x000171B9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00018FD4 File Offset: 0x000171D4
		public void Get(out DeleteFileCallbackInfo output)
		{
			output = default(DeleteFileCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000789 RID: 1929
		private Result m_ResultCode;

		// Token: 0x0400078A RID: 1930
		private IntPtr m_ClientData;

		// Token: 0x0400078B RID: 1931
		private IntPtr m_LocalUserId;
	}
}
