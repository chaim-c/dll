using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200029B RID: 667
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<WriteFileCallbackInfo>, ISettable<WriteFileCallbackInfo>, IDisposable
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0001ADFC File Offset: 0x00018FFC
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x0001AE14 File Offset: 0x00019014
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

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0001AE20 File Offset: 0x00019020
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x0001AE41 File Offset: 0x00019041
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

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0001AE54 File Offset: 0x00019054
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0001AE6C File Offset: 0x0001906C
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x0001AE8D File Offset: 0x0001908D
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

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0001AEA0 File Offset: 0x000190A0
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x0001AEC1 File Offset: 0x000190C1
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

		// Token: 0x0600123C RID: 4668 RVA: 0x0001AED1 File Offset: 0x000190D1
		public void Set(ref WriteFileCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0001AF08 File Offset: 0x00019108
		public void Set(ref WriteFileCallbackInfo? other)
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

		// Token: 0x0600123E RID: 4670 RVA: 0x0001AF76 File Offset: 0x00019176
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0001AF9D File Offset: 0x0001919D
		public void Get(out WriteFileCallbackInfo output)
		{
			output = default(WriteFileCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400080D RID: 2061
		private Result m_ResultCode;

		// Token: 0x0400080E RID: 2062
		private IntPtr m_ClientData;

		// Token: 0x0400080F RID: 2063
		private IntPtr m_LocalUserId;

		// Token: 0x04000810 RID: 2064
		private IntPtr m_Filename;
	}
}
