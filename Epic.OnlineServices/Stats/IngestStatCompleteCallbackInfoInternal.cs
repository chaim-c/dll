using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000AE RID: 174
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestStatCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<IngestStatCompleteCallbackInfo>, ISettable<IngestStatCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00009880 File Offset: 0x00007A80
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00009898 File Offset: 0x00007A98
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000098A4 File Offset: 0x00007AA4
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x000098C5 File Offset: 0x00007AC5
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000098D8 File Offset: 0x00007AD8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x000098F0 File Offset: 0x00007AF0
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00009911 File Offset: 0x00007B11
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00009924 File Offset: 0x00007B24
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x00009945 File Offset: 0x00007B45
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

		// Token: 0x06000662 RID: 1634 RVA: 0x00009955 File Offset: 0x00007B55
		public void Set(ref IngestStatCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000998C File Offset: 0x00007B8C
		public void Set(ref IngestStatCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000099FA File Offset: 0x00007BFA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00009A21 File Offset: 0x00007C21
		public void Get(out IngestStatCompleteCallbackInfo output)
		{
			output = default(IngestStatCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400030F RID: 783
		private Result m_ResultCode;

		// Token: 0x04000310 RID: 784
		private IntPtr m_ClientData;

		// Token: 0x04000311 RID: 785
		private IntPtr m_LocalUserId;

		// Token: 0x04000312 RID: 786
		private IntPtr m_TargetUserId;
	}
}
