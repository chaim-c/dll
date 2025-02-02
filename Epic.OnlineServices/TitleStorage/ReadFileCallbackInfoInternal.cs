using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200009D RID: 157
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<ReadFileCallbackInfo>, ISettable<ReadFileCallbackInfo>, IDisposable
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000088D8 File Offset: 0x00006AD8
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x000088F0 File Offset: 0x00006AF0
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000088FC File Offset: 0x00006AFC
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000891D File Offset: 0x00006B1D
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00008930 File Offset: 0x00006B30
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00008948 File Offset: 0x00006B48
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00008969 File Offset: 0x00006B69
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000897C File Offset: 0x00006B7C
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0000899D File Offset: 0x00006B9D
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

		// Token: 0x060005E0 RID: 1504 RVA: 0x000089AD File Offset: 0x00006BAD
		public void Set(ref ReadFileCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000089E4 File Offset: 0x00006BE4
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

		// Token: 0x060005E2 RID: 1506 RVA: 0x00008A52 File Offset: 0x00006C52
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00008A79 File Offset: 0x00006C79
		public void Get(out ReadFileCallbackInfo output)
		{
			output = default(ReadFileCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040002C7 RID: 711
		private Result m_ResultCode;

		// Token: 0x040002C8 RID: 712
		private IntPtr m_ClientData;

		// Token: 0x040002C9 RID: 713
		private IntPtr m_LocalUserId;

		// Token: 0x040002CA RID: 714
		private IntPtr m_Filename;
	}
}
