using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200017E RID: 382
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinRoomCallbackInfoInternal : ICallbackInfoInternal, IGettable<JoinRoomCallbackInfo>, ISettable<JoinRoomCallbackInfo>, IDisposable
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00010638 File Offset: 0x0000E838
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x00010650 File Offset: 0x0000E850
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

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001065C File Offset: 0x0000E85C
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0001067D File Offset: 0x0000E87D
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00010690 File Offset: 0x0000E890
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000106A8 File Offset: 0x0000E8A8
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x000106C9 File Offset: 0x0000E8C9
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000106DC File Offset: 0x0000E8DC
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x000106FD File Offset: 0x0000E8FD
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

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001070D File Offset: 0x0000E90D
		public void Set(ref JoinRoomCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00010744 File Offset: 0x0000E944
		public void Set(ref JoinRoomCallbackInfo? other)
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

		// Token: 0x06000B00 RID: 2816 RVA: 0x000107B2 File Offset: 0x0000E9B2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000107D9 File Offset: 0x0000E9D9
		public void Get(out JoinRoomCallbackInfo output)
		{
			output = default(JoinRoomCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400050C RID: 1292
		private Result m_ResultCode;

		// Token: 0x0400050D RID: 1293
		private IntPtr m_ClientData;

		// Token: 0x0400050E RID: 1294
		private IntPtr m_LocalUserId;

		// Token: 0x0400050F RID: 1295
		private IntPtr m_RoomName;
	}
}
