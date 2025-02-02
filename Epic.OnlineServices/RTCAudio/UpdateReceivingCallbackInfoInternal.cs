using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E9 RID: 489
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateReceivingCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateReceivingCallbackInfo>, ISettable<UpdateReceivingCallbackInfo>, IDisposable
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00014474 File Offset: 0x00012674
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0001448C File Offset: 0x0001268C
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

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00014498 File Offset: 0x00012698
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x000144B9 File Offset: 0x000126B9
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

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000144CC File Offset: 0x000126CC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x000144E4 File Offset: 0x000126E4
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00014505 File Offset: 0x00012705
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

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00014518 File Offset: 0x00012718
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x00014539 File Offset: 0x00012739
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0001454C File Offset: 0x0001274C
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0001456D File Offset: 0x0001276D
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

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00014580 File Offset: 0x00012780
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x000145A1 File Offset: 0x000127A1
		public bool AudioEnabled
		{
			get
			{
				bool result;
				Helper.Get(this.m_AudioEnabled, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AudioEnabled);
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000145B4 File Offset: 0x000127B4
		public void Set(ref UpdateReceivingCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.AudioEnabled = other.AudioEnabled;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00014610 File Offset: 0x00012810
		public void Set(ref UpdateReceivingCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.AudioEnabled = other.Value.AudioEnabled;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000146AB File Offset: 0x000128AB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000146DE File Offset: 0x000128DE
		public void Get(out UpdateReceivingCallbackInfo output)
		{
			output = default(UpdateReceivingCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000629 RID: 1577
		private Result m_ResultCode;

		// Token: 0x0400062A RID: 1578
		private IntPtr m_ClientData;

		// Token: 0x0400062B RID: 1579
		private IntPtr m_LocalUserId;

		// Token: 0x0400062C RID: 1580
		private IntPtr m_RoomName;

		// Token: 0x0400062D RID: 1581
		private IntPtr m_ParticipantId;

		// Token: 0x0400062E RID: 1582
		private int m_AudioEnabled;
	}
}
