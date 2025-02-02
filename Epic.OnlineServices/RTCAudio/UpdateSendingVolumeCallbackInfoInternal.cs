using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F5 RID: 501
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSendingVolumeCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateSendingVolumeCallbackInfo>, ISettable<UpdateSendingVolumeCallbackInfo>, IDisposable
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x000150C8 File Offset: 0x000132C8
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x000150E0 File Offset: 0x000132E0
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

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x000150EC File Offset: 0x000132EC
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x0001510D File Offset: 0x0001330D
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00015120 File Offset: 0x00013320
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00015138 File Offset: 0x00013338
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00015159 File Offset: 0x00013359
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

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0001516C File Offset: 0x0001336C
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x0001518D File Offset: 0x0001338D
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

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000151A0 File Offset: 0x000133A0
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x000151B8 File Offset: 0x000133B8
		public float Volume
		{
			get
			{
				return this.m_Volume;
			}
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000151C4 File Offset: 0x000133C4
		public void Set(ref UpdateSendingVolumeCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Volume = other.Volume;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00015214 File Offset: 0x00013414
		public void Set(ref UpdateSendingVolumeCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Volume = other.Value.Volume;
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00015297 File Offset: 0x00013497
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000152BE File Offset: 0x000134BE
		public void Get(out UpdateSendingVolumeCallbackInfo output)
		{
			output = default(UpdateSendingVolumeCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400065F RID: 1631
		private Result m_ResultCode;

		// Token: 0x04000660 RID: 1632
		private IntPtr m_ClientData;

		// Token: 0x04000661 RID: 1633
		private IntPtr m_LocalUserId;

		// Token: 0x04000662 RID: 1634
		private IntPtr m_RoomName;

		// Token: 0x04000663 RID: 1635
		private float m_Volume;
	}
}
