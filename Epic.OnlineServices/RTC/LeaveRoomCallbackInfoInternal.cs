using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000183 RID: 387
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveRoomCallbackInfoInternal : ICallbackInfoInternal, IGettable<LeaveRoomCallbackInfo>, ISettable<LeaveRoomCallbackInfo>, IDisposable
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00010B14 File Offset: 0x0000ED14
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x00010B2C File Offset: 0x0000ED2C
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

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00010B38 File Offset: 0x0000ED38
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00010B59 File Offset: 0x0000ED59
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

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00010B6C File Offset: 0x0000ED6C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00010B84 File Offset: 0x0000ED84
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x00010BA5 File Offset: 0x0000EDA5
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

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x00010BD9 File Offset: 0x0000EDD9
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

		// Token: 0x06000B30 RID: 2864 RVA: 0x00010BE9 File Offset: 0x0000EDE9
		public void Set(ref LeaveRoomCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00010C20 File Offset: 0x0000EE20
		public void Set(ref LeaveRoomCallbackInfo? other)
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

		// Token: 0x06000B32 RID: 2866 RVA: 0x00010C8E File Offset: 0x0000EE8E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00010CB5 File Offset: 0x0000EEB5
		public void Get(out LeaveRoomCallbackInfo output)
		{
			output = default(LeaveRoomCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000528 RID: 1320
		private Result m_ResultCode;

		// Token: 0x04000529 RID: 1321
		private IntPtr m_ClientData;

		// Token: 0x0400052A RID: 1322
		private IntPtr m_LocalUserId;

		// Token: 0x0400052B RID: 1323
		private IntPtr m_RoomName;
	}
}
