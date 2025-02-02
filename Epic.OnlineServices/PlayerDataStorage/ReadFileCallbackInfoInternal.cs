using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000294 RID: 660
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<ReadFileCallbackInfo>, ISettable<ReadFileCallbackInfo>, IDisposable
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0001A5F8 File Offset: 0x000187F8
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0001A610 File Offset: 0x00018810
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

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0001A61C File Offset: 0x0001881C
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0001A63D File Offset: 0x0001883D
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

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0001A650 File Offset: 0x00018850
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0001A668 File Offset: 0x00018868
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0001A689 File Offset: 0x00018889
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

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0001A69C File Offset: 0x0001889C
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0001A6BD File Offset: 0x000188BD
		public Utf8String Filename
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Filename, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0001A6CD File Offset: 0x000188CD
		public void Set(ref ReadFileCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0001A704 File Offset: 0x00018904
		public void Set(ref ReadFileCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0001A772 File Offset: 0x00018972
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0001A799 File Offset: 0x00018999
		public void Get(out ReadFileCallbackInfo output)
		{
			output = default(ReadFileCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040007E7 RID: 2023
		private Result m_ResultCode;

		// Token: 0x040007E8 RID: 2024
		private IntPtr m_ClientData;

		// Token: 0x040007E9 RID: 2025
		private IntPtr m_LocalUserId;

		// Token: 0x040007EA RID: 2026
		private IntPtr m_Filename;
	}
}
