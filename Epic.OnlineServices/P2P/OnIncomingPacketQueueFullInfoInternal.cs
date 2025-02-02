using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002C8 RID: 712
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnIncomingPacketQueueFullInfoInternal : ICallbackInfoInternal, IGettable<OnIncomingPacketQueueFullInfo>, ISettable<OnIncomingPacketQueueFullInfo>, IDisposable
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x0001C0DD File Offset: 0x0001A2DD
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

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0001C0F0 File Offset: 0x0001A2F0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0001C108 File Offset: 0x0001A308
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x0001C120 File Offset: 0x0001A320
		public ulong PacketQueueMaxSizeBytes
		{
			get
			{
				return this.m_PacketQueueMaxSizeBytes;
			}
			set
			{
				this.m_PacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0001C12C File Offset: 0x0001A32C
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x0001C144 File Offset: 0x0001A344
		public ulong PacketQueueCurrentSizeBytes
		{
			get
			{
				return this.m_PacketQueueCurrentSizeBytes;
			}
			set
			{
				this.m_PacketQueueCurrentSizeBytes = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0001C150 File Offset: 0x0001A350
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x0001C171 File Offset: 0x0001A371
		public ProductUserId OverflowPacketLocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_OverflowPacketLocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_OverflowPacketLocalUserId);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0001C184 File Offset: 0x0001A384
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x0001C19C File Offset: 0x0001A39C
		public byte OverflowPacketChannel
		{
			get
			{
				return this.m_OverflowPacketChannel;
			}
			set
			{
				this.m_OverflowPacketChannel = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		public uint OverflowPacketSizeBytes
		{
			get
			{
				return this.m_OverflowPacketSizeBytes;
			}
			set
			{
				this.m_OverflowPacketSizeBytes = value;
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public void Set(ref OnIncomingPacketQueueFullInfo other)
		{
			this.ClientData = other.ClientData;
			this.PacketQueueMaxSizeBytes = other.PacketQueueMaxSizeBytes;
			this.PacketQueueCurrentSizeBytes = other.PacketQueueCurrentSizeBytes;
			this.OverflowPacketLocalUserId = other.OverflowPacketLocalUserId;
			this.OverflowPacketChannel = other.OverflowPacketChannel;
			this.OverflowPacketSizeBytes = other.OverflowPacketSizeBytes;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0001C228 File Offset: 0x0001A428
		public void Set(ref OnIncomingPacketQueueFullInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.PacketQueueMaxSizeBytes = other.Value.PacketQueueMaxSizeBytes;
				this.PacketQueueCurrentSizeBytes = other.Value.PacketQueueCurrentSizeBytes;
				this.OverflowPacketLocalUserId = other.Value.OverflowPacketLocalUserId;
				this.OverflowPacketChannel = other.Value.OverflowPacketChannel;
				this.OverflowPacketSizeBytes = other.Value.OverflowPacketSizeBytes;
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0001C2C3 File Offset: 0x0001A4C3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_OverflowPacketLocalUserId);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0001C2DE File Offset: 0x0001A4DE
		public void Get(out OnIncomingPacketQueueFullInfo output)
		{
			output = default(OnIncomingPacketQueueFullInfo);
			output.Set(ref this);
		}

		// Token: 0x04000889 RID: 2185
		private IntPtr m_ClientData;

		// Token: 0x0400088A RID: 2186
		private ulong m_PacketQueueMaxSizeBytes;

		// Token: 0x0400088B RID: 2187
		private ulong m_PacketQueueCurrentSizeBytes;

		// Token: 0x0400088C RID: 2188
		private IntPtr m_OverflowPacketLocalUserId;

		// Token: 0x0400088D RID: 2189
		private byte m_OverflowPacketChannel;

		// Token: 0x0400088E RID: 2190
		private uint m_OverflowPacketSizeBytes;
	}
}
