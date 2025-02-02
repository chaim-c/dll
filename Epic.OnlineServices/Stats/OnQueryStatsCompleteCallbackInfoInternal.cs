using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B6 RID: 182
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryStatsCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnQueryStatsCompleteCallbackInfo>, ISettable<OnQueryStatsCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00009BF0 File Offset: 0x00007DF0
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00009C08 File Offset: 0x00007E08
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00009C14 File Offset: 0x00007E14
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00009C35 File Offset: 0x00007E35
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00009C48 File Offset: 0x00007E48
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00009C60 File Offset: 0x00007E60
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00009C81 File Offset: 0x00007E81
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00009C94 File Offset: 0x00007E94
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00009CB5 File Offset: 0x00007EB5
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

		// Token: 0x06000695 RID: 1685 RVA: 0x00009CC5 File Offset: 0x00007EC5
		public void Set(ref OnQueryStatsCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00009CFC File Offset: 0x00007EFC
		public void Set(ref OnQueryStatsCompleteCallbackInfo? other)
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

		// Token: 0x06000697 RID: 1687 RVA: 0x00009D6A File Offset: 0x00007F6A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00009D91 File Offset: 0x00007F91
		public void Get(out OnQueryStatsCompleteCallbackInfo output)
		{
			output = default(OnQueryStatsCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400031F RID: 799
		private Result m_ResultCode;

		// Token: 0x04000320 RID: 800
		private IntPtr m_ClientData;

		// Token: 0x04000321 RID: 801
		private IntPtr m_LocalUserId;

		// Token: 0x04000322 RID: 802
		private IntPtr m_TargetUserId;
	}
}
