using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000272 RID: 626
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileTransferProgressCallbackInfoInternal : ICallbackInfoInternal, IGettable<FileTransferProgressCallbackInfo>, ISettable<FileTransferProgressCallbackInfo>, IDisposable
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000196F8 File Offset: 0x000178F8
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x00019719 File Offset: 0x00017919
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

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0001972C File Offset: 0x0001792C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00019744 File Offset: 0x00017944
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00019765 File Offset: 0x00017965
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

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00019778 File Offset: 0x00017978
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00019799 File Offset: 0x00017999
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

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x000197AC File Offset: 0x000179AC
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x000197C4 File Offset: 0x000179C4
		public uint BytesTransferred
		{
			get
			{
				return this.m_BytesTransferred;
			}
			set
			{
				this.m_BytesTransferred = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x000197D0 File Offset: 0x000179D0
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x000197E8 File Offset: 0x000179E8
		public uint TotalFileSizeBytes
		{
			get
			{
				return this.m_TotalFileSizeBytes;
			}
			set
			{
				this.m_TotalFileSizeBytes = value;
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000197F4 File Offset: 0x000179F4
		public void Set(ref FileTransferProgressCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.BytesTransferred = other.BytesTransferred;
			this.TotalFileSizeBytes = other.TotalFileSizeBytes;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00019844 File Offset: 0x00017A44
		public void Set(ref FileTransferProgressCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.BytesTransferred = other.Value.BytesTransferred;
				this.TotalFileSizeBytes = other.Value.TotalFileSizeBytes;
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000198C7 File Offset: 0x00017AC7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000198EE File Offset: 0x00017AEE
		public void Get(out FileTransferProgressCallbackInfo output)
		{
			output = default(FileTransferProgressCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040007AE RID: 1966
		private IntPtr m_ClientData;

		// Token: 0x040007AF RID: 1967
		private IntPtr m_LocalUserId;

		// Token: 0x040007B0 RID: 1968
		private IntPtr m_Filename;

		// Token: 0x040007B1 RID: 1969
		private uint m_BytesTransferred;

		// Token: 0x040007B2 RID: 1970
		private uint m_TotalFileSizeBytes;
	}
}
