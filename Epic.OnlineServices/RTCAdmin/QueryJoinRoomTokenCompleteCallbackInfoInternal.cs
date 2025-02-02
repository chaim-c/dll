using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000207 RID: 519
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryJoinRoomTokenCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryJoinRoomTokenCompleteCallbackInfo>, ISettable<QueryJoinRoomTokenCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00015844 File Offset: 0x00013A44
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0001585C File Offset: 0x00013A5C
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00015868 File Offset: 0x00013A68
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00015889 File Offset: 0x00013A89
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0001589C File Offset: 0x00013A9C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000158B4 File Offset: 0x00013AB4
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x000158D5 File Offset: 0x00013AD5
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000158E8 File Offset: 0x00013AE8
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x00015909 File Offset: 0x00013B09
		public Utf8String ClientBaseUrl
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ClientBaseUrl, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientBaseUrl);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0001591C File Offset: 0x00013B1C
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00015934 File Offset: 0x00013B34
		public uint QueryId
		{
			get
			{
				return this.m_QueryId;
			}
			set
			{
				this.m_QueryId = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00015940 File Offset: 0x00013B40
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00015958 File Offset: 0x00013B58
		public uint TokenCount
		{
			get
			{
				return this.m_TokenCount;
			}
			set
			{
				this.m_TokenCount = value;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00015964 File Offset: 0x00013B64
		public void Set(ref QueryJoinRoomTokenCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.RoomName = other.RoomName;
			this.ClientBaseUrl = other.ClientBaseUrl;
			this.QueryId = other.QueryId;
			this.TokenCount = other.TokenCount;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000159C0 File Offset: 0x00013BC0
		public void Set(ref QueryJoinRoomTokenCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.RoomName = other.Value.RoomName;
				this.ClientBaseUrl = other.Value.ClientBaseUrl;
				this.QueryId = other.Value.QueryId;
				this.TokenCount = other.Value.TokenCount;
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00015A5B File Offset: 0x00013C5B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ClientBaseUrl);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00015A82 File Offset: 0x00013C82
		public void Get(out QueryJoinRoomTokenCompleteCallbackInfo output)
		{
			output = default(QueryJoinRoomTokenCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000684 RID: 1668
		private Result m_ResultCode;

		// Token: 0x04000685 RID: 1669
		private IntPtr m_ClientData;

		// Token: 0x04000686 RID: 1670
		private IntPtr m_RoomName;

		// Token: 0x04000687 RID: 1671
		private IntPtr m_ClientBaseUrl;

		// Token: 0x04000688 RID: 1672
		private uint m_QueryId;

		// Token: 0x04000689 RID: 1673
		private uint m_TokenCount;
	}
}
