using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200017C RID: 380
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DisconnectedCallbackInfoInternal : ICallbackInfoInternal, IGettable<DisconnectedCallbackInfo>, ISettable<DisconnectedCallbackInfo>, IDisposable
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000103EC File Offset: 0x0000E5EC
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00010404 File Offset: 0x0000E604
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

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00010410 File Offset: 0x0000E610
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00010431 File Offset: 0x0000E631
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

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00010444 File Offset: 0x0000E644
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0001045C File Offset: 0x0000E65C
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0001047D File Offset: 0x0000E67D
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

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00010490 File Offset: 0x0000E690
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x000104B1 File Offset: 0x0000E6B1
		public Utf8String RoomName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_RoomName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000104C1 File Offset: 0x0000E6C1
		public void Set(ref DisconnectedCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000104F8 File Offset: 0x0000E6F8
		public void Set(ref DisconnectedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00010566 File Offset: 0x0000E766
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001058D File Offset: 0x0000E78D
		public void Get(out DisconnectedCallbackInfo output)
		{
			output = default(DisconnectedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000504 RID: 1284
		private Result m_ResultCode;

		// Token: 0x04000505 RID: 1285
		private IntPtr m_ClientData;

		// Token: 0x04000506 RID: 1286
		private IntPtr m_LocalUserId;

		// Token: 0x04000507 RID: 1287
		private IntPtr m_RoomName;
	}
}
