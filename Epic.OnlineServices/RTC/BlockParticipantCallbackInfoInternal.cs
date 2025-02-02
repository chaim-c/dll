using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000178 RID: 376
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BlockParticipantCallbackInfoInternal : ICallbackInfoInternal, IGettable<BlockParticipantCallbackInfo>, ISettable<BlockParticipantCallbackInfo>, IDisposable
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0000FF78 File Offset: 0x0000E178
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0000FF90 File Offset: 0x0000E190
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0000FF9C File Offset: 0x0000E19C
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0000FFBD File Offset: 0x0000E1BD
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00010009 File Offset: 0x0000E209
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001001C File Offset: 0x0000E21C
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0001003D File Offset: 0x0000E23D
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00010050 File Offset: 0x0000E250
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00010071 File Offset: 0x0000E271
		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_ParticipantId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ParticipantId);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00010084 File Offset: 0x0000E284
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x000100A5 File Offset: 0x0000E2A5
		public bool Blocked
		{
			get
			{
				bool result;
				Helper.Get(this.m_Blocked, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Blocked);
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000100B8 File Offset: 0x0000E2B8
		public void Set(ref BlockParticipantCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Blocked = other.Blocked;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00010114 File Offset: 0x0000E314
		public void Set(ref BlockParticipantCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.Blocked = other.Value.Blocked;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000101AF File Offset: 0x0000E3AF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000101E2 File Offset: 0x0000E3E2
		public void Get(out BlockParticipantCallbackInfo output)
		{
			output = default(BlockParticipantCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040004F1 RID: 1265
		private Result m_ResultCode;

		// Token: 0x040004F2 RID: 1266
		private IntPtr m_ClientData;

		// Token: 0x040004F3 RID: 1267
		private IntPtr m_LocalUserId;

		// Token: 0x040004F4 RID: 1268
		private IntPtr m_RoomName;

		// Token: 0x040004F5 RID: 1269
		private IntPtr m_ParticipantId;

		// Token: 0x040004F6 RID: 1270
		private int m_Blocked;
	}
}
