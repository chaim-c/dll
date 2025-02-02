using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034D RID: 845
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HardMuteMemberCallbackInfoInternal : ICallbackInfoInternal, IGettable<HardMuteMemberCallbackInfo>, ISettable<HardMuteMemberCallbackInfo>, IDisposable
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00021104 File Offset: 0x0001F304
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x0002111C File Offset: 0x0001F31C
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

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00021128 File Offset: 0x0001F328
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x00021149 File Offset: 0x0001F349
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

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0002115C File Offset: 0x0001F35C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00021174 File Offset: 0x0001F374
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x00021195 File Offset: 0x0001F395
		public Utf8String LobbyId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LobbyId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000211A8 File Offset: 0x0001F3A8
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x000211C9 File Offset: 0x0001F3C9
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

		// Token: 0x06001652 RID: 5714 RVA: 0x000211D9 File Offset: 0x0001F3D9
		public void Set(ref HardMuteMemberCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00021210 File Offset: 0x0001F410
		public void Set(ref HardMuteMemberCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0002127E File Offset: 0x0001F47E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000212A5 File Offset: 0x0001F4A5
		public void Get(out HardMuteMemberCallbackInfo output)
		{
			output = default(HardMuteMemberCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A1E RID: 2590
		private Result m_ResultCode;

		// Token: 0x04000A1F RID: 2591
		private IntPtr m_ClientData;

		// Token: 0x04000A20 RID: 2592
		private IntPtr m_LobbyId;

		// Token: 0x04000A21 RID: 2593
		private IntPtr m_TargetUserId;
	}
}
