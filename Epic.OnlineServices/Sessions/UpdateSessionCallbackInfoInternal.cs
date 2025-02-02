using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000161 RID: 353
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateSessionCallbackInfo>, ISettable<UpdateSessionCallbackInfo>, IDisposable
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0000F2CC File Offset: 0x0000D4CC
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0000F2F9 File Offset: 0x0000D4F9
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

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0000F30C File Offset: 0x0000D50C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0000F324 File Offset: 0x0000D524
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0000F345 File Offset: 0x0000D545
		public Utf8String SessionName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_SessionName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0000F358 File Offset: 0x0000D558
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0000F379 File Offset: 0x0000D579
		public Utf8String SessionId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_SessionId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SessionId);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000F389 File Offset: 0x0000D589
		public void Set(ref UpdateSessionCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.SessionName = other.SessionName;
			this.SessionId = other.SessionId;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		public void Set(ref UpdateSessionCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.SessionName = other.Value.SessionName;
				this.SessionId = other.Value.SessionId;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0000F42E File Offset: 0x0000D62E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_SessionId);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000F455 File Offset: 0x0000D655
		public void Get(out UpdateSessionCallbackInfo output)
		{
			output = default(UpdateSessionCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040004B5 RID: 1205
		private Result m_ResultCode;

		// Token: 0x040004B6 RID: 1206
		private IntPtr m_ClientData;

		// Token: 0x040004B7 RID: 1207
		private IntPtr m_SessionName;

		// Token: 0x040004B8 RID: 1208
		private IntPtr m_SessionId;
	}
}
