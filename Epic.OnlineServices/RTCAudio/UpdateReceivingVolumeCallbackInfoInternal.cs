using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001ED RID: 493
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateReceivingVolumeCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateReceivingVolumeCallbackInfo>, ISettable<UpdateReceivingVolumeCallbackInfo>, IDisposable
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00014918 File Offset: 0x00012B18
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00014930 File Offset: 0x00012B30
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

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0001493C File Offset: 0x00012B3C
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0001495D File Offset: 0x00012B5D
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

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00014970 File Offset: 0x00012B70
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00014988 File Offset: 0x00012B88
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x000149A9 File Offset: 0x00012BA9
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

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000149BC File Offset: 0x00012BBC
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x000149DD File Offset: 0x00012BDD
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000149F0 File Offset: 0x00012BF0
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x00014A08 File Offset: 0x00012C08
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

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00014A14 File Offset: 0x00012C14
		public void Set(ref UpdateReceivingVolumeCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Volume = other.Volume;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00014A64 File Offset: 0x00012C64
		public void Set(ref UpdateReceivingVolumeCallbackInfo? other)
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

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00014AE7 File Offset: 0x00012CE7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00014B0E File Offset: 0x00012D0E
		public void Get(out UpdateReceivingVolumeCallbackInfo output)
		{
			output = default(UpdateReceivingVolumeCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400063D RID: 1597
		private Result m_ResultCode;

		// Token: 0x0400063E RID: 1598
		private IntPtr m_ClientData;

		// Token: 0x0400063F RID: 1599
		private IntPtr m_LocalUserId;

		// Token: 0x04000640 RID: 1600
		private IntPtr m_RoomName;

		// Token: 0x04000641 RID: 1601
		private float m_Volume;
	}
}
