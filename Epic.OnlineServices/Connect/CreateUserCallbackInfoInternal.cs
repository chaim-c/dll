using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000523 RID: 1315
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserCallbackInfoInternal : ICallbackInfoInternal, IGettable<CreateUserCallbackInfo>, ISettable<CreateUserCallbackInfo>, IDisposable
	{
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x00032764 File Offset: 0x00030964
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x0003277C File Offset: 0x0003097C
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

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x00032788 File Offset: 0x00030988
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x000327A9 File Offset: 0x000309A9
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x000327BC File Offset: 0x000309BC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x000327D4 File Offset: 0x000309D4
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x000327F5 File Offset: 0x000309F5
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

		// Token: 0x060021C8 RID: 8648 RVA: 0x00032805 File Offset: 0x00030A05
		public void Set(ref CreateUserCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00032830 File Offset: 0x00030A30
		public void Set(ref CreateUserCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00032889 File Offset: 0x00030A89
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000328A4 File Offset: 0x00030AA4
		public void Get(out CreateUserCallbackInfo output)
		{
			output = default(CreateUserCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F08 RID: 3848
		private Result m_ResultCode;

		// Token: 0x04000F09 RID: 3849
		private IntPtr m_ClientData;

		// Token: 0x04000F0A RID: 3850
		private IntPtr m_LocalUserId;
	}
}
